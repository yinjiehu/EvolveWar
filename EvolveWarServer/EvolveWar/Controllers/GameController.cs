using EvolveWar.Infrastructure;
using Shared.Battle;
using Shared.Login;
using Shared.Rank;
using Shared.Talent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EvolveWar.Controllers
{
	public class GameController : Controller
	{
		public ActionResult Login(string id)
		{
			var db = GameRedis.GetDB();
			var uid = AccountDAO.Login(id);
			
			var res = new LoginRes();
			res.Player = PlayerDAO.Login(uid);
			res.AccountID = id;
			return this.JsonOK(res);
		}

		public ActionResult SetNickName(string id)
		{
			var res = new SetNickNameRes();
			var player = this.GetPlayerStatus();
			player.SetNickName(id);
			this.SetPlayerStatus(player);
			res.Player = player;
			return this.JsonOK(res);
		}

		public ActionResult GetRankingList()
		{
			var res = new GetRankingListRes();
			var db = GameRedis.GetDB();
			res.RankingList = new List<RankingInfo>();
			var rankingList = RankingDAO.GetScoreRankingRange(db, -50,  - 1);
			foreach(var uid in rankingList)
			{
				var player = PlayerDAO.GetPlayerStatus(uid);
				var scores = RankingDAO.GetScoreFromRanking(db, uid);
				var info = new RankingInfo();
				info.DisplayName = player.NickName;
				info.Level = (int)player.Level;
				info.Kill = scores[0];
				info.Dead = scores[1];
				res.RankingList.Add(info);
			}
			return this.JsonOK(res);
		}

		public ActionResult BattleStart()
		{
			var res = new BattleStartRes();
			return this.JsonOK(res);
		}

		public ActionResult BattleClear(BattleClearReq req)
		{
			var res = new BattleClearRes();
			var db = GameRedis.GetDB();
			RankingDAO.UpdateScoreToRanking(db, req.Uid, req.Kill, req.Dead);
			var playerStatus = PlayerDAO.GetPlayerStatus(req.Uid);
			var exp = 30f * (req.Result ? 1 : 0.5f) + (22 - 2 * req.Rank + 1.5f * req.Kill);
			playerStatus.AddExp((int)exp);
			PlayerDAO.SetPlayerStatus(playerStatus);
			res.Player = playerStatus;
			return this.JsonOK(res);
		}

		//public static Protocol<TalentLevelUpReq, TalentLevelUpRes> TalentLevelUp;
		public ActionResult TalentLevelUp(TalentLevelUpReq req)
		{
			var uid = this.GetUID();
			var playerStatus = PlayerDAO.GetPlayerStatus(uid);
			playerStatus.Talent.LevelUp(req.TalentID);
			PlayerDAO.SetPlayerStatus(playerStatus);
			return this.JsonOK(new TalentLevelUpRes() { Player = playerStatus });
		}

		public ActionResult TalentReset()
		{
			var uid = this.GetUID();
			var playerStatus = PlayerDAO.GetPlayerStatus(uid);
			playerStatus.Talent.TalentReset();
			PlayerDAO.SetPlayerStatus(playerStatus);
			return this.JsonOK(new TalentResetRes() { Player = playerStatus });
		}

		public ActionResult Recharge()
		{
			var uid = this.GetUID();
			var playerStatus = PlayerDAO.GetPlayerStatus(uid);
			playerStatus.Recharge();
			PlayerDAO.SetPlayerStatus(playerStatus);

			return this.JsonOK(new RechargeRes() { Player = playerStatus });
		}
	}
}