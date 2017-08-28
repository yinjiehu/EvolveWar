using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Kit.Event;
using EvolveWar.BattleEvent;

namespace UI.Battle
{

	public class HpBar : MonoBehaviour
	{
		[SerializeField]
		Text _nameTxt;
		[SerializeField]
		Text _levelTxt;
		[SerializeField]
		Image _levelSlider;
		[SerializeField]
		Slider _expSlider;
		[SerializeField]
		Image _hpColor;

		Transform _hpPos;
		UnitInfo _unitInfo;

		EventBindingInfo _eventBindingInfo;
		// Use this for initialization
		void Start()
		{
			_eventBindingInfo = GlobalEventManager.Instance.RegistEvent<DestroyUnitInfo>(OnDestroyUnitEvent);
		}

		void OnDestroy()
		{
			GlobalEventManager.Instance.UnregistEvent(_eventBindingInfo);
		}

		void OnDestroyUnitEvent(DestroyUnitInfo info, EventControl eventControl)
		{
			if(_unitInfo.UnitID == info.DeadInfo.UnitID)
			{
				DestroyObject(gameObject);
			}
		}


		// Update is called once per frame
		void Update()
		{
			if(_hpPos != null)
			{
				var position = Utility.Utility.WorldToScreenPosition(_hpPos.position);
				transform.position = Vector3.Lerp(transform.position, position, 1);
			}

			if(_unitInfo != null)
			{
				_levelTxt.text = _unitInfo.Level.ToString();
				_expSlider.value = _unitInfo.Hp / (_unitInfo.TotalHp * 1f);
				_levelSlider.fillAmount = (_unitInfo.Exp%100) / 100f;
				if(_expSlider.value > 0.66f)
				{
					_hpColor.color = new Color(0, 1, 0, 1);
				}
				else if (_expSlider.value > 0.33f)
				{
					_hpColor.color = new Color(1, 1, 0, 1);
				}
				else
				{
					_hpColor.color = new Color(1, 0, 0, 1);
				}
			}
			
		}

		public void GenerateBar(Transform hpPos, UnitInfo unitInfo)
		{
			_hpPos = hpPos;
			_unitInfo = unitInfo;
			_nameTxt.text = _unitInfo.NickName;
			
		}
	}

}
