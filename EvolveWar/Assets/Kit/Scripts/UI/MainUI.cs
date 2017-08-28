using UnityEngine;
using System.Collections.Generic;
namespace Kit.UI
{

	public class MainUI : MonoBehaviour
	{
		static MainUI instance;

		static public MainUI Instance
		{
			get { return instance; }
		}

		public string[] _paths;

		public List<BasePanel> _prefabs = new List<BasePanel>();

		CanvasGroup _canvasGroup;

		public CanvasGroup canvasGroup
		{
			get
			{
				if (_canvasGroup == null)
				{
					_canvasGroup = GetComponent<CanvasGroup>();
					if (_canvasGroup == null)
						_canvasGroup = gameObject.AddComponent<CanvasGroup>();
				}

				return _canvasGroup;
			}
		}

		public static List<BasePanel> panels = new List<BasePanel>();

		void Awake()
		{
			instance = this;

			BasePanel[] ps = GetComponentsInChildren<BasePanel>(true);
			panels.Clear();
			for (int i = 0; i < ps.Length; i++)
			{
				panels.Add(ps[i]);
			}
		}

		List<BasePanel> _closePanels = new List<BasePanel>();

		public void AddPopPanel<T>() where T : BasePanel
		{
			BasePanel panel = GetPanel<T>();
			if (panel != null)
			{
				_closePanels.Add(panel);
			}
		}

		public void RemovePopPanel<T>() where T : BasePanel
		{
			string name = typeof(T).Name;
			for (int i = 0; i < _closePanels.Count; i++)
			{
				if (_closePanels[i] != null && _closePanels[i].name == name)
				{
					_closePanels.Remove(_closePanels[i]);
				}
			}
		}

		void OnDestroy()
		{
			instance = null;
		}

		public BasePanel GetPanel(string panelName)
		{
			for (int i = 0; i < panels.Count; i++)
			{
				if (panelName == panels[i].name)
				{
					return panels[i];
				}
			}

			for (int i = 0; i < panels.Count; i++)
			{
				if (panelName == panels[i].GetType().Name)
				{
					return panels[i];
				}
			}
			return null;
		}

		public bool IsPanelLoaded<T>() where T : BasePanel
		{
			return GetPanel(typeof(T).Name) != null;
		}

		public T GetPanel<T>() where T : BasePanel
		{
			BasePanel panel = GetPanel(typeof(T).Name);

			if (panel != null)
			{
				return panel as T;
			}
			else
			{
				string panelName = typeof(T).Name;
				BasePanel bp = LoadPanel(panelName);
				if (bp != null)
				{
					return bp as T;
				}
			}

			return null;
		}

		BasePanel LoadPanel(string panelName)
		{
			GameObject obj = GetGameObjectByPanelName(panelName);
			if (obj == null)
				return null;

			Debug.Log("load ui panel " + obj.name);
			GameObject go = GameObject.Instantiate(obj) as GameObject;
			go.name = panelName;
			BasePanel bp = go.GetComponent<BasePanel>();
			if (bp == null)
				go.AddComponent<BasePanel>();

			panels.Add(bp);

			return bp;
		}

		public GameObject GetGameObjectByPanelName(string panelName)
		{
			foreach (var go in _prefabs)
			{
				if (go != null)
				{
					if (go.name == panelName)
						return go.gameObject;
				}
			}
			return null;
		}

		public T ShowPanel<T>(object obj=null) where T : BasePanel
		{
			T t = GetPanel<T>();
			if (t != null)
			{
				t.ShowPanel(obj);
			}
			else
			{
				string panelName = typeof(T).Name;
				BasePanel bp = LoadPanel(panelName);
				if (bp != null)
				{
					bp.ShowPanel(obj);
					return bp as T;
				}
			}
			return t;
		}

		BasePanel InstantiateObj(string panelName)
		{
			GameObject obj = GetGameObjectByPanelName(panelName);
			Debug.Log("load ui panel " + obj.name);
			if (obj == null)
				return null;

			GameObject go = GameObject.Instantiate(obj) as GameObject;
			go.name = panelName;
			BasePanel bp = go.GetComponent<BasePanel>();
			if (bp == null)
				go.AddComponent<BasePanel>();

			return bp;
		}

		public T HidePanel<T>() where T : BasePanel
		{
			T t = GetPanel<T>();
			if (t != null)
			{
				t.HidePanel();
			}
			return t;
		}

		public BasePanel ShowPanel(string panelName)
		{
			BasePanel panel = GetPanel(panelName);
			if (panel != null)
			{
				panel.ShowPanel(null);
			}
			else
			{
				panel = LoadPanel(panelName);
				panel.ShowPanel(null);
			}
			return panel;
		}
		public void InitPanel()
		{
			_prefabs = new List<BasePanel>();
		}

		public void AddPanel(BasePanel panel)
		{
			if (GetGameObjectByPanelName(panel.name) == null)
			{
				_prefabs.Add(panel);
			}
		}
	}

}