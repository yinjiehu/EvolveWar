using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;
using System.Reflection;

public class Joystick : MonoBehaviour
{
    public enum Aligns
    {
        Left = -1, Right = 1
    }
    public Aligns align;
    [Range(0, 3)]
    public float padSize;
    public Vector2 margins = new Vector2(3, 3);
    public Vector2 touchZoneSize = new Vector2(3, 3);
    public float dragDistance = 1;
    public bool snapsToFinger = true;
    public bool hideOnRelease = false;
    public bool touchPad;
    public bool showJoystick;

    GameObject stick;
    GameObject stickBase;
    Camera joystickCamera;
    bool touching;
    bool touchPlatform;
    Touch currentTouch;
    int currentFingerId;
    Rect touchZoneRect;
    Vector2 currentAxisValue;
    Vector3 previousPosition;
    //public Vector2 Direction { get { return (_image.rectTransform.anchoredPosition - _origin).normalized; } }

    void Awake()
    {
    }
    // Use this for initialization
    void Start()
    {
        touchPlatform = checkTouchPlatform();
        joystickCamera = transform.parent.GetComponent<Camera>();
        stick = transform.FindChild("stick").GetComponent<Transform>().gameObject;
        stickBase = transform.FindChild("base").GetComponent<Transform>().gameObject;
        setJoystickPosition();
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
		KeyBoardUpdate();
#else
		TouchUpdate();
#endif
	}

	void KeyBoardUpdate()
	{
		float axisX = 0;
		float axisY = 0;
		if(Input.GetKey(KeyCode.W))
		{
			axisY = 1;
		}
		if (Input.GetKey(KeyCode.S))
		{
			axisY = -1;
		}
		if (Input.GetKey(KeyCode.A))
		{
			axisX = -1;
		}
		if (Input.GetKey(KeyCode.D))
		{
			axisX = 1;
		}
		currentAxisValue = new Vector2(axisX, axisY);
	}

	void TouchUpdate()
	{
		if (moveJoystick())
		{
			return;
		}
		//search for any touch created with a finger if the game is not in editor mode, or with the mouse in the editor
		int touchCount = Input.touchCount;
		if (!touchPlatform)
		{
			touchCount++;
		}
		for (int i = 0; i < touchCount; i++)
		{
			if (!touchPlatform)
			{
				currentTouch = convertMouseIntoFinger();
			}
			else {
				currentTouch = Input.GetTouch(i);
			}
			//if the touch action has started, check if the finger is inside the touch zone rect, visible in the editor
			if (currentTouch.phase == TouchPhase.Began && touchZoneRect.Contains(joystickCamera.ScreenToWorldPoint(currentTouch.position)))
			{
				currentFingerId = currentTouch.fingerId;
				fingerTouching(true);
				if (snapsToFinger)
				{
					stick.transform.position = stickBase.transform.position = joystickCamera.ScreenToWorldPoint(currentTouch.position);
				}
				if (touchPad)
				{
					previousPosition = joystickCamera.ScreenToWorldPoint(currentTouch.position);
				}
			}
		}
	}

    bool moveJoystick()
    {
		if (touching)
        {
            Touch? touch = getTouch(currentFingerId);
            if (touch == null || touch.Value.phase == TouchPhase.Ended)
            {
                resetJoystickPosition();
                return false;
            }
            Vector3 globalTouchPosition = joystickCamera.ScreenToWorldPoint(touch.Value.position);
            Vector3 differenceVector = globalTouchPosition - stickBase.transform.position;
            if (differenceVector.sqrMagnitude > dragDistance * dragDistance)
            {
                differenceVector.Normalize();
                stick.transform.position = stickBase.transform.position + differenceVector * dragDistance;
            }
            else {
                stick.transform.position = globalTouchPosition;
            }
            if (!touchPad)
            {
                currentAxisValue = differenceVector;
            }
            else {
                Vector3 difference = globalTouchPosition - previousPosition;
                if (differenceVector.sqrMagnitude > dragDistance * dragDistance)
                {
                    difference.Normalize();
                }
                currentAxisValue = difference;
                previousPosition = globalTouchPosition;
            }
            return true;
        }
        return false;
    }

