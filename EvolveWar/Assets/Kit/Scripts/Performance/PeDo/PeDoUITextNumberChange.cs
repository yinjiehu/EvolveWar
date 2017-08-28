using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

namespace Kit.Performance
{
    public class PeDoUITextNumberChange : PeNode
    {
        Text _text;
        [SerializeField]
        TextTarget _target;

        public Color color;
        public string stringFormat;
        public float from;
        public float to;
        public AnimationCurve _globalSpeedCurve;
        
        public float _duration;

        float _currentValue;

        protected override void PerformanceStart()
        {
            base.PerformanceStart();
            _text = _target.GetTarget(transform);
            _text.color = color;
        }

        protected override void PerformancUpdate()
        {
            base.PerformancUpdate();
            _currentValue = Mathf.Lerp(from, to, _globalSpeedCurve.Evaluate(_elapsedTime / _duration));
            _text.text = string.Format(stringFormat, (int)_currentValue);

            if (_elapsedTime > _duration)
            {
                PerformanceComplete();
            }
        }

        public override void ResetToStartStatus()
        {
            base.ResetToStartStatus();
        }
    }

}