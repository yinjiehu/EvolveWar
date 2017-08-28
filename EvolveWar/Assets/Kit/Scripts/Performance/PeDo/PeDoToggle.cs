using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Kit.Performance
{
    public class PeDoToggle : PeNode
    {

        public TransformTarget _targetGameObject;

        public bool value;

        protected override void PerformanceStart()
        {
            base.PerformanceStart();

            _targetGameObject.GetTarget(transform).GetComponent<Toggle>().isOn = value;
            PerformanceComplete();
        }

        public override void ResetToStartStatus()
        {
            base.ResetToStartStatus();

            _targetGameObject.GetTarget(transform).GetComponent<Toggle>().isOn = !value;
        }
    }
}
