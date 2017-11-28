using UnityEngine;

using UnityEngine.SocialPlatforms;

using UnityEngine.SocialPlatforms.GameCenter;

using System.Collections;
using System;
using UI.Main;



public class GameCenterManager : MonoBehaviour {



	// Use this for initialization
//
//	void Start () {
//
//		Social.localUser.Authenticate(HandleAuthenticated);
//	}

	static GameCenterManager _instance;

	public static GameCenterManager Instance { get { return _instance; } }

	void Awake()
	{
		_instance = this;
	}
	bool _authenticated = false;
	Action<string> _callback;

	public void GameCenterLogin(Action<string> callback)
	{
		if (_authenticated) {
			Account.LoadAccount ();
			Account.LoadGift ();
			callback (Account.PlayerID);
			return;
		}
		_authenticated = true;
		#if UNITY_EDITOR
		callback.Invoke("111");
		#else
		_callback = callback;
		Social.localUser.Authenticate(HandleAuthenticated);
		#endif
	}


	#region 事件回调

	private void HandleAuthenticated(bool success)

	{
		Debug.Log("*** HandleAuthenticated: success = " + success);

		if (success) {
			if (_callback != null)
				_callback.Invoke (Social.localUser.id.Replace (':', '_'));
			

			Debug.Log ("*** HandleAuthenticated: Social.localUser.id = " + Social.localUser.id);
			//Social.localUser.LoadFriends(HandleFriendsLoaded);

			//Social.LoadAchievements(HandleAchievementsLoaded);

			//Social.LoadAchievementDescriptions(HandleAchievementDescriptionsLoaded);

		} else {
			DialogPanel.ShowDialogMessage ("Failed to Authenticate player.", delegate {
				PayMgr.Instance.CallIosSetting();
			});
		}

	}



	private void HandleFriendsLoaded(bool success)

	{

		Debug.Log("*** HandleFriendsLoaded: success = " + success);

		foreach (IUserProfile friend in Social.localUser.friends) {

			Debug.Log("*   friend = " + friend.ToString());

		}

	}

	private void HandleAchievementsLoaded(IAchievement[] achievements)

	{

		Debug.Log("*** HandleAchievementsLoaded");

		foreach (IAchievement achievement in achievements) {

			Debug.Log("*   achievement = " + achievement.ToString());

		}

	}

	private void HandleAchievementDescriptionsLoaded(IAchievementDescription[] achievementDescriptions)

	{

		Debug.Log("*** HandleAchievementDescriptionsLoaded");

		foreach (IAchievementDescription achievementDescription in achievementDescriptions) {

			Debug.Log("*   achievementDescription = " + achievementDescription.ToString());

		}

	}

	#endregion



	#region 成就系统

	/// <summary>

	/// 成就进度

	/// </summary>

	/// <param name="achievementId">Achievement identifier.</param>

	/// <param name="progress">Progress.</param>

	public void ReportProgress(string achievementId, double progress)

	{

		if (Social.localUser.authenticated) {

			Social.ReportProgress(achievementId, progress, HandleProgressReported);

		}

	}

	private void HandleProgressReported(bool success)

	{

		Debug.Log("*** 成就进度上传: success = " + success);

	}



	/// <summary>

	/// 显示成就

	/// </summary>

	public void ShowAchievements()

	{

		if (Social.localUser.authenticated) {

			Social.ShowAchievementsUI();

		}

	}

	#endregion



	#region 排名系统

	/// <summary>

	/// 上传积分

	/// </summary>

	/// <param name="leaderboardId">Leaderboard identifier.</param>

	/// <param name="score">Score.</param>

	public void ReportScore(string leaderboardId, long score)

	{

		if (Social.localUser.authenticated) {

			Social.ReportScore(score, leaderboardId, HandleScoreReported);

		}

	}

	public void HandleScoreReported(bool success)

	{

		Debug.Log("*** 排名积分上传: success = " + success);

	}



	/// <summary>

	/// 显示排名

	/// </summary>

	public void ShowLeaderboard()

	{

		if (Social.localUser.authenticated) {

			Social.ShowLeaderboardUI();

		}

	}

	#endregion

}