using System.Collections.Generic;
using UnityEngine;

public class Basket : MonoBehaviour
{
	private int goal;
	private List<GameObject> rabbitsInBasketList = new List<GameObject>();


	private void Awake()
	{
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
		Debug.Log("Goal Reached!!");
	}
}
