using UnityEngine;

public class PoopRotator : MonoBehaviour
{
	private float rotateValue = 0;
	[SerializeField]
	private Transform poopSprite;


	private void Start()
	{
		Rigidbody rb = GetComponent<Rigidbody>();
		rotateValue = 10 * Mathf.Abs(rb.velocity.sqrMagnitude); // Bigger velocity = bigger rotation.
	}

	private void Update()
	{
		poopSprite.rotation = Quaternion.Euler(0, 0, poopSprite.rotation.eulerAngles.z + rotateValue*Time.deltaTime);
	}
}
