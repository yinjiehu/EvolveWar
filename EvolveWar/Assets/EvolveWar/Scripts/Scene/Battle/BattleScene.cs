using UnityEngine;
using System.Collections;
using Kit.UI;
using UI.Battle;
using UI.Main;
using System.Collections.Generic;
using Kit.Event;
using EvolveWar.BattleEvent;
using Models.Battle;
using Kit.Utility;
using System;
using EvolveWar.Battle.Component;
using System.Linq;

public enum BattleStatus
{
	None,
	Game,
	Pause,
	Over
}


[Serializable]
public class EffectPrefab
{
    public string SkillID;

    public FxHandler SkillEffect;
}

public class BattleScene : MonoBehaviour {

	static BattleScene _instance;

	public static BattleScene Instance { get { return _instance; } }

	GenerateFood _foodMgr;
	public GenerateFood FoodMgr { get { return _foodMgr; } }
	[SerializeField]
	BattleUnit _playerPrefab;
	[SerializeField]
	BattleUnit _player;
	public BattleUnit Player { get { return _player; } }
	public List<BattleUnit> _aiPlayers;
	[SerializeField]
	NickNameCollection _nickName;
    [SerializeField]
    List<EffectPrefab> _skillPrefabs;
    [SerializeField]
    List<EffectPrefab> _attackPrefabs;

    BattleStatus _status;

	List<BattlePlayerInfo> _players;

	List<UnitInfo> _allUnits;

	public BattleStatus BattleStatus { get { return _status; } }

	public int TotalSeconds = 10;
	float _elapsedTime = 0;
	public int CountDown { get { return TotalSeconds - (int)_elapsedTime; } }

	int _continueKill = 0;
	float _elapsedContinueTime = 0;
	bool _isContinueKill = false;
	public float ContinueInterval = 3;

    public EffectPrefab GetPrefabBySkillID(string skillID)
    {
        return _skillPrefabs.Find(s => s.SkillID == skillID);
    }


    public List<EffectPrefab> AttackEffects { get { return _attackPrefabs; } }

    List<BattleProgressData> _battleProgress = new List<BattleProgressData>();

	public class BattleProgressData
	{
		public string Uid;
		public int Level;
		public int Kill;
		public int Dead;
		public int Index;
	}

	int _increaseID = 0;
	public int IncreaseID
	{
		get
		{
			++_increaseID;
			return _increaseID;
		}
	}
	public void Pause()
	{
		_status = BattleStatus.Pause;
	}

	public void Resume()
	{
		_status = BattleStatus.Game;
	}

    void Awake()
    {
        _instance = this;
    }

	// Use this for initialization
	void Start () {
		Account.LoadAccount();
		MainUI.Instance.ShowPanel<LoginPanel>();
		GlobalEventManager.Instance.RegistEvent<DestroyUnitInfo>(OnDestoryUnitInfoCallback);
    }

	void OnDestoryUnitInfoCallback(DestroyUnitInfo info, EventControl eventControl)
	{
		AddKillNum(info.AttackInfo.Uid);
		AddDeadNum(info.DeadInfo.Uid);
	}

	BattlePlayerInfo GetBattlePlayerByUid(string uid)
	{
		for(int i = 0;i < _players.Count;i++)
		{
			var p = _players[i];
			if (p != null && p.Uid == uid)
				return p;
		}
		return null;
	}

	bool CheckUnit(out string uid)
	{
		uid = "";
		for (int i = _allUnits.Count - 1; i >= 0; --i)
		{
			var unit = _allUnits[i];
			if (unit.Unit == null)
			{
				uid = unit.Uid;
				_allUnits.RemoveAt(i);
				return true;
			}
		}
		return false;
	}

	BattleProgressData GetProgressDataByUid(string uid)
	{
		foreach(var info in _battleProgress)
		{
			if (info.Uid == uid)
				return info;
		}
		return null;
	}

