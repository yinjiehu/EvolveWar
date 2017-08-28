using Data.Skill;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Battle
{
    public class SkillBlock : MonoBehaviour
    {
        [SerializeField]
        Image _icon;
		[SerializeField]
		Image _lock;
		[SerializeField]
		Image _bg;
		[SerializeField]
		Color _green;
		[SerializeField]
		Color _blue;
		[SerializeField]
		Color _yellow;
		[SerializeField]
		Color _gray;

		[SerializeField]
		GameObject _deleteBtn;
		public SkillSettings Settings;

        public void UpdateData(SkillSettings settings, int index)
        {
            Settings = settings;
			if (settings == null)
			{
				_icon.enabled = false;
				_bg.color = _gray;
			}	
			else
			{
				if(settings.Quality == 1)
				{
					_bg.color = _green;
				}
				else if(settings.Quality == 2)
				{
					_bg.color = _blue;
				}else if(settings.Quality == 3)
				{
					_bg.color = _yellow;
				}
				_icon.sprite = settings.Icon;
				_icon.enabled = true;
				
			}
            
			if(index < BattleScene.Instance.Player.UnitInfo.SkillCount)
			{
				_lock.gameObject.SetActive(false);
			}
			else
			{
				_lock.gameObject.SetActive(true);
				_bg.color = _gray;
			}
        }

		public void OnClick()
		{
			if(Settings != null)
			{
				_deleteBtn.SetActive(!_deleteBtn.activeSelf);
			}
		}

		public void OnDeleteSkillClick()
		{
			if (Settings != null)
			{
				BattleScene.Instance.Player.DelelteSkill(Settings.ID);
				OnClick();
			}
		}
    }
}