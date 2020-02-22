using UnityEngine;

public class TreeAnimations : MonoBehaviour
{
	[SerializeField]
	private int treeNumber;


	private void Awake()
	{
		GetComponent<Animator>().Play("Tree-"+treeNumber, 0, 0);
	}
}