	public void UpdateProgressLevel(string uid, int level)
	{
		for(int i = 0;i < _battleProgress.Count;i++)
		{
			var progress = _battleProgress[i];
			if (progress != null && progress.Uid == uid)
				progress.Level = level;
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(_status == BattleStatus.Game)
		{
			string uid = "";
			if(CheckUnit(out uid))
			{
				var battlePlayer = GetBattlePlayerByUid(uid);
				if(battlePlayer != null)
				{
					var unitInfo = GetProgressDataByUid(uid);
					if(unitInfo != null)
					{
						GenerateAI(_aiPlayers[RandomUtility.Random0ToMax(_aiPlayers.Count)], battlePlayer.NickName, battlePlayer.Uid, battlePlayer.Level, unitInfo.Level * 100);
					}					
					else
					{
						GenerateAI(_aiPlayers[RandomUtility.Random0ToMax(_aiPlayers.Count)], battlePlayer.NickName, battlePlayer.Uid, battlePlayer.Level, 0);
					}

				}
			}
			_elapsedTime += Time.deltaTime;
			if(_elapsedTime >= TotalSeconds)
			{
				GameOver();
			}
			if (IsKillComplte())
			{
				GameOver();
			}

			_elapsedContinueTime += Time.deltaTime;
			if (_elapsedContinueTime > ContinueInterval)
			{
				_elapsedContinueTime = 0;
				_isContinueKill = false;
				_continueKill = 0;
			}
		}
	}

	public bool IsKillComplte()
	{
		int index = _battleProgress.FindIndex(p => p.Kill >= 30);
		return index != -1;
	}

	public bool IsWin()
	{
		var kill = _battleProgress.Max(p=>p.Kill);
		var data = _battleProgress.Find(p => p.Kill == kill);
		return data.Uid == Save.Player.UID;
	}

	public void BeginBattle(List<BattlePlayerInfo> players)
	{
		_players = players;
		_increaseID = 0;
		_continueKill = 0;
		_isContinueKill = false;
		_allUnits = new List<UnitInfo>();
		MainUI.Instance.ShowPanel<BattlePanel>();
		_battleProgress = new List<BattleProgressData>();
		GeneratePlayer();
		_battleProgress.Add(new BattleProgressData() { Uid = _player.UnitInfo.Uid });

		for(int i = 0;i < players.Count;i++)
		{
			var player = players[i];
			Debug.Log("AI:" + player.Uid);
			GenerateAI(_aiPlayers[i], player.NickName, player.Uid, player.Level, 0);

			_battleProgress.Add(new BattleProgressData() { Uid = player.Uid, Index = (10 - i) });
		}

		_foodMgr = GetComponent<GenerateFood>();
		FoodMgr.GenerateFoods();
		_status = BattleStatus.Game;
		_elapsedTime = 0;
	}

	public void GeneratePlayer()
	{
		var unitInfo = GetProgressDataByUid(Save.Player.UID);
		if(unitInfo != null)
		{
			_player = GenerateUnit(_playerPrefab, Save.Player.NickName, Save.Player.UID, (int)Save.Player.Level, unitInfo.Level * 100);
			_allUnits.Add(_player.UnitInfo);
		}
		else
		{
			_player = GenerateUnit(_playerPrefab, Save.Player.NickName, Save.Player.UID, (int)Save.Player.Level, 0);
			_allUnits.Add(_player.UnitInfo);
		}

	}

	public void EndBattle()
	{
		MainUI.Instance.HidePanel<BattlePanel>();
		foreach(var unit in _allUnits)
		{
			if(unit.Unit != null)
			{
				DestroyObject(unit.Unit.gameObject);
			}
		}
		_foodMgr.RemoveAllFoods();
	}

	public void GameOver()
	{
		_status = BattleStatus.Over;
		MainUI.Instance.ShowPanel<BattleResultPanel>(IsWin());
	}

	public int GetKillNumByUid(string uid)
	{
		var progress = _battleProgress.Find(b => b.Uid == uid);
		if (progress == null)
			return 0;
		return progress.Kill;
	}

	public int GetRankByUid(string uid)
	{
		_battleProgress.Sort(SortOn);
		_battleProgress.Reverse();
		var index = _battleProgress.FindIndex(b => b.Uid == uid);
		if (index < 0)
			return 0;
		return index + 1;
	}

    public bool IsFirstKill()
    {
        foreach(var battleP in _battleProgress)
        {
            if (battleP.Kill > 0)
                return false;
        }
        return true;
    }
    
	int SortOn(BattleProgressData data1, BattleProgressData data2)
	{
		return (data1.Kill * 100000 - data2.Kill * 100000) + (data1.Dead * 100 - data2.Dead*100) + (data1.Index - data2.Index);
	}

	public int GetDeadNumByUid(string uid)
	{
		var progress = _battleProgress.Find(b => b.Uid == uid);
		if (progress == null)
			return 0;
		return progress.Dead;
	}


	public void AddKillNum(string uid)
	{
		
		var progressData = _battleProgress.Find(b=>b.Uid == uid);
		progressData.Kill += 1;
		if (uid == Save.Player.UID)
		{
			//if(_continueKill == 0)
			//{
			//	_isContinueKill = true;
			//	_elapsedContinueTime = 0;
			//}
			//if(_isContinueKill)
			//{
			//	_continueKill += 1;
			//             if(IsFirstKill())//首次击杀
			//             {

			//             }
			//}
			MainUI.Instance.GetPanel<BattlePanel>().PlayKillEffect(progressData.Kill);
		}
	}

	public void AddDeadNum(string uid)
	{
		var progressData = _battleProgress.Find(b => b.Uid == uid);
		progressData.Dead += 1;
	}

    public BattleUnit GenerateUnit(BattleUnit prefab, string nickname, string uid, int level, int exp)
    {
        var unit = Instantiate(prefab);
        unit.transform.position = Vector3.zero;
        unit.transform.localRotation = Quaternion.identity;
        unit.transform.localScale = Vector3.one;
        unit.Init(nickname, uid, level, exp);
		unit.gameObject.SetActive(true);
        return unit;
    }

	public BattleUnit GenerateAI(BattleUnit prefab, string nickname, string uid, int level, int exp)
	{
		var camera = FindObjectOfType<FollowCamera>();
		var positionX = UnityEngine.Random.Range(-camera.MapSize.x / 2f, camera.MapSize.x / 2f);
		var positionY = UnityEngine.Random.Range(-camera.MapSize.y / 2f, camera.MapSize.y / 2f);
		var unit = Instantiate(prefab);
		unit.transform.position = new Vector2(positionX, positionY);
		unit.transform.localRotation = Quaternion.identity;
		unit.transform.localScale = Vector3.one;
		unit.Init(nickname, uid, level, exp);
		_allUnits.Add(unit.UnitInfo);
		unit.gameObject.SetActive(true);
		unit.gameObject.name = uid;
		return unit;
	}

	public string GetRandomNickName()
	{
		return _nickName.GetRandomName();
	}
	
	public BattleUnit GetInRangeUnitByPosition(BattleUnit selfUnit, int range)
	{
		foreach(var unit in _allUnits)
		{
			if (unit.Uid == selfUnit.UnitInfo.Uid)
				continue;
			if(Vector3.Distance(selfUnit.transform.position, unit.Unit.transform.position) < range)
			{
				return unit.Unit;
			}
		}
		return null;
	}

	public BattleUnit GetNearestUnit(string uid, Vector3 position)
	{
		float dis = float.MaxValue;
		int index = 0;
		for (int i = 0;i < _allUnits.Count;i++)
		{
			var unit = _allUnits[i];
			if (unit.Uid == uid)
				continue;
			var distance = Vector3.Distance(unit.Unit.transform.position, position);
			if (distance < dis)
			{
				dis = distance;
				index = i;
			}
		}
		return _allUnits[index].Unit;

	}
}
