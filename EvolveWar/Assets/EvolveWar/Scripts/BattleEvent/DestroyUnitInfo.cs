using UnityEngine;
using System.Collections;
using Kit.UI;
using UI.Main;
using UnityEngine.UI;
using WhiteCat.Tween;
using Kit.Event;

namespace EvolveWar.BattleEvent
{
	public class DestroyUnitInfo : GlobalEventInfo
	{
		public UnitInfo AttackInfo;
		public UnitInfo DeadInfo;
	}
}
