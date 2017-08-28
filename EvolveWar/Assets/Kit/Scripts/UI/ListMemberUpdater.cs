using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections;
using Kit.Inspector;

namespace Kit.UI
{
	public class ListMemberUpdater : MonoBehaviour
	{
		Transform _sample;

		[SerializeField]
		bool _isAsync;
		[SerializeField]
		[HarunaInspect(ShowIf = "_isAsync", Indent = 0f)]
		int _firstShowNum;

		public void OnListUpdate<D>(IEnumerable<D> data, Action<D, GameObject> OnUpdate)
		{
			if (_sample == null)
			{
				InitSample();
			}

			if (this._isAsync)
			{
				if (gameObject.activeInHierarchy && gameObject.activeSelf)
				{
					StopAllCoroutines();
					StartCoroutine(OnUpdateter(transform, data, OnUpdate, this._firstShowNum));
				}
			}
			else
			{
				var count = data.Count();

				if (count < transform.childCount)
				{
					for (var i = 0; i < transform.childCount; i++)
					{
						if (i < count)
						{
							var d = data.ToList()[i];
							var m = transform.GetChild(i).gameObject;
							m.SetActive(true);
							OnUpdate(d, m);
						}
						else
						{
							var c = transform.GetChild(i);
							if (c == _sample)
							{
								c.gameObject.SetActive(false);
							}
							else
							{
								GameObject.Destroy(c.gameObject);
							}
						}
					}
				}
				else
				{
					_sample.gameObject.SetActive(false);

					for (int i = 0; i < transform.childCount; i++)
					{
						var d = data.ToList()[i];
						var m = transform.GetChild(i).gameObject;
						m.SetActive(true);
						OnUpdate(d, m);
					}
					for (var i = transform.childCount; i < count; i++)
					{
						var d = data.ToList()[i];
						var c = GameObject.Instantiate(_sample);
						c.SetParent(transform, false);
						c.localPosition = Vector3.zero;
						c.localScale = Vector3.one;
						OnUpdate(d, c.gameObject);
					}
				}
			}
		}
		

		IEnumerator OnUpdateter<D>(Transform tr, IEnumerable<D> data, System.Action<D, GameObject> OnUpdate, int first_frame_count)
		{
			var count = data.Count();

			if (count < tr.childCount)
			{
				for (var i = 0; i < tr.childCount; i++)
				{
					if (i < count)
					{
						var d = data.ToList()[i];
						var m = tr.GetChild(i).gameObject;
						m.SetActive(true);
						OnUpdate(d, m);
					}
					else
					{
						var c = tr.GetChild(i);
						if (c == _sample)
						{
							c.gameObject.SetActive(false);
						}
						else
						{
							GameObject.Destroy(c.gameObject);
						}
					}
				}
			}
			else
			{
				_sample.gameObject.SetActive(false);

				for (int i = 0; i < tr.childCount; i++)
				{
					var d = data.ToList()[i];
					var m = tr.GetChild(i).gameObject;
					m.SetActive(true);
					OnUpdate(d, m);
				}
				for (var i = tr.childCount; i < count; i++)
				{
					var d = data.ToList()[i];
					if (i < first_frame_count)
					{
						StartCoroutine(InstantiateGameObject(_sample, d, OnUpdate));
						continue;
					}
					yield return StartCoroutine(InstantiateGameObject(_sample, d, OnUpdate));
				}
			}
			yield return null;

		}

		IEnumerator InstantiateGameObject<D>(Transform tr, D d, System.Action<D, GameObject> OnUpdate)
		{
			var c = GameObject.Instantiate(tr);
			c.SetParent(transform, false);
			c.localPosition = Vector3.zero;
			c.localScale = Vector3.one;
			OnUpdate(d, c.gameObject);
			yield return null;
		}

		void InitSample()
		{
			if (transform.childCount != 1)
			{
				//throw new UnityException("initial list chould be only one");
				//for(int i = 1;i < transform.childCount;i++)
				//{
				//	var tr = transform.GetChild(i);
				//	Destroy(tr.gameObject);
				//}
			}

			_sample = transform.GetChild(0);
			_sample.gameObject.SetActive(false);
		}
	}
}