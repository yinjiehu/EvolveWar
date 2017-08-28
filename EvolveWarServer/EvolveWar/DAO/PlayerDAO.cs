using BattleWorld.Data;
using Kit.Json;
using Model;
using StackExchange.Redis;
using System;
using System.Collections.Generic;

namespace EvolveWar
{
	public class PlayerDAO
	{
		public const string PREFIX_PLAYER = "obj:Player";

		public static PlayerStatus Login(string uid)
		{
			var player = GetPlayerStatus(uid);
			if(player == null)
			{
				player = CreatePlayer(uid);
			}
			return player;
		}

		public static PlayerStatus CreatePlayer(string uid)
		{
			var player = new PlayerStatus();
			player.Initialize(Config.TalentSettings);
			player.SetUID(uid);
			SetPlayerStatus(player);
			return player;
		}


		public static PlayerStatus GetPlayerStatus(string uid)
		{
			var key = PREFIX_PLAYER + uid;
			var db = GameRedis.GetDB();
			return GetValue<PlayerStatus>(db, key);
		}

		public static void SetPlayerStatus(PlayerStatus player)
		{
			var key = PREFIX_PLAYER + player.UID;
			var db = GameRedis.GetDB();
			SetValue(db, key, player);
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