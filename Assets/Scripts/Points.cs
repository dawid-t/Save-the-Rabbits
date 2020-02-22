using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Points : MonoBehaviour
{
	private static Points instance;

	private int maxScore = 10000, shots = 0, time = 0, score;
	[SerializeField]
	private Text scoreText;


	public static Points Instance => instance;


	private void Awake()
	{
		instance = this;
		StartCoroutine(CountTime());
	}

	private IEnumerator CountTime()
	{
		while(true)
		{
			yield return new WaitForSeconds(1);
			time++;
		}
	}

	public void AddShot()
	{
		shots++;
	}

	public void UpdateScoreText()
	{
		score = maxScore - (shots*100 + time*10);
		scoreText.text = "LEVEL COMPLETED!\nSHOTS: "+shots+"\nTIME: "+time+"s\nSCORE: "+score;
	}
}
