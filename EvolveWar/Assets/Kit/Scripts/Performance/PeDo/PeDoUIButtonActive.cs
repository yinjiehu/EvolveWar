using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Kit.Performance
{
    public class PeDoUIButtonActive : PeNode
    {
        public Button _targetButton;
		public enum ObjectControlTypeEnum
		{
			active,
			deactive
		}
		public ObjectControlTypeEnum _controlType;

		protected override void PerformanceStart()
		{
			base.PerformanceStart();

			_targetButton.enabled = _controlType == ObjectControlTypeEnum.active;
			PerformanceComplete();
		}
    }
}