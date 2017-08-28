using StackExchange.Redis;
using System;
using System.Collections.Generic;

namespace EvolveWar
{
	public class GameRedis
	{
		static ConfigurationOptions _connectOptions;
		static object _locker = new object();
		static ConnectionMultiplexer _connection;
		static int _db = 0;

		static ConnectionMultiplexer GetConnection()
		{
			lock (_locker)
			{
				if (_connection == null || !_connection.IsConnected)
				{
					_connection = ConnectionMultiplexer.Connect(_connectOptions);
				}
			}
			return _connection;
		}

		public static void Connect(string host, int port, string auth, int db = 0)
		{
			_db = db;
			_connectOptions = new ConfigurationOptions();
			_connectOptions.EndPoints.Add(host, port);
			_connectOptions.Password = auth;
			if (_connection != null && _connection.IsConnected)
				_connection.Close();
			_connection = ConnectionMultiplexer.Connect(_connectOptions);
		}

		public static IDatabase GetDB()
		{
			var connection = GetConnection();
			return connection.GetDatabase(_db);
		}
		static IServer GetServer()
		{
			var connection = GetConnection();
			return connection.GetServer(connection.GetEndPoints()[0]);
		}
		public static IEnumerable<RedisKey> Keys(string pattern)
		{
			return GetServer().Keys(_db, pattern);
		}
	}
}