using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using System;
using System.Net;

namespace Network.Http
{
	/// <summary>
	/// 请求类型影响在发生错误时的处理
	/// </summary>
	public enum HttpRequestTypeEnum
	{
		/// <summary>
		/// 前台，由用户行为触发，结果返回前不能进行其他操作
		/// </summary>
		Foreground,
		/// <summary>
		/// 后台的轮询请求。活动剩余时间、修理剩余时间等
		/// </summary>
		Background
	}

	public delegate void OnCommonHttpErrorDelegate(HttpWebRequestInfo request, int httpCode);
	public delegate void OnCustomStatusErrorDelegate(HttpWebRequestInfo request, string code, string msg);

	public class HttpWebRequestInfo
	{
		public int RetryTimes { set; get; }

		public HttpRequestTypeEnum RequestType { set; get; }
		public Type ReturnType { set; get; }
		public bool ReturnRawJsonStr { set; get; }
		public bool ShouldSetToken { set; get; }
		public string Host { set; get; }
		public string Port { set; get;}
		public string UrlParamters { set; get;}
		public object SendData { set; get; }

		public Component Caller { set; get; }
		public Delegate OnResponse { set; get; }
		public Action<HttpWebRequestInfo> OnClientVersionError { set; get; }
		public Action<HttpWebRequestInfo> OnHttp404Error { set; get; }
		public Action<HttpWebRequestInfo> OnHttp401Error { set; get; }

		public Action<HttpWebRequestInfo> OnUnknowError { set; get; }

		public OnCommonHttpErrorDelegate OnCommonHttpError { set; get; }
		public OnCustomStatusErrorDelegate OnCustomStatusError { set; get; }
	}
}