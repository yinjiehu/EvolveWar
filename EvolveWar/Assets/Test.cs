using Network.Http.Protocols;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SendTest()
	{
		DownloadProtocol.GetAndroidVersionXml.BackgroundSend(this, (res) =>
		{
			Debug.Log(res);
		});
	}
}
