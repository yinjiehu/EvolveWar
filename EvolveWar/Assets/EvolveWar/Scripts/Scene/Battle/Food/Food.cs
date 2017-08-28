using UnityEngine;
using System.Collections;

public class Food : MonoBehaviour {

	protected int _exp;

	public virtual void Init()
	{
	}

	protected virtual void OnTriggerEnter2D(Collider2D collider)
	{
		//Debug.Log("trigger food gameObject is " + collider.name);
	}

	public int Exp { get { return _exp; } }
}
