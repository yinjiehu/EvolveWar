using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using WhiteCat.FSM;
using System;
using Data.Player;
using Kit.Event;
using EvolveWar.BattleEvent;
using UI.Main;
using Kit.UI;
using EvolveWar.Battle.Component;
using UI.Battle;
using Kit.Utility;
using Data.Skill;
using EvolveWar;
using System.Linq;

[Serializable]
public class UnitInfo
{
	/// <summary>
	/// 单位ID
	/// </summary>
	public int UnitID;
	/// <summary>
	/// 昵称
	/// </summary>
	public string NickName;

	public string Uid;
	/// <summary>
	/// 账号等级
	/// </summary>
	public int PlayerLevel;
	/// <summary>
	/// 当前经验
	/// </summary>
	public float Exp;
	/// <summary>
	/// 当前等级
	/// </summary>
	public int Level
	{
		get
		{
			int level = Mathf.FloorToInt(Exp / 100f) + 1;
			if (level > 10)
				level = 10;
			return level;
		}
	}

	public int SkillCount
	{
		get
		{
			if (Level >= 9)
				return 5;
			else if (Level >= 7)
				return 4;
			else if (Level >= 5)
				return 3;
			else if (Level >= 3)
				return 2;
			else
				return 1;
		}
	}
	/// <summary>
	/// 战斗总经验
	/// </summary>
	public float TotalExp
	{
		get
		{
			return (Level + 1) * 100f;
		}
	}
	/// <summary>
	/// 攻击范围，固定值
	/// </summary>
	public int Range = 3;
	/// <summary>
	/// 战斗单位
	/// </summary>
	public BattleUnit Unit;
	/// <summary>
	/// 当前生命值
	/// </summary>
	public int Hp;
	/// <summary>
	/// 总生命值
	/// </summary>
	public int TotalHp { get { return Settings.Life; } }

	public UnitInfo(int unitid, string nickname, string uid, int level)
	{
		UnitID = unitid;
		NickName = nickname;
		Uid = uid;
		PlayerLevel = level;
		Exp = 0;
		Hp = TotalHp;
	}

	public void SetExp(int exp)
	{
		Exp = exp;
		Hp = TotalHp;
	}

	public void ResetHp()
	{
		Hp = TotalHp;
	}

	public PlayerSettings Settings
	{
		get
		{
			return Config.PlayerSettings.Get(Level);
		}
	}
}


public class BattleUnit : MonoBehaviour
{
	[SerializeField]
	StateMachine _fsm;
	public StateMachine FSM { get { return _fsm; } }

	[SerializeField]
    List<Ability> _abilityPrefabs = new List<Ability>();

    List<IAbility> _abilities = new List<IAbility>();
	[SerializeField]
	UnitInfo _info;
    [SerializeField]
    FxHandler _attackEffect;
	[SerializeField]
	FxHandler _beAttacked;
	[SerializeField]
	UIFxHandler _bloodEffect;
	public UnitInfo UnitInfo { get { return _info; } }

	Vector3 _moveDirection;

    List<SkillSettings> _holdSkills = new List<SkillSettings>();
	Dictionary<string, GameObject> _holdSkillPrefabs = new Dictionary<string, GameObject>();
    public List<SkillSettings> HoldSkills
    {
        get { return _holdSkills; }
    }

    public void SetMoveDierection(Vector3 direction)
	{
		_moveDirection = direction;
	}

	public Vector3 MoveDirection { get { return _moveDirection; } }


    public void Init(string nickName, string uid, int level, int exp)
    {
        _holdSkills = new List<SkillSettings>();
		_holdSkillPrefabs = new Dictionary<string, GameObject>();
        _info = new UnitInfo(BattleScene.Instance.IncreaseID, nickName, uid, level);
		_info.SetExp(exp);
		_info.Unit = this;
		if(_fsm != null)
		{
			_fsm = Instantiate(_fsm);
			_fsm.transform.SetParent(transform);
			var states = _fsm.GetComponentsInChildren<AIState>();
			foreach(var state in states)
			{
				state.OnInit(this, _fsm);
			}
		}

		var abilities = GetComponentsInChildren<IAbility>(true);
        foreach(var ability in abilities)
        {
            _abilities.Add(ability);
        }

        foreach(var prefab in _abilityPrefabs)
        {
            var ability = Instantiate(prefab);
            ability.transform.SetParent(transform);
            _abilities.Add(ability);
        }

        _abilities.ForEach(a => a.Init(this));

		GlobalEventManager.Instance.RegistEvent<DestroyUnitInfo>(OnDestryUnitEvent);
    }

