using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TerrainHolder : MonoBehaviour
{
	private static List<TerrainHolder> instacesList = new List<TerrainHolder>();

	private Rigidbody rb;


	private void Awake()
	{
		rb = GetComponent<Rigidbody>();
	}

	private void OnEnable()
	{
		instacesList.Add(this);
	}

	private void OnDisable()
	{
		instacesList.Remove(this);
	}

	public static void HoldTerrains()
	{
		foreach(TerrainHolder instance in instacesList)
		{
			instance.HoldTerrain();
		}
	}

	/// <summary>
	/// Set Rigidbody "IsKinematic" to true in terrains which are touching this GameObject.
	/// </summary>
	public void HoldTerrain()
	{
		Collider[] colliders = Physics.OverlapSphere(transform.position, 0.01f);
		foreach(Collider collider in colliders)
		{
			if(collider.gameObject.CompareTag("Sliceable"))
			{
				collider.GetComponent<Rigidbody>().isKinematic = true;
			}
		}
	}
}
