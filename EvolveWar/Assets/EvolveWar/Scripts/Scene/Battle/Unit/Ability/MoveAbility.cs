using UnityEngine;
using System.Collections;
using System;
using Kit.Utility;
using Kit.UI;
using UI.Battle;

public class MoveAbility : Ability
{
    [SerializeField]
    float _speed = 3f;

    ScrollCircle _joystick;
    FollowCamera _followCamera;
	Vector3 _lastDirection;
	float _lastSpeed;

	Transform _unitArrow;

    public override void Init(BattleUnit unit)
    {
        base.Init(unit);
		_joystick = MainUI.Instance.GetPanel<BattlePanel>().Joystick;
		_unitArrow = unit.GetRoleArrow();
        _followCamera = GameObject.FindObjectOfType<FollowCamera>();
    }

    public override void OnUpdate(float deltaTime)
    {
        var direction = _joystick.Direction;
		

        if(direction != Vector3.zero)
        {
			_unit.SetMoveDierection(direction);
			var deltaPosition = direction * deltaTime * _speed;
			var movePosition = _unit.transform.position + new Vector3(deltaPosition.x, deltaPosition.y, 0);
			Vector3 targetDirection = Vector3.zero;

			float width = (_followCamera.MapSize.x - _unit.UnitSize.x) / 2f;
            float height = (_followCamera.MapSize.y - _unit.UnitSize.y) / 2f;

            if (movePosition.x > -width && movePosition.x < width)
            {
                targetDirection.x = direction.x;
            }
            else
            {
                targetDirection.x = 0;
            }
            if (movePosition.y > -height && movePosition.y < height)
            {
                targetDirection.y = direction.y;
            }
            else
            {
                targetDirection.y = 0;
            }
            targetDirection.Normalize();
            _unit.transform.Translate(targetDirection * deltaTime * _speed);
			_lastDirection = targetDirection;
			_lastSpeed = _speed;
			if(_unitArrow != null)
			{
				//_unitArrow.gameObject.SetActive(true);
				float angle = Vector3.Angle(Vector3.up, _lastDirection);
				if(_lastDirection.x > 0)
				{
					angle = -angle;
				}
				Vector3 axis = Vector3.zero;
				_unitArrow.localRotation = Quaternion.Euler(0, 0, angle);
			}
		}
		else if(_lastSpeed > 0)
		{
			var deltaPosition = _lastDirection * deltaTime * _speed;
			var movePosition = _unit.transform.position + new Vector3(deltaPosition.x, deltaPosition.y, 0);
			float width = (_followCamera.MapSize.x - _unit.UnitSize.x) / 2f;
			float height = (_followCamera.MapSize.y - _unit.UnitSize.y) / 2f;

			Vector3 targetDirection = Vector3.zero;
			if (movePosition.x > -width && movePosition.x < width)
			{
				targetDirection.x = _lastDirection.x;
			}
			else
			{
				targetDirection.x = 0;
			}
			if (movePosition.y > -height && movePosition.y < height)
			{
				targetDirection.y = _lastDirection.y;
			}
			else
			{
				targetDirection.y = 0;
			}
			_unit.transform.Translate(targetDirection * deltaTime * _lastSpeed);
			_lastSpeed -= Time.deltaTime;
			//if (_unitArrow != null)
			//{
			//	_unitArrow.gameObject.SetActive(false);
			//}
		}
		else
		{
			//if (_unitArrow != null)
			//{
			//	_unitArrow.gameObject.SetActive(false);
			//}
		}
		var ray = new Ray(_unit.transform.position, direction);
		RaycastHit hit;
		if(Physics.Raycast(ray, out hit))
		{
			Debug.Log("Hit Name:" + hit.transform.gameObject.name);
		}

		Debug.DrawRay(_unit.transform.position, direction, Color.yellow);
    }

}
