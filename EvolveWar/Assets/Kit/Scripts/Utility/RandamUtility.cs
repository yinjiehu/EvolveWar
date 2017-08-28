using System;
using System.Collections.Generic;

namespace Kit.Utility
{
	public static class RandomUtility
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
	}
}
