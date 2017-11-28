using System;
using System.Net;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Network.Http.Protocols
{
	public abstract class Protocol
	{
		public string SubUrl { protected set; get; }
		public ConnectTargetEnum ConnectTarget { protected set; get; }
		public bool CheckToken { set; get;}
		public Type ReturnType { protected set; get; }

		public virtual void Init(ConnectTargetEnum connectTarget, string subUrl, bool checkToken)
		{
			ConnectTarget = connectTarget;
			SubUrl = subUrl;
			CheckToken = checkToken;
		}

        protected HttpWebRequestInfo CreateRequest()
		{
			if (SubUrl == null || SubUrl == "")
			{
				throw new UnityEngine.UnityException("ProtocolManager has not been initialized!");
			}
			var host = HttpClient.Instance.Host;
			var req = new HttpWebRequestInfo();
			req.ShouldSetToken = CheckToken;
			req.Host = host;
			req.Port = HttpClient.Instance.Port;
			req.UrlParamters = "/" + SubUrl;
			req.ReturnType = ReturnType;
			
			req.OnClientVersionError = OnClientVersionError;
			req.OnHttp404Error = OnHttp404Error;
			req.OnHttp401Error = OnHttp401Error;
			req.OnCommonHttpError = OnCommonHttpError;
			req.OnCustomStatusError = OnCustomStatusError;
			req.OnUnknowError = OnUnkownError;
			return req;
		}

		protected virtual void OnClientVersionError(HttpWebRequestInfo httpRequest)
		{
			HttpClient.Instance.CancelAll();
			//DialogPresenter.ShowMessageBox("客户端版本需要更新！", () =>
   //         {
   //             if (BGMManager.Get() != null)
   //                 GameObject.DestroyImmediate(BGMManager.Get().gameObject);

   //             AssetBundleManager.Instance.UnloadAll();
   //             SceneManager.LoadScene("Download");
   //         });
		}
		protected virtual void OnHttp401Error(HttpWebRequestInfo httpRequest)
		{
			if (httpRequest.RequestType == HttpRequestTypeEnum.Foreground)
            {
                HttpClient.Instance.CancelAll();
				//DialogPresenter.ShowMessageBox("登陆过期，请重新登陆", () =>
    //            {
    //                if (BGMManager.Get() != null)
    //                    GameObject.DestroyImmediate(BGMManager.Get().gameObject);

    //                AssetBundleManager.Instance.UnloadAll();
    //                SceneManager.LoadScene("Download");
    //            });
			}
		}
		protected virtual void OnHttp404Error(HttpWebRequestInfo httpRequest)
		{
			if (httpRequest.RequestType == HttpRequestTypeEnum.Foreground)
			{
				//DialogPresenter.ShowRetryDialog("\n无法连接服务器\n请检查网络环境", () =>
				//{
				//	HttpClient.Instance.Retry(httpRequest);
				//});
			}
		}
		protected virtual void OnCommonHttpError(HttpWebRequestInfo httpRequest, int httpCode)
		{
			if (httpRequest.RequestType == HttpRequestTypeEnum.Foreground)
			{
				//DialogPresenter.ShowRetryDialog("连接服务器发生错误 H" + httpCode, () =>
				//{
				//	HttpClient.Instance.Retry(httpRequest);
				//});
			}
		}
		protected virtual void OnCustomStatusError(HttpWebRequestInfo httpRequest, string code, string msg)
		{
			if (httpRequest.RequestType == HttpRequestTypeEnum.Foreground)
			{
				//DialogPresenter.ShowRetryDialog("连接服务器发生错误 " + code, () =>
				//{
				//	HttpClient.Instance.Retry(httpRequest);
				//});
			}
		}
		protected virtual void OnUnkownError(HttpWebRequestInfo httpRequest)
		{
			if (httpRequest.RequestType == HttpRequestTypeEnum.Foreground)
			{
				//DialogPresenter.ShowRetryDialog("连接服务器发生未知错误", () =>
				//{
				//	HttpClient.Instance.Retry(httpRequest);
				//});
			}
		}
	}

    public class Protocol<R> : Protocol
    {
		public Protocol()
        {
            ReturnType = typeof(R);
        }

        public HttpWebRequestInfo Send(Action<R> onResponseCallback)
		{
			var req = CreateRequest();
			req.OnResponse = onResponseCallback;
			req.RequestType = HttpRequestTypeEnum.Foreground;

			HttpClient.Instance.Send(req);

			return req;
		}

		public HttpWebRequestInfo BackgroundSend(Component caller, Action<R> onResponseCallback)
		{
			var req = CreateRequest();
			req.Caller = caller;
			req.OnResponse = onResponseCallback;
			req.RequestType = HttpRequestTypeEnum.Background;

			HttpClient.Instance.Send(req);

			return req;
		}
    }

	public class Protocol<Q, R> : Protocol
	{
		public Protocol()
		{
			ReturnType = typeof(R);
		}

		public HttpWebRequestInfo Send(Q sendData, Action<R> onResponseCallback)
		{
			var req = CreateRequest();
			req.SendData = sendData;
			req.OnResponse = onResponseCallback;
			req.RequestType = HttpRequestTypeEnum.Foreground;

			HttpClient.Instance.Send(req);

			return req;
		}

		public HttpWebRequestInfo BackgroundSend(Q sendData, UnityEngine.Component caller, Action<R> onResponseCallback)
		{
			var req = CreateRequest();
			req.SendData = sendData;
			req.Caller = caller;
			req.OnResponse = onResponseCallback;
			req.RequestType = HttpRequestTypeEnum.Background;

			HttpClient.Instance.Send(req);

			return req;
        }

    }
}