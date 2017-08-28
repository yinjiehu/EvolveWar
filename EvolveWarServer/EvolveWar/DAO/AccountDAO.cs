using BattleWorld.Data;
using Kit.Json;
using Model;
using StackExchange.Redis;
using System;
using System.Collections.Generic;

namespace EvolveWar
{
	public class AccountDAO
	{
		public const string PREFIX_ACCOUNT = "obj:Account:";

		public const string KEY_INCREMENT_UID = "seq:Uid:Increment";

		public const long BASIC_UID = 1000000;



		public static string CreateUid()
		{
			var db = GameRedis.GetDB();
			var uid = BASIC_UID + db.StringIncrement(KEY_INCREMENT_UID);
			return uid.ToString();
		}

		public static string Login(string guid)
		{

			var key = PREFIX_ACCOUNT + guid;
			var db = GameRedis.GetDB();
			var str = db.StringGet(key);
			string uid = "";
			if (string.IsNullOrEmpty(str))
			{
				uid = CreateUid();
				db.StringSet(key, uid);
			}else
			{
				uid = str;
			}
			return uid;
		}

		static void SetValue<T>(IDatabase db, string key, T obj)
		{
			var str = JsonUtilDB.Serialize(obj);

			db.StringSet(key, str);
		}

		static T GetValue<T>(IDatabase db, string key)
		{
			var str = db.StringGet(key);
			if (string.IsNullOrEmpty(str))
			{
				return default(T);
			}
			return JsonUtilDB.Deserialize<T>(str);
		}
	}
}