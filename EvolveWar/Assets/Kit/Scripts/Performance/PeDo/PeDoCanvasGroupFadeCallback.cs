using UnityEngine;
using UnityEngine.Events;

namespace Kit.Performance
{
    public class PeDoCanvasGroupFadeCallback : PeNode
    {
        public CanvasGroup _canvasGroup;
        public float _from;
        public float to;

        public float _duration;
        public UnityEvent _callback;

        float _alpha;

        bool _first = true;

        protected override void PerformanceStart()
        {
            base.PerformanceStart();
            _alpha = _canvasGroup.alpha;
            _canvasGroup.alpha = _from;
            _first = true;
        }

        protected override void PerformancUpdate()
        {
            base.PerformancUpdate();

            _canvasGroup.alpha = Mathf.Lerp(_from, to, _elapsedTime / _duration);
            if (_elapsedTime >= _duration / 2 && _first)
            {
                _first = false;
                if (_callback != null)
                {
                    _callback.Invoke();
                }
            }
            
            if (_elapsedTime > _duration)
            {
                PerformanceComplete();
            }
        }

        public override void PerformanceComplete()
        {
            base.PerformanceComplete();
            _canvasGroup.alpha = to;
        }
        public override void ResetToStartStatus()
        {
            base.ResetToStartStatus();
            _canvasGroup.alpha = _alpha;
        }

    }
}
