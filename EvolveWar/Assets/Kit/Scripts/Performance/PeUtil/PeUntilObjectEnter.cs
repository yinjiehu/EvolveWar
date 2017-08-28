using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using Kit.Performance;

namespace  FullMoon.Battle
{
	public class PeUntilObjectEnter : PeNode
	{
		[SerializeField]
		TransformTarget _target;

		[SerializeField]
		BoxCollider _collider;

		protected override void PerformancUpdate()
		{
			base.PerformancUpdate();

			var traget = _target.GetTarget(transform);
			if (_collider.bounds.Contains(traget.position))
			{
				PerformanceComplete();
			}
		}
	}
}