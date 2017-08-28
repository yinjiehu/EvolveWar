using Network.Http.Protocols;
using Shared.Battle;
using Shared.Login;
using Shared.Rank;
using Shared.Talent;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameProtocol : Protocol
{
	[DontCheckToken]
	public static Protocol<string, LoginRes> Login;

	public static Protocol<string, SetNickNameRes> SetNickName;

	public static Protocol<GetRankingListRes> GetRankingList;

	public static Protocol<BattleStartRes> BattleStart;

	public static Protocol<BattleClearReq, BattleClearRes> BattleClear;

	public static Protocol<TalentLevelUpReq, TalentLevelUpRes> TalentLevelUp;

	public static Protocol<TalentResetRes> TalentReset;

	public static Protocol<RechargeRes> Recharge;
}
