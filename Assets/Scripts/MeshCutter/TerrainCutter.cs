using UnityEngine;

public class TerrainCutter : MonoBehaviour
{
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
		if(other.gameObject.CompareTag("Sliceable"))
		{
			mouseSlice.CutMesh(transform.position, transform.position+lastVelocity.normalized, Vector3.forward, other.gameObject);

			// TODO: create effects here
		}
		Destroy(gameObject);
	}
}
