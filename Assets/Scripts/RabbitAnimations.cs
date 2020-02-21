using UnityEngine;

public class RabbitAnimations : MonoBehaviour
{
	private ThrowPoop throwPoop;
	private Animator animator;
	[SerializeField]
	private GameObject leftEye, rightEye, leftClosedEye, rightClosedEye;
	[SerializeField]
	private SpriteRenderer face, leftBlush, rightBlush;


	private void Awake()
	{
		throwPoop = GetComponent<ThrowPoop>();
		animator = GetComponent<Animator>();
	}

	private void Update()
	{
		UpdateRabbitBehaviour();
	}

	private void UpdateRabbitBehaviour()
	{
		// Face:
		float redFace = (255 - 31*throwPoop.DragSqrMagnitude) / 255;
		face.color = new Color(1, redFace, redFace);

		// Face blush:
		float blushAlpha = throwPoop.DragSqrMagnitude / throwPoop.MaxDragSqrMagnitude;
		Color blushColor = new Color(1, 1, 1, blushAlpha);
		leftBlush.color = blushColor;
		rightBlush.color = blushColor;

		// Eyes:
		if(throwPoop.DragSqrMagnitude < 1)
		{
			leftEye.SetActive(true);
			rightEye.SetActive(true);
			leftClosedEye.SetActive(false);
			rightClosedEye.SetActive(false);
			animator.speed = 1;
		}
		else if(throwPoop.DragSqrMagnitude < 3)
		{
			leftEye.SetActive(true);
			rightEye.SetActive(false);
			leftClosedEye.SetActive(false);
			rightClosedEye.SetActive(true);
			animator.speed = 2;
		}
		else if(throwPoop.DragSqrMagnitude < 5)
		{
			leftEye.SetActive(false);
			rightEye.SetActive(false);
			leftClosedEye.SetActive(true);
			rightClosedEye.SetActive(true);
			animator.speed = 5;
		}
		else if(throwPoop.DragSqrMagnitude == 5)
		{
			leftEye.SetActive(false);
			rightEye.SetActive(false);
			leftClosedEye.SetActive(true);
			rightClosedEye.SetActive(true);
			animator.speed = 10;
		}
	}
}
