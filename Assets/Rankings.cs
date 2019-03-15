using System;
using System.Collections;
using System.Collections.Generic;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.game;
using UnityEngine;

public class Rankings : MonoBehaviour, App42CallBack
{
	[SerializeField] ResultLine linePrefab;
	ResultLine[] lines = new ResultLine[10];
	void Start()
	{
		for (int i = 0; i < lines.Length; ++i)
		{
			lines[i] = Instantiate(linePrefab, transform);
		}
	}

	public void LoadRankings(ScoreBoardService service)
	{
		service.GetTopNRankings("TestLevel1", lines.Length, this);
	}

	public void OnSuccess(object response)
	{
		if (response is Game)
		{
			Game game = response as Game;
			IList<Game.Score> scores = game.GetScoreList();
			for (int i = 0; i < lines.Length; ++i)
			{
				if (i < scores.Count)
				{
					Game.Score score = scores[i];
					lines[i].Set(score.userName, (int) score.value);
				}
				else
				{
					lines[i].Hide();
				}
			}
		}
	}

	public void OnException(Exception exception)
	{
		if (exception is App42NotFoundException)
		{
			// leader board is currently empty
			for (int i = 0; i < lines.Length; ++i)
			{
				lines[i].Hide();
			}
		}
		else
		{
			Debug.LogWarningFormat("GetTopNRankings exception: {0}", exception);
		}
	}
}