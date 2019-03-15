using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultLine : MonoBehaviour
{
	[SerializeField] Text nameText;
	[SerializeField] Text scoreText;
	
	public void Set(string name, int score)
	{
		nameText.text = name;
		scoreText.text = score.ToString();
		gameObject.SetActive(true);
	}

	public void Hide()
	{
		gameObject.SetActive(false);
	}
}
