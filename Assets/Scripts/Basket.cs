using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Basket : MonoBehaviour
{
	private static Basket instance;

	private bool isLevelCompleted;
	private int goal;
	private List<GameObject> rabbitsInBasketList = new List<GameObject>();
	[SerializeField]
	private Canvas winCanvas, changeSceneEffectCanvas;
	[SerializeField]
	private AudioSource winAudioSource;


	public static Basket Instance => instance;


	private void Awake()
	{
		instance = this;
		isLevelCompleted = false;
		goal = GameObject.FindGameObjectsWithTag("SmallRabbit").Length;
	}

	private void OnTriggerEnter(Collider collider)
	{
		GameObject rabbit = collider.transform.parent?.gameObject; // Probably a rabbit.
		if(rabbit != null && rabbit.CompareTag("SmallRabbit"))
		{
			if(!rabbitsInBasketList.Contains(rabbit))
			{
				rabbitsInBasketList.Add(rabbit);
				if(rabbitsInBasketList.Count == goal)
				{
					GoalReached();
				}
			}
		}
	}

	private void OnTriggerExit(Collider collider)
	{
		GameObject rabbit = collider.transform.parent?.gameObject; // Probably a rabbit.
		if(rabbit != null && rabbit.CompareTag("SmallRabbit"))
		{
			if(rabbitsInBasketList.Contains(rabbit))
			{
				rabbitsInBasketList.Remove(rabbit);
			}
		}
	}

	private void GoalReached()
	{
		isLevelCompleted = true;
		Points.Instance.UpdateScoreText();

		winCanvas.enabled = true;
		winCanvas.transform.GetChild(0).GetComponent<Animator>().Play("Start", 0, 0);
		winAudioSource.Play();
	}

	public void Fail(bool waitLonger)
	{
		StartCoroutine(Restart(waitLonger));
	}

	private IEnumerator Restart(bool waitLonger)
	{
		if(waitLonger)
		{
			yield return new WaitForSeconds(2);
		}

		changeSceneEffectCanvas.enabled = true;
		changeSceneEffectCanvas.transform.GetChild(0).GetComponent<Animator>().Play("End", 0, 0);

		yield return new WaitForSeconds(1);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
