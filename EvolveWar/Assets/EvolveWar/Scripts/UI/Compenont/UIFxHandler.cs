using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Battle
{
	public class UIFxHandler : MonoBehaviour
	{
		[SerializeField]
		float _duration = 1;

		float _elapsedTime;

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

		public void ShowUI(Transform parent, Vector3 position)
		{
			var prefab = Instantiate(gameObject);
			prefab.transform.SetParent(parent);
			prefab.transform.localScale = Vector3.one;
			prefab.GetComponent<RectTransform>().anchoredPosition3D = position;
		}

		public void Show(Transform parent, Vector3 position)
		{
			var prefab = Instantiate(gameObject);
			prefab.transform.SetParent(parent);
			prefab.transform.localScale = Vector3.one;
			var uiPosition = Utility.Utility.WorldToScreenPosition(position);
			prefab.transform.position = uiPosition;

		}
	}
}