	void OnDestryUnitEvent(DestroyUnitInfo eventInfo, EventControl eventControl)
	{

	}

	// Use this for initialization
	void Start ()
	{
	}

	IState _tempState;
	
	// Update is called once per frame
	void Update () {
		if (BattleScene.Instance.BattleStatus == BattleStatus.Game)
		{
			foreach (var ability in _abilities)
			{
				ability.OnUpdate(Time.deltaTime);
			}
		}
	}

	public Transform GetRoleArrow()
	{
		return transform.FindChild("AttackEffect");
	}

	public void Attack()
	{

		EffectSoundMgr.Instance.PlayAttackSound();
		var effect = _attackEffect.Show(transform, transform.position);
        float angle = Vector3.Angle(Vector3.up, _moveDirection);
        if (_moveDirection.x > 0)
        {
            angle = -angle;
        }
        Vector3 axis = Vector3.zero;
        effect.transform.localRotation = Quaternion.Euler(0, 0, angle);
        var attackFillter = effect.GetComponentInChildren<AttackColliderFillter>();
        if (attackFillter == null)
        {
            throw new Exception("此单位没有攻击物，单位名字是" + transform.name);
        }
        attackFillter.Init(UnitInfo);
    }

	bool CheckTriggerEffect(out EffectPrefab prefab)
	{
		prefab = null;
		var effects = _holdSkills.FindAll(s => s.ID.Contains("Attack"));
		if(effects != null && effects.Count > 0)
		{
			foreach(var e in effects)
			{
				if(RandomUtility.Random0To100() < e.TriggerProb)
				{
					prefab = BattleScene.Instance.AttackEffects.Find(a => a.SkillID == e.ID);
					return true;
				}
			}
		}
		return false;
	}

	public void BeAttaked(BattleUnit attacker)
	{
		var hurt = attacker.AttackPower - IgnoreAttack - (DefensePower - attacker.IgnoreDefese);
		var damage = hurt;
		EffectPrefab attackEffect;
		if(CheckTriggerEffect(out attackEffect))
		{
			if (attackEffect != null)
			{
				attackEffect.SkillEffect.Show(transform, transform.position);

				damage += Config.SkillSettings.Get(attackEffect.SkillID).Hurt;
			}
		}
		int damageValue = damage > 0 ? damage : 0;
		UnitInfo.Hp -= damageValue;
		if(damageValue > 0)
		{
			_bloodEffect.GetComponent<BloodEffect>().SetText(string.Format("<color=#FF0000>-{0}</color>", damageValue));
			_bloodEffect.Show(MainUI.Instance.canvasGroup.transform, transform.position);
		}
		else
		{
			_bloodEffect.GetComponent<BloodEffect>().SetText("<color=#FF00FF>Miss</color>");
			_bloodEffect.Show(MainUI.Instance.canvasGroup.transform, transform.position);
		}
		if(UnitInfo.Hp <= 0)
		{
			if(UnitInfo.Uid == Save.Player.UID)
			{
				MainUI.Instance.ShowPanel<RevivePanel>();

				EffectSoundMgr.Instance.PlayDeadSound();
			}else if(attacker.UnitInfo.Uid == Save.Player.UID)
			{
				EffectSoundMgr.Instance.PlayKillSound();
			}

			DestroyObject(gameObject);

			GlobalEventManager.Instance.TriggerEvent(new DestroyUnitInfo() { AttackInfo = attacker.UnitInfo, DeadInfo = _info });
		}
		else
		{
			_beAttacked.Show(transform, transform.position);
		}
    }

	public void EatFood(Food food)
	{
		if(food is FoodBlood)
		{
			RecoverBlood(food as FoodBlood);
		}else if(food is FoodBox)
		{
			ObtainBox(food as FoodBox);
		}else if(food is FoodExp)
		{
			IncreaseExp(food as FoodExp);
		}
	}

	void IncreaseExp(FoodExp exp)
	{
		if (_info.Level < 10)
		{
			var tempLevel = _info.Level;
			_info.Exp += exp.Exp;
			if(_info.Level != tempLevel)
			{
				EffectSoundMgr.Instance.PlayBattleLevelUpSound();
				//_info.ResetHp();
				BattleScene.Instance.UpdateProgressLevel(_info.Uid, _info.Level);
			}else if(exp.ExpEnum == FoodExp.FoodExpEnum.Five)
			{
				EffectSoundMgr.Instance.PlayExpSound();
			}
		}
	}

