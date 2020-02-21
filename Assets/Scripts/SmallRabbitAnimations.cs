using UnityEngine;

public class SmallRabbitAnimations : MonoBehaviour
{
	private Rigidbody rb;
	[SerializeField]
	private GameObject leftEye, rightEye, leftClosedEye, rightClosedEye;


	private void Awake()
	{
		rb = GetComponent<Rigidbody>();
	}

	private void Update()
	{
		UpdateRabbitBehaviour();
	}

	private void UpdateRabbitBehaviour()
	{
		// Eyes:
		if(rb.velocity.sqrMagnitude < 1)
		{
			leftEye.SetActive(true);
			rightEye.SetActive(true);
			leftClosedEye.SetActive(false);
			rightClosedEye.SetActive(false);
		}
		else
		{
			leftEye.SetActive(false);
			rightEye.SetActive(false);
			leftClosedEye.SetActive(true);
			rightClosedEye.SetActive(true);
		}
	}
}
