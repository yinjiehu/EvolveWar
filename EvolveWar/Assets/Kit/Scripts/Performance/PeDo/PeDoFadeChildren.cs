using UnityEngine;
using System.Collections;
namespace Kit.Performance
{
    public class PeDoFadeChildren : PeNode
    {
        public TransformTarget _transformTarget;
        public float _alpha;

        protected override void PerformanceStart()
        {
            base.PerformanceStart();
            var canvasChildren = _transformTarget.GetTarget(transform).transform.GetComponentsInChildren<CanvasGroup>();
            foreach(var canvasGroup in canvasChildren)
            {
                canvasGroup.alpha = _alpha;
            }
            PerformanceComplete();
        }

        public override void ResetToStartStatus()
        {
            base.ResetToStartStatus();
        }
    }
}
