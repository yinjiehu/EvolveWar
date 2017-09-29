using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using System;
using System.Runtime.Serialization.Formatters;
using System.Net;
using Network.Data;
using Network.Code;
using Network.Utillity;
using Network.Http;
using UI.Main;

namespace Network.Http
{
	public class HttpClient : MonoBehaviour
	{
		static HttpClient _instance;

		public static HttpClient Instance
		{
			get
			{
				if(_instance == null)
				{
					var go = new GameObject("HttpClient");
					_instance = go.AddComponent<HttpClient>();
					DontDestroyOnLoad(go);
				}
				return _instance;
			}
		}

		[SerializeField]
		string _url = "http://www.gamebelief.com:8888";
		//string _url = "http://localhost:8888";
		public string Url { get { return _url; } }


		void OnDestroy()
		{
			_currentForground = null;
			_instance = null;
		}
		
		bool _shouldCancelAll;
		public bool Disabled { set; get; }

		HttpWebRequestInfo _currentForground;
		Queue<HttpWebRequestInfo> _toSendRequest = new Queue<HttpWebRequestInfo>();
		HashSet<HttpWebRequestPoster> _allSendingRequest = new HashSet<HttpWebRequestPoster>();

		public void Send(HttpWebRequestInfo request)
		{
			if (Disabled)
			{
				Debug.LogWarning("http disabled");
				return;
			}

			if(request.RequestType == HttpRequestTypeEnum.Foreground && _currentForground != null)
			{
				Debug.LogError("Only one foreground http request can be sended at the same time." + request.Url);
				return;	
			}
			_toSendRequest.Enqueue(request);
			Debug.Log("start send request " + request.Url);

			if (request.RequestType == HttpRequestTypeEnum.Foreground)
				_currentForground = request;
		}

		internal void Retry(HttpWebRequestInfo request)
		{
			request.RetryTimes++;
			Send(request);
		}

		public void CancelAll()
		{
			_shouldCancelAll = true;
		}

		void Update()
		{
			if (_shouldCancelAll)
			{
				_allSendingRequest.ForEachByEmurator(p =>
				{
					p.Stop();
				});
				_allSendingRequest.Clear();
				_toSendRequest.Clear();

				_currentForground = null;
				_shouldCancelAll = false;

				return;
			}
			if (_toSendRequest.Count > 0)
			{
				var toSendRequest = _toSendRequest.Dequeue();
				var poster = new HttpWebRequestPoster(toSendRequest);
				if (toSendRequest.RequestType == HttpRequestTypeEnum.Foreground)
				{
					_currentForground = toSendRequest;
					//NetWaitPresenter.ShowNetWait();
					NetWaitPanel.Show();
				}
				_allSendingRequest.Add(poster);
				poster.SendRequest();

			}

			_allSendingRequest.ForEachByEmurator(p =>
			{
				if (p.Complete)
				{
					if (p.RequestInfo.RequestType == HttpRequestTypeEnum.Foreground)
					{
						_currentForground = null;
						NetWaitPanel.Hide();
						//NetWaitPresenter.HideNetWait();
					}
					if (p.Response != null && p.Response.StatusCode == 200)
					{
						string customStatusCode = p.Response.GetFirstHeaderValue(StatusCode.KEY_STS);
						if (!string.IsNullOrEmpty(customStatusCode) && customStatusCode == StatusCode.CODE_OK)
						{
							OnSucess(p.RequestInfo, p.Response.DataAsText);
						}
						else
						{
							OnCustomError(p.RequestInfo, customStatusCode);
						}
					}
					else if (p.Response != null)
					{
						OnHttpError(p.RequestInfo, p.Response);
					}
					else
					{
						Debug.LogException(p.Request.Exception);
						p.RequestInfo.OnUnknowError(p.RequestInfo);
					}
				}
			});
			_allSendingRequest.RemoveWhere(p => p.Complete);
		}

		void OnHttpError(HttpWebRequestInfo reqInfo, BestHTTP.HTTPResponse res)
		{
			if (res.StatusCode == 404)
			{
				reqInfo.OnHttp404Error(reqInfo);
			}
			else if (res.StatusCode == 401)
			{
				var customCode = res.GetFirstHeaderValue(StatusCode.KEY_STS);

				if (customCode == ErrCode.D0000S.Code)
				{
					Debug.LogFormat("{0} 401, Need update. {1} . ", reqInfo.Url, customCode);
					reqInfo.OnClientVersionError(reqInfo);
				}
				else if (customCode == ErrCode.G0001S.Code)
				{
					Debug.LogFormat("{0} 401 Server not started {1} . ", reqInfo.Url, customCode);
					reqInfo.OnHttp404Error(reqInfo);
				}
				else
				{
					Debug.LogFormat("{0} 401 {1} . ", reqInfo.Url, customCode);
					reqInfo.OnHttp401Error(reqInfo);
				}
			}
			else
			{
				reqInfo.OnCommonHttpError(reqInfo, res.StatusCode);
				Debug.LogError(res.DataAsText, reqInfo.Caller);
			}
		}
		void OnCustomError(HttpWebRequestInfo reqInfo, string customStatusCode)
		{
			if (string.IsNullOrEmpty(customStatusCode))
			{
				Debug.LogErrorFormat("Request {0} has no custom status code", reqInfo.Url);
			}
			else
			{
				Debug.LogErrorFormat("Request {0} has custom error: {1} ", reqInfo.Url, customStatusCode);

				var codes = customStatusCode.Split(';');
				if (codes.Length > 1)
					reqInfo.OnCustomStatusError(reqInfo, codes[0], codes[1]);
				else
					reqInfo.OnCustomStatusError(reqInfo, codes[0], "");
			}
		}

		void OnSucess(HttpWebRequestInfo request, string jsonData)
		{
			object o;
			try
			{
				if (request.ReturnRawJsonStr)
				{
					o = jsonData;
				}
				else
				{
					o = JsonUtilNet.Deserialize(jsonData, request.ReturnType);
					Debug.LogFormat("receive data from {0} \n {1}", request.Url, jsonData);
				}
            }
            catch (Exception e)
			{
				Debug.LogError("json deserialize failed. data: \n" + jsonData);
				Debug.LogException(e);
				request.OnCustomStatusError(request, "", "服务器返回数据错误");
				return;
            }

			try
			{
				if (Validator.ValidateObject(o))
				{
					request.OnResponse.DynamicInvoke(o);
				}
				else
				{
					request.OnCustomStatusError(request, "", "服务器返回数据错误");
				}
			}
			catch(Exception e)
			{
				Debug.LogException(e);
			}
		}
		
	}
}