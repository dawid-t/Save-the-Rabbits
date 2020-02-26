using UnityEngine;

public class ParentSetter : MonoBehaviour
{
	private Transform currentParent;


	private void OnCollisionStay(Collision collision)
	{
		if(collision.transform != currentParent && collision.gameObject.layer == 11)
		{
			currentParent = collision.transform;
			transform.parent = currentParent;
		}
	}

	private void OnCollisionExit(Collision collision)
	{
		if(collision.transform == currentParent)
		{
			currentParent = null;
			transform.parent = null;
		}
	}
}
