using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Kit.Utility
{
	public class ScrollCircle : ScrollRect, IPointerDownHandler
	{
		protected float mRadius = 0f;

		public Vector3 Direction { set; get; }

		protected override void Start()
		{
			base.Start();
			//计算摇杆块的半径
			mRadius = (transform as RectTransform).sizeDelta.x * 0.5f;

			this.content.anchoredPosition = Vector2.zero;
		}

		void Update()
		{
			if(this.content != null)
			{
				var contentPostion = this.content.anchoredPosition;
				Direction = contentPostion.normalized;
#if UNITY_EDITOR
				KeyBoardUpdate();
#endif
			}
			
		}

		void KeyBoardUpdate()
		{
			float axisX = 0;
			float axisY = 0;
			if (Input.GetKey(KeyCode.W))
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
			Direction = new Vector2(axisX, axisY);
		}


		public override void OnDrag(UnityEngine.EventSystems.PointerEventData eventData)
		{
			base.OnDrag(eventData);
			var contentPostion = this.content.anchoredPosition;
			if (contentPostion.magnitude > mRadius)
			{
				contentPostion = contentPostion.normalized * mRadius;
				SetContentAnchoredPosition(contentPostion);
			}
		}

		public override void OnEndDrag(PointerEventData eventData)
		{
			base.OnEndDrag(eventData);
			this.content.anchoredPosition = Vector2.zero;
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			base.OnBeginDrag(eventData);
			var contentPostion = this.content.anchoredPosition;
			if (contentPostion.magnitude > mRadius)
			{
				contentPostion = contentPostion.normalized * mRadius;
				SetContentAnchoredPosition(contentPostion);
			}
		}
	}
}