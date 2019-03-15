using System;
using System.Collections;
using System.Collections.Generic;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.game;
using UnityEngine;
using Random = UnityEngine.Random;

public class LeaderBoard : MonoBehaviour, App42CallBack
{
	[SerializeField] Rankings rankings;
	ScoreBoardService scoreBoardService;
	string testUserName = "Paul";
	int testScore = 1;

	public void OnException(Exception exception)
	{
		Debug.LogWarningFormat("SaveScore exception: {0}", exception);
	}

	public void OnSuccess(object response)
	{
		if (response is App42OfflineResponse)
		{
			Debug.LogFormat("Network not available, score stored in cache");
		}
		else if (response is Game)
		{
			Debug.LogFormat("Score saved to online leader board");
		}
	}

	void Start()
	{
		string apiKey = "77380b4ffde67a4df3ca6823eb6c02fa6a2fc2917378a565bdb9e267a2457100";
		string secretKey = "d863121bdcaa08bfca3d41dc3c4d41f26f7489c09efb1992a9bd6695eecc4223";

		App42API.Initialize(apiKey, secretKey);
		App42API.SetOfflineStorage(true);
		scoreBoardService = App42API.BuildScoreBoardService();

		UpdateRankings();
	}

	public void SetUserName(string user)
	{
		testUserName = user;
	}

	public void SetScore(string score)
	{
		testScore = int.Parse(score);
	}

	public void SubmitScore()
	{
		scoreBoardService.SaveUserScore("TestLevel1", testUserName, testScore, this);
	}

	public void SubmitRandomScores()
	{
		string [] names = new string [] 
		{
			"44Magic44",
			"amymanchester",
			"charga600",
			"cupidc",
			"DanNameTaken",
			"DomainofPicasso",
			"EyeballZ678",
			"FullSilver",
			"galadrea55",
			"HarryBushell",
			"JavanKD",
			"KuraiSensei",
			"Lakshitha77",
			"MarlonLM",
			"MordanonVihl",
			"Patturner109",
			"paulsinnett",
			"piersreed",
			"rapopescu",
			"StoneJay",
			"timj96",
			"TommyJoeBrown",
			"WOR15105027",
			"xSystemIOx",
			"zzTVO"
		};

		foreach (string name in names)
		{
			scoreBoardService.SaveUserScore("TestLevel1", name, Random.Range(1, 2000), this);
		}
	}

	public void UpdateRankings()
	{
		rankings.LoadRankings(scoreBoardService);
	}
}
