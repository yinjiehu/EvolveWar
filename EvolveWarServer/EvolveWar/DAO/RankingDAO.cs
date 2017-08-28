using BattleWorld.Data;
using Model;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EvolveWar
{
	public class RankingDAO
	{
		public const string KEY_RANKING_SCORE = "zset:Ranking:Score";

		public static long GetRankFromScoreRanking(IDatabase db, string uid)
		{
			var rank = db.SortedSetRank(KEY_RANKING_SCORE, uid);
			if (rank == null)
				return -1;

			return (long)rank + 1;
		}

		/// <summary>
		/// long[]{ 击杀, 被杀 }
		/// </summary>
		/// <param name="db"></param>
		/// <param name="uid"></param>
		/// <returns></returns>
		public static long[] GetScoreFromRanking(IDatabase db, string uid)
		{
			var score = db.SortedSetScore(KEY_RANKING_SCORE, uid);
			if (score == null || score == 0)
				return new long[] { 0, 0 };

			long kill = ((long)score >> 16) & 0xFF;
			long dead = (long)score & 0xFF;
			return new long[] { kill, dead };
		}

		public static void SetScoreToRanking(IDatabase db, string uid, long kill, long dead)
		{
			long score = kill << 16 | dead;
			db.SortedSetAdd(KEY_RANKING_SCORE, uid, score);
		}

		public static List<string> GetScoreRankingRange(IDatabase db, int min, int max)
		{
			var uids = db.SortedSetRangeByRank(KEY_RANKING_SCORE, min, max);

			return uids.Select(u => (string)u).ToList();
		}

		public static void UpdateScoreToRanking(IDatabase db, string uid, long kill, long dead)
		{
			var scores = GetScoreFromRanking(db, uid);
			long dbKill = scores[0] + kill;
			long dbDead = scores[1] + dead;

			SetScoreToRanking(db, uid, dbKill, dbDead);
		}
	}
}