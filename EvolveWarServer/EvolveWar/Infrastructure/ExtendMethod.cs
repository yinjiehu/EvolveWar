using IronFury.Data;
using IronFury.Infrastructure.Exceptions;
using Kit.Json;
using Model;
using Network.Code;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace EvolveWar.Infrastructure
{
    public static class ExtendMethod
	{

		static NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

		public static ContentResult JsonOK(this Controller ctrl, object data, bool indent = false)
		{	
			ctrl.Response.AddHeader(StatusCode.KEY_STS, StatusCode.CODE_OK);
			var result = new ContentResult();
			result.Content = JsonUtilNet.Serialize(data);
			_logger.Debug("result.Content" + result.Content);

			return result;
		}

		public static ContentResult Error(this Controller ctrl, CodeMessage err)
		{
			ctrl.Response.AddHeader(StatusCode.KEY_STS, err.ToString());
			return new ContentResult();
		}

		public static ContentResult HttpError(this Controller ctrl, int httpCode)
		{
			ctrl.Response.StatusCode = httpCode;
			return new ContentResult();
		}
		
		public static string GetUID(this Controller ctrl)
		{
            return ctrl.Request.Form["uid"];
            //测试用修改
            //return "10000002";
        }

        public static PlayerStatus GetPlayerStatus(this Controller ctrl)
        {
            var db = GameRedis.GetDB();
			var uid = GetUID(ctrl);
            return PlayerDAO.GetPlayerStatus(uid);
		}

        public static void SetPlayerStatus(this Controller ctrl, PlayerStatus p)
		{
			PlayerDAO.SetPlayerStatus(p);
		}

        public static T GetJsonDataFromRequestStream<T>(this Controller ctrl)
		{
			var stream = ctrl.Request.InputStream;
			var buffer = new byte[stream.Length];
			var readed = stream.Read(buffer, 0, (int)stream.Length);
			var str = System.Text.Encoding.UTF8.GetString(buffer);

			return JsonUtilNet.Deserialize<T>(str);
		}

	}
}