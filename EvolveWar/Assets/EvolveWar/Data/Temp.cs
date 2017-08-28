using System;
using UnityEngine;

namespace EvolveWar.Data
{
	public class Temp : MonoBehaviour
	{
		[SerializeField]
		bool _online;
		public static bool Online { get { return Get ()._online; }}

		static Temp _instance;


		public static Temp Get()
		{
			if (_instance == null)
			{
				var go = new GameObject("Temp");
				_instance = go.AddComponent<Temp>();
			}
			return _instance;
		}

		void Awake()
		{
			_instance = this;
		}

		void Start()
		{
			var soundType = PlayerPrefs.GetInt("Sound", 0);
			if(soundType == 0)//音效打开
			{
				BGSoundPlayer.Instance.OpenSound();
			}
			else
			{
				BGSoundPlayer.Instance.CloseSound();
			}
		}

		public bool IsPlayingSoundBG()
		{
			var soundType = PlayerPrefs.GetInt("SoundBG", 0);
			return soundType == 0;
		}

		public bool IsPlayingSoundEffect()
		{
			var soundType = PlayerPrefs.GetInt("SoundEffect", 0);
			return soundType == 0;
		}
	}
}

