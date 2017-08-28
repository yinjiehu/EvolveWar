using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Kit.UI;
using UI.Main;
using UnityEngine.UI;
using WhiteCat.Tween;
using Kit.Event;
using EvolveWar.BattleEvent;
using Data.Skill;
using System.Collections.Generic;
using Kit.Utility;
using System;

namespace UI.Battle
{
	[Serializable]
	public class KillEffectPrefab
	{
		public int Kill;
		public UIFxHandler fxEffect;
	}
	public class BattlePanel : BasePanel
	{
		[SerializeField]
		ScrollCircle _scrollCircle;
		[SerializeField]
		Text _nickName;
		[SerializeField]
		Text _killTxt;
		[SerializeField]
		Text _deadTxt;
		[SerializeField]
		Text _rankTxt;
		[SerializeField]
		Text _countDownTxt;

		[SerializeField]
		Transform _hpBarContainer;
		[SerializeField]
		HpBar _hpBar;
        [SerializeField]
        ListMemberUpdater _updater;

		[SerializeField]
		Text _attackTxt;
		[SerializeField]
		Text _defenseTxt;
		[SerializeField]
		Text _liftTxt;
		[SerializeField]
		Text _ignoreAttackTxt;
		[SerializeField]
		Text _ignoreDefenseTxt;
		[SerializeField]
		Transform _killEffectContianer;
		[SerializeField]
		List<KillEffectPrefab> _killPrefabs;


		public ScrollCircle Joystick
		{
			get
			{
				return _scrollCircle;
			}
		}

        public override void ShowPanel(object obj)
		{
			base.ShowPanel(obj);


			BGSoundPlayer.Instance.PlayBattleSound();

			_nickName.text = Save.Player.NickName;
			var settings = Save.Player.Settings;
			_attackTxt.text = settings.Attack.ToString();
			_defenseTxt.text = settings.Defense.ToString();
			_liftTxt.text = settings.Life.ToString();
			_ignoreAttackTxt.text = settings.IgnoreAttack.ToString();
			_ignoreDefenseTxt.text = settings.IgnoreDefense.ToString();
			GlobalEventManager.Instance.RegistEvent<DestroyUnitInfo>(OnDestroyUnitEvent);
		}

		public Vector3 MoveDirection
		{
			get
			{
				return _scrollCircle.Direction;
			}
		}

		void OnDestroyUnitEvent(DestroyUnitInfo info, EventControl eventControl)
		{
		}

		void Update()
		{
			_killTxt.text = BattleScene.Instance.GetKillNumByUid(Save.Player.UID).ToString();
			_deadTxt.text = BattleScene.Instance.GetDeadNumByUid(Save.Player.UID).ToString();
			_rankTxt.text = BattleScene.Instance.GetRankByUid(Save.Player.UID).ToString();
			var countDown = BattleScene.Instance.CountDown;
			if(countDown < 60)
			{
				_countDownTxt.text = string.Format("{0:D2}:{1:D2}", Mathf.FloorToInt(countDown / 60f), countDown % 60);
				_countDownTxt.GetComponent<Tweener>().enabled = countDown != 0;
			}
			else
			{
				_countDownTxt.text = string.Format("{0:D2}:{1:D2}", Mathf.FloorToInt(countDown / 60f), countDown % 60);
			}
			if(BattleScene.Instance.Player != null)
            {
                var skills = new List<SkillSettings>();
                for(int i= 0;i < 5;i++)
                {
                    if(i < BattleScene.Instance.Player.HoldSkills.Count)
                    {
                        skills.Add(BattleScene.Instance.Player.HoldSkills[i]);
                    }
                    else
                    {
                        skills.Add(null);
                    }
                }
				int n = 0;
                _updater.OnListUpdate(skills, delegate (SkillSettings settings, GameObject block)
                {
                    block.GetComponent<SkillBlock>().UpdateData(settings, n);
					n++;
                });
            }
		}

		public void GenerateHpBar(Transform hpPos, UnitInfo unit)
		{
			var hpBar = Instantiate(_hpBar);
			hpBar.transform.SetParent(_hpBarContainer);
			hpBar.transform.localRotation = Quaternion.identity;
			hpBar.transform.localScale = Vector3.one;
			hpBar.GenerateBar(hpPos, unit);

			var position = Utility.Utility.WorldToScreenPosition(hpPos.position);
			hpBar.transform.position = position;
		}

		public void OnReturnClick()
		{
			BattleScene.Instance.Pause();
			MainUI.Instance.ShowPanel<BattleQuitPanel>();
		}

		public void OnBattleClick()
		{
            BattleScene.Instance.Player.Attack();
        }

		public override void HidePanel()
		{
			base.HidePanel();
			foreach(Transform tr in _hpBarContainer)
			{
				DestroyObject(tr.gameObject);
			}
		}

		public void PlayKillEffect(int kill)
		{
			var prefab = _killPrefabs.Find(k => k.Kill == kill);
			if(prefab != null)
			{
				prefab.fxEffect.ShowUI(_killEffectContianer, Vector3.zero);
			}
		}
	}
}
