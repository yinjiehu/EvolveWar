using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;

namespace BattleWorld.Data
{
	public static partial class DataUtil
	{
		static Random _random = new Random();

		public static float Random0To1()
		{
			return (float)_random.NextDouble();
		}

		public static int Random0To100()
		{
			return _random.Next(100);
		}

		public static int Random0ToMax(int max)
		{
			return _random.Next(max);
		}

		public static int RandomMinToMax(int min, int max)
		{
			return _random.Next(min, max);
		}

		public static float RandomMinToMax(float min, float max)
		{
			return min + (float)_random.NextDouble() * (max - min);
		}

		public static int RandomPlusMinus()
		{
			var t = _random.Next(2);
			if (t == 0)
			{
				return -1;
			}
			return 1;
		}

		public static bool RandomTrueFalse()
		{
			return _random.Next(2) == 0;
		}

		public static T RandomListMember<T>(this IList<T> list)
		{
			if (list.Count == 0)
			{
				return default(T);
			}
			return list[Random0ToMax(list.Count)];
		}

		public static void ForeachEnumerator<T>(this IEnumerable<T> list, Action<T> action)
		{
			using (var itr = list.GetEnumerator())
			{
				while (itr.MoveNext())
				{
					action(itr.Current);
				}
			}
		}

		public static float GetSlp(float level)
		{
			return (float)Math.Log(level + 4, 5);
		}

		public static DateTime GetTodayDateLocal(DateTime utcActulaDate)
		{
			var localDate = utcActulaDate.ToLocalTime();
			if (localDate.Hour < 4)
			{
				return localDate.Date.AddDays(-1);
			}

			return localDate.Date;
		}

		public static DateTime GetDayStartTimeInUtc(DateTime utcActualDate)
		{
			var localDate = utcActualDate.ToLocalTime();
			if (localDate.Hour < 4)
			{
				return localDate.Date.AddDays(-1);
			}

			localDate = localDate.Date.AddHours(4);
			return localDate.ToUniversalTime();
		}

		public static bool IsUtcTimeToday(DateTime targetUtcTime, DateTime utcNow)
		{
			var today = GetDayStartTimeInUtc(utcNow);
			return targetUtcTime >= today && targetUtcTime < utcNow.AddDays(1);
		}

		public static void ForEachEnum<T>(Action<string, T> callback)
		{
			var names = Enum.GetNames(typeof(T));
			foreach (var name in names)
			{
				var type = (T)Enum.Parse(typeof(T), name);
				callback(name, type);
			}
		}

		public static string CalcFileMD5(string filePath)
		{
			MD5CryptoServiceProvider md5Generator = new MD5CryptoServiceProvider();
			FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
			byte[] hash = md5Generator.ComputeHash(file);
			var strMD5 = BitConverter.ToString(hash).Replace("-", "").ToLower();

			file.Dispose();
			file.Close();

			return strMD5;
		}
		public static string CalcMD5(byte[] bytes)
		{
			MemoryStream stream = new MemoryStream(bytes);
			MD5CryptoServiceProvider md5Generator = new MD5CryptoServiceProvider();
			byte[] hash = md5Generator.ComputeHash(stream);
			var strMD5 = BitConverter.ToString(hash).Replace("-", "").ToLower();

			stream.Dispose();
			stream.Close();

			return strMD5;
		}


        //public static byte[] GZipCompressString(string rawString)
        //{
        //    if (string.IsNullOrEmpty(rawString) || rawString.Length == 0)
        //    {
        //        return new byte[0];
        //    }
        //    else
        //    {
        //        byte[] rawData = System.Text.Encoding.UTF8.GetBytes(rawString.ToString());

        //        using (MemoryStream ms = new MemoryStream())
        //        {
        //            using (GZipStream compressedzipStream = new GZipStream(ms, CompressionMode.Compress, true))
        //            {
        //                compressedzipStream.Write(rawData, 0, rawData.Length);
        //                compressedzipStream.Close();
        //                return ms.ToArray();
        //            }
        //        }
        //    }
        //}
        //public static string GZipDecompressString(byte[] zippedData)
        //{
        //    using (MemoryStream ms = new MemoryStream(zippedData))
        //    {
        //        using (MemoryStream outBuffer = new MemoryStream())
        //        {
        //            GZipStream compressedzipStream = new GZipStream(ms, CompressionMode.Decompress);
        //            byte[] block = new byte[1024];
        //            while (true)
        //            {
        //                int bytesRead = compressedzipStream.Read(block, 0, block.Length);
        //                if (bytesRead <= 0)
        //                    break;
        //                else
        //                    outBuffer.Write(block, 0, bytesRead);
        //            }
        //            compressedzipStream.Close();

        //            return System.Text.Encoding.UTF8.GetString(outBuffer.ToArray());
        //        }
        //    }
        //}
        public static void AnalysisValueByShift(long value, out long arg1, out long arg2, out long arg3)
		{
			arg1 = (value >> 16) & 0xFF;
			arg2 = (value >> 8) & 0xFF;
			arg3 = value & 0xFF;
		}

		public static long MergeValueByShift(long arg1, long arg2, long arg3)
		{
			return (arg1 << 16) | (arg2 << 8) | arg3;
		}
	}
}