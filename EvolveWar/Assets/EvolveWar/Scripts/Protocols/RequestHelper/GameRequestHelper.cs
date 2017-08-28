using System;
using System.Collections.Generic;
using EvolveWar.Data;
using Network.Code;
using Shared.Rank;
using Models.Battle;
using Kit.Utility;
using UI.Main;
using Shared.Battle;
using Shared.Talent;
using Kit.UI;
using UnityEngine;

namespace EvolveWar.RequestHelper
{
	public static class GameRequestHelper
	{
		public static void Login(string playerID, Action callback)
		{
			if (Temp.Online) {
				GameProtocol.Login.Send (playerID, res => {
					if (res.Result == ReturnCode.OK)
					{
						SDKManager.Instance.SetAccount();
						Account.PlayerID = res.AccountID;
						Account.SaveAccount ();
						Save.SetPlayer (res.Player);
						callback ();
					}
				});
			} else {
				Account.PlayerID = playerID;
				Account.SaveAccount ();

				callback ();
			}
		}

		public static void SetNickName(string nickname, Action callback)
		{
			if (Temp.Online) {
				GameProtocol.SetNickName.Send (nickname, res => {
					if (res.Result == ReturnCode.OK) {
						Save.SetPlayer (res.Player);
						callback ();
					}
				});
			} else {
				Save.Player.SetNickName (nickname);

				callback ();
			}
		}

		public static void GetRankingList(Action<List<RankingInfo>> callback)
		{
			if (Temp.Online) {
				GameProtocol.GetRankingList.Send (res => {
					if (res.Result == ReturnCode.OK) {
						callback (res.RankingList);
					}
				});
			} else {
				callback (new List<RankingInfo> ());
			}
		}

		public static void BattleStart(Action<List<BattlePlayerInfo>> callback)
		{
			if (Temp.Online) {
				GameProtocol.BattleStart.Send (res => {
					if (res.Result == ReturnCode.OK) {
						var players = new List<BattlePlayerInfo> ();
						for (int i = 0; i < 9; i++) {
							players.Add (new BattlePlayerInfo () {
								NickName = BattleScene.Instance.GetRandomNickName (),
								Uid = Guid.NewGuid ().ToString (),
								Level = RandomUtility.Random0ToMax (10)
							});
						}
						callback (players);
					} else {
						DialogPanel.ShowDialogMessage (res.Result.Message);
					}
				});
			} else {
				var players = new List<BattlePlayerInfo>();
				for(int i = 0;i < 9;i++)
				{
					players.Add(new BattlePlayerInfo()
						{
							NickName = BattleScene.Instance.GetRandomNickName(),
							Uid = Guid.NewGuid().ToString(),
							Level = RandomUtility.Random0ToMax(10)
						});
				}
				callback(players);
			}
		}

		public static void BattleClear(bool victory, Action callback)
		{
			if (Temp.Online) {
				var req = new BattleClearReq ();
				req.TempID = Guid.NewGuid().ToString();
				req.Uid = Save.Player.UID;
				req.Rank = BattleScene.Instance.GetRankByUid(Save.Player.UID);
				req.Kill = BattleScene.Instance.GetKillNumByUid (Save.Player.UID);
				req.Dead = BattleScene.Instance.GetDeadNumByUid (Save.Player.UID);
				GameProtocol.BattleClear.Send (req, res => {
					if (res.Result == ReturnCode.OK)
					{
						Save.SetPlayer(res.Player);
						callback ();
					}
				});
			} else {
				var exp = 30f * (victory ? 1 : 0.5f) + (22 - 2 * BattleScene.Instance.GetRankByUid(Save.Player.UID) + 1.5f * BattleScene.Instance.GetKillNumByUid(Save.Player.UID));

				Save.Player.AddExp((int)exp);
				callback();
			}
		}

		public static void TalentLevelUp(string id)
		{
			if(Temp.Online)
			{
				var req = new TalentLevelUpReq();
				req.TalentID = id;
				GameProtocol.TalentLevelUp.Send(req, res =>
				{
					if(res.Result == ReturnCode.OK)
					{
						Save.SetPlayer(res.Player);
						MainUI.Instance.ShowPanel<TalentPanel>();
					}
				});
			}
			else
			{
				Save.Player.Talent.LevelUp(id);
				MainUI.Instance.ShowPanel<TalentPanel>();
			}
		}

		public static void TalentReset()
		{
			if (Temp.Online)
			{
				GameProtocol.TalentReset.Send(res =>
				{
					if (res.Result == ReturnCode.OK)
					{
						Save.SetPlayer(res.Player);
						MainUI.Instance.ShowPanel<TalentPanel>();
					}
				});
			}
			else
			{
				Save.Player.Talent.TalentReset();
				MainUI.Instance.ShowPanel<TalentPanel>();
			}
		}


		public static void Recharge()
		{
			if (Temp.Online)
			{
				string rechargeID = string.Empty;
#if UNITY_IOS
				rechargeID = "Recharge_IOS_"+Guid.NewGuid().ToString();
				TDGAVirtualCurrency.OnChargeRequest(rechargeID, "礼包", 6, "CNY", 6, "IOS");
#else
				rechargeID = "Recharge_Android_" + Guid.NewGuid().ToString();
				TDGAVirtualCurrency.OnChargeRequest(rechargeID, "礼包", 6, "CNY", 6, "Android");
#endif
				GameProtocol.Recharge.Send(res =>
				{
					if (res.Result == ReturnCode.OK)
					{
						Save.SetPlayer(res.Player);
						PlayerPrefs.SetInt("GiftValue", 1);
						TDGAVirtualCurrency.OnChargeSuccess(rechargeID);
						MainUI.Instance.ShowPanel<RechargeSuccessPanel>();
					}
				});
			}
			else
			{
				Save.Player.Recharge();
				MainUI.Instance.ShowPanel<RechargeSuccessPanel>();
			}
		}

	}
}

