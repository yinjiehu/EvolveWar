using System;
using UnityEngine;

namespace EvolveWar.Battle.Component
{
	public class FxHandler : MonoBehaviour
	{
		[SerializeField]
		float _duration = 1;

		float _elapsedTime = 0;
		void Start()
		{
			_elapsedTime = 0;
		}

		void Update()
		{
			_elapsedTime += Time.deltaTime;
			if(_elapsedTime > _duration)
			{
				Destroy(gameObject);
			}
		}

		public GameObject Show(Transform parent, Vector3 position, bool isLoop=false)
		{
			var prefab = Instantiate(gameObject);
			prefab.transform.SetParent(parent);
			prefab.transform.position = position;
            if (isLoop)
                _duration = float.MaxValue;

            return prefab;
		}

		public GameObject Show(Vector3 position)
		{
			var prefab = Instantiate(gameObject);
			prefab.transform.position = position;
            return prefab;
		}
	}
}
