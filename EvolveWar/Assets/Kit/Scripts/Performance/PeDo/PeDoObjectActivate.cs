using UnityEngine;
using System.Collections.Generic;

namespace Kit.Performance
{
	public class PeDoObjectActivate : PeNode
	{
		public TransformTarget _targetGameObject;
		public enum ObjectControlTypeEnum
		{
			active,
			deactive
		}
		public ObjectControlTypeEnum _controlType;

		protected override void PerformanceStart()
		{
			base.PerformanceStart();

			_targetGameObject.GetTarget(transform).gameObject.SetActive(_controlType == ObjectControlTypeEnum.active);
			PerformanceComplete();
		}

		public override void ResetToStartStatus()
		{
			base.ResetToStartStatus();
			
			_targetGameObject.GetTarget(transform).gameObject.SetActive(_controlType != ObjectControlTypeEnum.active);
		}

	}
}