	void RecoverBlood(FoodBlood blood)
	{
		var addHp = (int)(UnitInfo.TotalHp * (blood.Exp / 100f));
		UnitInfo.Hp += addHp;

		EffectSoundMgr.Instance.PlayBloodSound();
		if (addHp > 0)
		{
			_bloodEffect.GetComponent<BloodEffect>().SetText(string.Format("<color=#00FF00>+{0}</color>", addHp));
			_bloodEffect.Show(MainUI.Instance.canvasGroup.transform, transform.position);
		}
		if (UnitInfo.Hp > UnitInfo.TotalHp)
			UnitInfo.Hp = UnitInfo.TotalHp;
	}

	void ObtainBox(FoodBox box)
	{
		//EffectSoundMgr.Instance.PlayBoxSound();
		var index = RandomUtility.Random0ToMax(Config.SkillSettings.DataCollection.Count);
        var skillSettings = Config.SkillSettings.Get(index);
        if (skillSettings == null)
            return;
		var replaceSkillIndex = _holdSkills.FindIndex(h => h.ID == skillSettings.ID);
		if (replaceSkillIndex != -1)
		{
			if(_holdSkills[replaceSkillIndex].Quality < skillSettings.Quality)
			{
				_holdSkills[replaceSkillIndex] = skillSettings;
				EffectSoundMgr.Instance.PlaySkillLevelUpSound();
			}
			else
			{
				EffectSoundMgr.Instance.PlayAddInvalidSound();
			}
		}
		else
		{
			if (_holdSkills.Count < UnitInfo.SkillCount)
			{
				EffectSoundMgr.Instance.PlayAddSkillSound();
				_holdSkills.Add(skillSettings);
				var skillPrefab = BattleScene.Instance.GetPrefabBySkillID(skillSettings.ID);
				if (skillPrefab != null)
				{
					var go = skillPrefab.SkillEffect.Show(transform, transform.position, true);
					if(!_holdSkillPrefabs.ContainsKey(skillSettings.ID))
					{
						_holdSkillPrefabs.Add(skillSettings.ID, go);
					}
				}
			}
			else
			{
				EffectSoundMgr.Instance.PlayAddInvalidSound();
			}
		}
	}

	public void DelelteSkill(string id)
	{
		_holdSkills.RemoveAll(s => s.ID == id);
		if(_holdSkillPrefabs.ContainsKey(id))
		{
			Destroy(_holdSkillPrefabs[id]);
			_holdSkillPrefabs.Remove(id);
		}
	}

	public int AttackPower
	{
		get
		{
			var list = _holdSkills.FindAll(s => s.ID.Contains("Skill")).Select(s => s.Attack);
			if (list.Count() > 0)
				return list.Sum() + UnitInfo.Settings.Attack;
			else
				return UnitInfo.Settings.Attack;
		}
	}

	public int DefensePower
	{
		get
		{
			var list = _holdSkills.FindAll(s => s.ID.Contains("Skill")).Select(s => s.Defense);
			if (list.Count() > 0)
				return list.Sum() + UnitInfo.Settings.Defense;
			else
				return UnitInfo.Settings.Defense;
		}
	}

	public int IgnoreAttack
	{
		get
		{
			var list = _holdSkills.FindAll(s => s.ID.Contains("Skill")).Select(s => s.IgnoreAttack);
			if (list.Count() > 0)
				return list.Sum() + UnitInfo.Settings.IgnoreAttack;
			else
				return UnitInfo.Settings.IgnoreAttack;
		}
	}

	public int IgnoreDefese
	{
		get
		{
			var list = _holdSkills.FindAll(s => s.ID.Contains("Skill")).Select(s => s.IgnoreDefense);
			if(list.Count() > 0)
				return list.Sum() + UnitInfo.Settings.IgnoreDefense;
			else
				return UnitInfo.Settings.IgnoreDefense;
		}
	}

    public Vector2 UnitSize
    {
        get
        {
            var sprite = GetComponent<SpriteRenderer>();
            return sprite.bounds.size;
        }
    }

	void OnDestroy()
	{
		//Debug.Log("destroy unit : " + transform.gameObject.name);
	}

    void OnDrawGizmosSelected()
    {
        DrawGizmos();
    }
    void DrawGizmos()
    {

        Gizmos.color = Color.yellow;
  //      Gizmos.DrawWireCube(transform.position, new Vector3(UnitSize.x, UnitSize.y, 0f));
		//Gizmos.DrawSphere(transform.position, UnitInfo.Range/2f);
    }
}
