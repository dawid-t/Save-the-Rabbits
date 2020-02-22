using UnityEngine;

public class TerrainCutter : MonoBehaviour
{
	[SerializeField]
	private GameObject bloodPrefab, poopBumPrefab;
	private Vector3 lastVelocity;
	private Rigidbody rb;
	private MouseSlice mouseSlice;


	public MouseSlice MouseSlice { get => mouseSlice; set => mouseSlice = value; }


	private void Awake()
	{
		rb = GetComponent<Rigidbody>();
	}

	private void FixedUpdate()
	{
		lastVelocity = rb.velocity;
	}

	private void OnTriggerEnter(Collider other)
	{
		Vector3 vec = new Vector3(transform.position.x+0.2f, transform.position.y+0.2f, -2);
		float angle = Mathf.Atan2(lastVelocity.y, lastVelocity.x) * Mathf.Rad2Deg;
		Quaternion quat = Quaternion.AngleAxis(angle, Vector3.forward);
		GameObject spawnedEffect;

		if(other.transform.parent != null && other.transform.parent.CompareTag("SmallRabbit")) // Small rabbit collision (restart the level).
		{
			spawnedEffect = Instantiate(bloodPrefab, other.transform.position, Quaternion.identity);
			Destroy(other.transform.parent.gameObject);
			Basket.Instance.Fail(true);
		}
		else // Ground collision.
		{
			if(other.gameObject.CompareTag("Sliceable"))
			{
				mouseSlice.CutMesh(transform.position, transform.position+lastVelocity.normalized, Vector3.forward, other.gameObject);
			}
			spawnedEffect = Instantiate(poopBumPrefab, vec, quat);
		}

		Destroy(spawnedEffect, 1);
		Destroy(gameObject);
	}
}
