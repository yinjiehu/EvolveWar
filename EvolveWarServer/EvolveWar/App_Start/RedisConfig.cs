using System;

namespace EvolveWar
{
    public class RedisConfig
	{
		static NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

		public static void Initialize()
		{
			GameRedis.Connect("localhost", 6379, "", 6);
			_logger.Debug("Config reids initialize");
		}
	}
}