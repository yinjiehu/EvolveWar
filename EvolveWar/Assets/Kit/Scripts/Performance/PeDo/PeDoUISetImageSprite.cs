using UnityEngine;
using UnityEngine.UI;

namespace Kit.Performance
{
    public class PeDoUISetImageSprite : PeNode
    {
        public TransformTarget _targetGameObject;
        public Sprite _defualtSprite;

        protected override void PerformanceStart()
        {
            base.PerformanceStart();

            _targetGameObject.GetTarget(transform).GetComponent<Image>().sprite = _defualtSprite;
            PerformanceComplete();
        }


        public override void ResetToStartStatus()
        {
            base.ResetToStartStatus();
        }
    }
}
