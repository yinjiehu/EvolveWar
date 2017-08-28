using UnityEngine;
using System.Collections;
using Kit.Performance;
using UnityEngine.Events;

namespace Kit.UI
{

	public class BasePanel : MonoBehaviour
	{

		public enum SortingLayer
		{
			BGLayer = 1,
			DefaultLayer = 2,
			MidLayer = 3,
			BarLayer = 4,
			MaskLayer = 5,
			TipLayer = 6,
			SystemLayer = 7,
			TutorialLayer = 8,
			NetworkLayer = 9,
			DebugLayer = 10
		}

		public SortingLayer sortingLayer = SortingLayer.DefaultLayer;

		[SerializeField]
		UnityEvent _showEvent;

		virtual protected void Awake()
		{
			//Debug.Log ("----------Awake-----------" + name);

			Transform parentTran = MainUI.Instance.transform.FindChild(sortingLayer.ToString());
			if (parentTran == null)
			{
				for (int i = (int)SortingLayer.BGLayer; i <= (int)SortingLayer.DebugLayer; i++)
				{
					GameObject go = new GameObject(((SortingLayer)i).ToString(), typeof(RectTransform));
					go.transform.SetParent(MainUI.Instance.transform, false);
					go.layer = MainUI.Instance.gameObject.layer;
					RectTransform rt = go.GetComponent<RectTransform>();
					rt.anchorMin = Vector2.zero;
					rt.anchorMax = Vector2.one;
					rt.sizeDelta = Vector2.zero;
					rt.anchoredPosition3D = Vector3.zero;
					rt.SetSiblingIndex(i);
				}

				parentTran = MainUI.Instance.transform.FindChild(sortingLayer.ToString());
			}
			transform.SetParent(parentTran, false);
		}

		virtual protected void Start()
		{
		}

		virtual public void ShowPanel(object obj)
		{
			transform.SetAsLastSibling();
			gameObject.SetActive(true);
			_showEvent.Invoke();
		}

		virtual public void HidePanel()
		{
			gameObject.SetActive(false);
		}
	}

}