    public void setJoystickPosition()
    {
        if (!joystickCamera)
        {
            joystickCamera = transform.parent.GetComponent<Camera>();
        }
        float halfHeight = joystickCamera.orthographicSize;
        float halfWidth = halfHeight * joystickCamera.aspect;
        Vector3 newPosition = Vector3.zero;//18671380548
        newPosition.x = (int)align * (halfWidth - margins.x);
        newPosition.y = -halfHeight + margins.y;
        touchZoneRect = new Rect(transform.position.x - touchZoneSize.x / 2f, transform.position.y - touchZoneSize.y / 2f, touchZoneSize.x, touchZoneSize.y);
        transform.localPosition = newPosition;
        transform.localScale = new Vector3(padSize, padSize, 1);
    }

    void resetJoystickPosition()
    {
        stick.transform.localPosition = stickBase.transform.localPosition = Vector3.zero;
        currentAxisValue = Vector2.zero;
        fingerTouching(false);
    }

    void fingerTouching(bool state)
    {
        touching = state;
        if (showJoystick)
        {
            if (hideOnRelease)
            {
                setSticksState(state, state);
            }
            else if ((!stickBase.gameObject.activeSelf || !stick.gameObject.activeSelf))
            {
                setSticksState(true, true);
            }
        }
        else {
            setSticksState(false, false);
        }
    }

    void setSticksState(bool stickBaseState, bool stickState)
    {
        stickBase.gameObject.SetActive(stickBaseState);
        stick.gameObject.SetActive(stickState);
    }

    Touch? getTouch(int fingerId)
    {
        if (!touchPlatform)
        {
            if (fingerId == 11)
            {
                return convertMouseIntoFinger();
            }
        }
        int touchCount = Input.touchCount;
        for (int i = 0; i < touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);
            if (touch.fingerId == fingerId)
            {
                return touch;
            }
        }
        return null;
    }

    //it simulates touch control in the game with the mouse position, using left button as tap finger with press, hold and release actions
    public static Touch convertMouseIntoFinger()
    {
        object mouseFinger = new Touch();
        FieldInfo phase = mouseFinger.GetType().GetField("m_Phase", BindingFlags.NonPublic | BindingFlags.Instance);
        FieldInfo fingerId = mouseFinger.GetType().GetField("m_FingerId", BindingFlags.NonPublic | BindingFlags.Instance);
        FieldInfo position = mouseFinger.GetType().GetField("m_Position", BindingFlags.NonPublic | BindingFlags.Instance);
        if (Input.GetMouseButtonDown(0))
        {
            phase.SetValue(mouseFinger, TouchPhase.Began);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            phase.SetValue(mouseFinger, TouchPhase.Ended);
        }
        else {
            phase.SetValue(mouseFinger, TouchPhase.Moved);
        }
        fingerId.SetValue(mouseFinger, 11);
        position.SetValue(mouseFinger, new Vector2(Input.mousePosition.x, Input.mousePosition.y));
        return (Touch)mouseFinger;
    }

    //check the if the current platform is a touch device
    public static bool checkTouchPlatform()
    {
        bool value = false;
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            value = true;
        }
        return value;
    }


    //get the vertical and horizontal axis values
    public Vector2 GetAxis()
    {
        return currentAxisValue;
    }

    void OnDrawGizmos()
    {
        DrawGizmos();
    }
    void OnDrawGizmosSelected()
    {
        DrawGizmos();
    }
    void DrawGizmos()
    {
        if (!Application.isPlaying)
        {
            if (!joystickCamera)
            {
                Start();
            }
            setJoystickPosition();
        }
        Gizmos.color = Color.red;
        Vector3 touchZone = new Vector3(touchZoneRect.x + touchZoneRect.width / 2f, touchZoneRect.y + touchZoneRect.height / 2f, transform.position.z);
        Gizmos.DrawWireCube(touchZone, new Vector3(touchZoneSize.x, touchZoneSize.y, 0f));
    }
}
