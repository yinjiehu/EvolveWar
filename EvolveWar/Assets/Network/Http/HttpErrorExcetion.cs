using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using System;

namespace Network.Http
{
	public class HttpErrorExcetion
	{
		public string Pid { private set; get; }
		public int HttpCode { private set; get; }
		public HttpErrorExcetion(int httpCode)
		{
			HttpCode = httpCode;
		}
	}
}