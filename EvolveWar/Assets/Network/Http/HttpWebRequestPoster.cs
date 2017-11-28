using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using System.Threading;
using System;
using System.Collections;
using System.IO;
using BestHTTP;

namespace Network.Http
{
	public class HttpWebRequestPoster
	{
		HttpWebRequestInfo _reqInfo;
		public HttpWebRequestInfo RequestInfo { get { return _reqInfo; } }

		HTTPRequest _request;
		public HTTPRequest Request { get { return _request; } }
		HTTPResponse _response;
		public HTTPResponse Response { get { return _response; } }

		public bool Complete { private set; get; }

		public HttpWebRequestPoster(HttpWebRequestInfo reqInfo)
		{
			_reqInfo = reqInfo;
		}

		public void SendRequest()
		{
			string url = string.Empty;
			try
			{
				var uploadsValues = new Dictionary<string, string>();
				uploadsValues.Add("version", "1.0.0");

				if (_reqInfo.ShouldSetToken)
				{
					uploadsValues.Add("uid", Save.Player.UID);
					//uploadsValues.Add("token", Save.Account.Token);
     //               uploadsValues.Add("serverid", string.IsNullOrEmpty(Save.CurrentServerId) ? "" : Save.CurrentServerId);
                }


				if (_reqInfo.SendData != null)
				{
					Type sendType = _reqInfo.SendData.GetType();

					if (sendType.IsPrimitive || sendType.IsEnum || sendType == typeof(string))
					{
						url += "/" + _reqInfo.SendData.ToString();
					}
					else
					{
						SetObjectPropertyValueToForm(uploadsValues, sendType, _reqInfo.SendData);
					}
				}
				Debug.Log("Host:" + _reqInfo.Host);

				var address = PayMgr.Instance.GetIPv6(_reqInfo.Host, _reqInfo.Port);
				Debug.Log("address:" + address);
				var addresses = Dns.GetHostAddresses(_reqInfo.Host);
				if(addresses[0].AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
				{
					//var requestUrl = "http://["+addresses[0] + "]:" + _reqInfo.Port + _reqInfo.UrlParamters + url;

					var requestUrl = "http://"+_reqInfo.Host + ":" + _reqInfo.Port + _reqInfo.UrlParamters + url;
					Debug.Log("IPv6:" + requestUrl + "   address:" + addresses[0]);
					_request = new HTTPRequest(new Uri(requestUrl), HTTPMethods.Post, OnRequestFinished);
				}else{
					var requestUrl = "http://"+_reqInfo.Host + ":" + _reqInfo.Port + _reqInfo.UrlParamters + url;
					Debug.Log("IPv4:" + requestUrl);
					_request = new HTTPRequest(new Uri(requestUrl), HTTPMethods.Post, OnRequestFinished);
				}

				_request.Priority = _reqInfo.RequestType == HttpRequestTypeEnum.Foreground ? 1 : 0;

				using (var itr = uploadsValues.GetEnumerator())
				{
					while (itr.MoveNext())
					{
						_request.AddField(itr.Current.Key, itr.Current.Value);
					}
				}
				
				_request.UseAlternateSSL = false;
				_request.Send();
			}
			catch (Exception e)
			{
				Complete = true;
                Debug.LogException(e);
			}
		}
		void OnRequestFinished(HTTPRequest originalRequest, HTTPResponse response)
		{
			_response = response;
			Complete = true;
		}

		public void Stop()
		{
			if (_request != null)
				_request.Abort();
		}
		
		void SetObjectPropertyValueToForm(Dictionary<string, string> form, Type objType, object obj, string fieldName = "")
		{
			if (objType.IsPrimitive || objType == typeof(Guid))
			{
				form.Add(fieldName, obj.ToString());
			}
			else if (objType.IsEnum)
			{
				form.Add(fieldName, obj.ToString());
			}
			else if (objType == typeof(string))
			{
				form.Add(fieldName, (string)obj);
			}
			else if (objType.GetInterface(typeof(IEnumerable).FullName) != null)
			{
				if (obj != null)
				{
					var temp = (IEnumerable)obj;

					var itr = temp.GetEnumerator();
					int index = 0;
					while (itr.MoveNext())
					{
						SetObjectPropertyValueToForm(form, itr.Current.GetType(), itr.Current, fieldName + "[" + index + "]");
						index++;
					}
				}
			}
			else
			{
				var properties = objType.GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
				for (int i = 0; i < properties.Length; i++)
				{
					var p = properties[i];

					var jsonIgnore = p.GetCustomAttributes(typeof(JsonIgnoreAttribute), true);
					if (jsonIgnore.Length != 0)
						continue;

					var v = p.GetValue(obj, null);

					var childFieldName = string.IsNullOrEmpty(fieldName) ? p.Name : fieldName + "." + p.Name;
					if (v != null)
					{
						SetObjectPropertyValueToForm(form, p.PropertyType, v, childFieldName);
					}
				}
			}
		}
	}
}