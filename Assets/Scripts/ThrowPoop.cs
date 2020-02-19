using UnityEngine;

public class ThrowPoop : MonoBehaviour
{
	private bool clicked = false, cancel = false;
	[SerializeField]
	private int poopVelocity = 10;
	private Ray startRay, endRay;
	[SerializeField]
	private MouseSlice mouseSlice;
	[SerializeField]
	private GameObject poopPrefab;
	private new Camera camera;
	private LineRenderer poopDirectionLineRenderer;


	private void Awake()
	{
		camera = Camera.main;
		poopDirectionLineRenderer = GetComponent<LineRenderer>();
	}

	private void Update()
	{
		SetThrowForce();
		Throw();
		CancelThrow();
	}

	private void SetThrowForce()
	{
		if(!clicked && Input.GetMouseButtonDown(0)) // Clicked the left mouse button.
		{
			clicked = true;
			cancel = false;
			poopDirectionLineRenderer.positionCount = 2;

			startRay = camera.ScreenPointToRay(Input.mousePosition);
			endRay = camera.ScreenPointToRay(Input.mousePosition);

			//poopDirectionLineRenderer.SetPosition(0, startRay.origin); // Free draw.
			Vector3 gameObjectPosition = new Vector3(transform.position.x, transform.position.y, startRay.origin.z);
			poopDirectionLineRenderer.SetPosition(0, gameObjectPosition); // Draw on the Rabbit.
		}

		if(clicked) // Holding the left mouse button.
		{
			int maxSqrMagnitude = 5;
			Ray endRayTmp = camera.ScreenPointToRay(Input.mousePosition);
			endRay.origin = Vector3.MoveTowards(startRay.origin, endRayTmp.origin, Mathf.Sqrt(maxSqrMagnitude));

			//poopDirectionLineRenderer.SetPosition(1, endRay.origin); // Free draw.
			Vector3 gameObjectPosition = new Vector3(transform.position.x, transform.position.y, startRay.origin.z);
			poopDirectionLineRenderer.SetPosition(1, gameObjectPosition+(endRay.origin - startRay.origin)); // Draw on the Rabbit.

			// LineRenderer color:
			float sqrMagnitude = Mathf.Abs((endRayTmp.origin - startRay.origin).sqrMagnitude);
			sqrMagnitude = (sqrMagnitude <= 5) ? sqrMagnitude : maxSqrMagnitude;

			float hsvColorPercent = Mathf.Abs(sqrMagnitude-maxSqrMagnitude)/maxSqrMagnitude * 0.32f; // 0% (min) = red, 16% (mid) = yellow, 32% (max) = green.
			poopDirectionLineRenderer.sharedMaterial.SetColor("_BaseColor", Color.HSVToRGB(hsvColorPercent, 1, 1));
		}
	}

	private void Throw()
	{
		if(Input.GetMouseButtonUp(0) && !cancel) // Released the left mouse button.
		{
			clicked = false;
			poopDirectionLineRenderer.positionCount = 0;

			GameObject poop = Instantiate(poopPrefab, transform.position, Quaternion.identity);
			poop.GetComponent<TerrainCutter>().MouseSlice = mouseSlice;

			Vector3 velocity = endRay.origin - startRay.origin;
			poop.GetComponent<Rigidbody>().velocity = new Vector3(velocity.x, velocity.y, 0)*(-poopVelocity);
			Destroy(poop, 10);
		}
	}

	private void CancelThrow()
	{
		if(Input.GetMouseButtonDown(1)) // Clicked the right mouse button.
		{
			clicked = false;
			cancel = true;
			poopDirectionLineRenderer.positionCount = 0;
		}
	}
}
