using UnityEngine;

namespace Utility
{
	public class Utility
	{

		public static Vector3 WorldToScreenPosition(Vector3 position)
		{
			var viewPoint = Camera.main.WorldToViewportPoint(position);
			var uiCamera = GameObject.FindGameObjectWithTag("UICamera").GetComponent<Camera>();
			return uiCamera.ViewportToWorldPoint(viewPoint);
		}

	}
}
