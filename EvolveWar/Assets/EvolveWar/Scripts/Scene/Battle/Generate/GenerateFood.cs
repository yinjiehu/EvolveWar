using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GenerateFood : MonoBehaviour
{
	[SerializeField]
	List<Food> _foodExpPrefabs;
	[SerializeField]
	List<Food> _foodBloodPrefabs;
	[SerializeField]
	List<Food> _foodBoxPrefabs;

	Vector2 _mapSize;
	List<Food> _foods;
	// Use this for initialization
	void Start ()
	{
		_mapSize = FindObjectOfType<FollowCamera>().MapSize;
		
	}
	
	// Update is called once per frame
	void Update () {
		if(BattleScene.Instance.BattleStatus == BattleStatus.Game)
		{
			if (_foods.Count < 300)
			{
				GenerateFoodUnit();
			}
		}
		
	}

	public void GenerateFoods()
	{
		_foods = new List<Food>();
		for (int i = 0; i < 300; i++)
		{
			GenerateFoodUnit();
		}
	}

	public void GenerateFoodUnit()
	{
		var prefabs = new List<Food>();
		prefabs.AddRange(_foodExpPrefabs);
		var random = Random.Range(0, 100);
		if (random > 70)
		{
			if(random > 90)
			{
				prefabs.AddRange(_foodBloodPrefabs);
				prefabs.AddRange(_foodBoxPrefabs);
			}else{
				prefabs.AddRange(_foodBloodPrefabs);
			}
		}

		int index = Random.Range(0, prefabs.Count);
		var food = Instantiate(prefabs[index]);
		food.Init();
		var point = GetPoint();
		food.transform.position = new Vector3(point.x, point.y, 0);
		_foods.Add(food);
	}

	Vector2 GetPoint()
	{
		var point = new Vector2(0, 0);
		while(true)
		{
			point.x = Random.Range(-_mapSize.x / 2.2f, _mapSize.x / 2.2f);
			point.y = Random.Range(-_mapSize.y / 2.2f, _mapSize.y / 2.2f);

			var colliders = Physics2D.OverlapCircleAll(point, 0.2f);
			if (colliders.Length == 0)
			{
				break;
			}
		}
		return point;
	}

	public void RemoveAllFoods()
	{
		foreach(var food in _foods)
		{
			DestroyObject(food.gameObject);
		}
	}

	public void RemoveFood(Food food)
	{
		if(_foods.Contains(food))
			_foods.Remove(food);

		DestroyObject(food.gameObject);
	}

	public Food GetNearestFood(BattleUnit unit)
	{
		int n = 0;
		float minDis = int.MaxValue;
		for(int i = 0;i < _foods.Count;i++)
		{
			var food = _foods[i];
			if (food == null)
				continue;
			var distance = Vector3.Distance(food.transform.position, unit.transform.position);
			if (distance < minDis)
			{
				minDis = distance;
				n = i;
			}
			
		}

		return _foods[n];
	}

	public Food GetRandomFood()
	{
		var index = Random.Range(0, _foods.Count);
		return _foods[index];
	}

	public bool IsContains(Food food)
	{
		return _foods.Contains(food);
	}
}
