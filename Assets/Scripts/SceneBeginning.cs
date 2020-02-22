using System.Collections;
using UnityEngine;

public class SceneBeginning : MonoBehaviour
{
	private void Awake()
	{
		StartCoroutine(Restart());
	}

	private IEnumerator Restart()
	{
		Canvas canvas = GetComponent<Canvas>();
		canvas.enabled = true;
		canvas.transform.GetChild(0).GetComponent<Animator>().Play("Start", 0, 0);

		yield return new WaitForSeconds(1);
		canvas.enabled = false;
	}
}
