using UnityEngine;
using System;
using System.Collections.Generic;

namespace Kit.Performance
{
	public class PeDoAnimatorControl : PeNode
	{
		[SerializeField]
		AnimatorTarget _animator;

		[SerializeField]
		List<AnimatorVariableControl> _controls;

		protected override void PerformanceStart()
		{
			base.PerformanceStart();

			var itr = _controls.GetEnumerator();
			while (itr.MoveNext())
			{
				switch (itr.Current.VariableType)
				{
					case AnimatorVariableControl.VariableTypeEnum.Bool:
						_animator.GetTarget(transform).SetBool(itr.Current.VariableName, itr.Current.ValueBool);
						break;
					case AnimatorVariableControl.VariableTypeEnum.Float:
						_animator.GetTarget(transform).SetFloat(itr.Current.VariableName, itr.Current.ValueFloat);
						break;
					case AnimatorVariableControl.VariableTypeEnum.Trigger:
						_animator.GetTarget(transform).SetTrigger(itr.Current.VariableName);
						break;
				}
			}

			PerformanceComplete();
		}


		[Serializable]
		public class AnimatorVariableControl
		{
			public enum VariableTypeEnum
			{
				Bool,
				Float,
				Trigger
			}
			[SerializeField]
			VariableTypeEnum _varialbleType;
			public VariableTypeEnum VariableType { get { return _varialbleType; } }

			[SerializeField]
			protected string _variableName;
			public string VariableName { get { return _variableName; } }

			////[FullInspector.InspectorShowIf("VariableTypeIsBool")]
			protected bool _valueBool;
			public bool ValueBool { get { return _valueBool; } }
			////[FullInspector.InspectorShowIf("VariableTypeIsFloat")]
			protected float _valueFloat;
			public float ValueFloat { get { return _valueFloat; } }

			bool VariableTypeIsBool()
			{
				return _varialbleType == VariableTypeEnum.Bool;
			}
			bool VariableTypeIsFloat()
			{
				return _varialbleType == VariableTypeEnum.Float;
			}
		}
	}
}