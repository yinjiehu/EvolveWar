using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using System;
using System.Collections.Generic;

namespace Network.Utillity
{
    public static class NetworkUtil
    {
		public static void ForEachByEmurator<T>(this IEnumerable<T> list, System.Action<T> action)
		{
			using (var itr = list.GetEnumerator())
			{
				while (itr.MoveNext())
				{
					action(itr.Current);
				}
			}
		}
		public static void ForEachByEmurator<T>(this IEnumerable<T> list, System.Action<int, T> action)
		{
			int i = 0;
			using (var itr = list.GetEnumerator())
			{
				while (itr.MoveNext())
				{
					action(i, itr.Current);
					i++;
				}
			}
		}
	}
}