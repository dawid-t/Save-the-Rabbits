using UnityEngine;

public class ThrowPoop : MonoBehaviour
{
	private bool clicked = false, cancel = false;
	[SerializeField]
	private int poopVelocityMultiplier = 10;
	private int maxDragSqrMagnitude = 5;
	private float dragSqrMagnitude;
	private Ray startRay, endRay;
	[SerializeField]
	private MouseSlice mouseSlice;
	[SerializeField]
	private GameObject poopPrefab, buttFirePrefab;
	[SerializeField]
	private Transform butt, buttFirePositionRight, buttFirePositionLeft;
	private new Camera camera;
	private LineRenderer poopDirectionLineRenderer;


	public int MaxDragSqrMagnitude => maxDragSqrMagnitude;
	public float DragSqrMagnitude => dragSqrMagnitude;


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
			Cursor.visible = false;
			poopDirectionLineRenderer.positionCount = 2;

			startRay = camera.ScreenPointToRay(Input.mousePosition);
			endRay = camera.ScreenPointToRay(Input.mousePosition);

			//poopDirectionLineRenderer.SetPosition(0, startRay.origin); // Free draw.
			Vector3 gameObjectPosition = new Vector3(butt.position.x, butt.position.y, startRay.origin.z);
			poopDirectionLineRenderer.SetPosition(0, gameObjectPosition); // Draw on the Rabbit.
		}

		if(clicked) // Holding the left mouse button.
		{
			Ray endRayTmp = camera.ScreenPointToRay(Input.mousePosition);
			endRay.origin = Vector3.MoveTowards(startRay.origin, endRayTmp.origin, Mathf.Sqrt(maxDragSqrMagnitude));

			//poopDirectionLineRenderer.SetPosition(1, endRay.origin); // Free draw.
			Vector3 gameObjectPosition = new Vector3(butt.position.x, butt.position.y, startRay.origin.z);
			poopDirectionLineRenderer.SetPosition(1, gameObjectPosition+(endRay.origin - startRay.origin)); // Draw on the Rabbit.

			dragSqrMagnitude = Mathf.Abs((endRayTmp.origin - startRay.origin).sqrMagnitude);
			dragSqrMagnitude = (dragSqrMagnitude <= 5) ? dragSqrMagnitude : maxDragSqrMagnitude;

			// LineRenderer color:
			float hsvColorPercent = Mathf.Abs(dragSqrMagnitude-maxDragSqrMagnitude)/maxDragSqrMagnitude * 0.32f; // 0% (min) = red, 16% (mid) = yellow, 32% (max) = green.
			poopDirectionLineRenderer.sharedMaterial.SetColor("_BaseColor", Color.HSVToRGB(hsvColorPercent, 1, 1));
		}
	}

	private void Throw()
	{
		if(Input.GetMouseButtonUp(0) && !cancel) // Released the left mouse button.
		{
			clicked = false;
			Cursor.visible = true;
			poopDirectionLineRenderer.positionCount = 0;

			GameObject poop = Instantiate(poopPrefab, butt.position, Quaternion.identity);
			poop.GetComponent<TerrainCutter>().MouseSlice = mouseSlice;

			Vector3 velocity = endRay.origin - startRay.origin;
			poop.GetComponent<Rigidbody>().velocity = new Vector3(velocity.x, velocity.y, 0)*(-poopVelocityMultiplier);
			Destroy(poop, 10);

			GameObject buttFire = Instantiate(buttFirePrefab, buttFirePositionRight.position, buttFirePositionRight.rotation);
			GameObject buttFire2 = Instantiate(buttFirePrefab, buttFirePositionLeft.position, buttFirePositionLeft.rotation);
			Destroy(buttFire, 1);
			Destroy(buttFire2, 1);

			dragSqrMagnitude = 0;
		}
	}

	private void CancelThrow()
	{
		if(Input.GetMouseButtonDown(1)) // Clicked the right mouse button.
		{
			clicked = false;
			cancel = true;
			Cursor.visible = true;
			poopDirectionLineRenderer.positionCount = 0;

			dragSqrMagnitude = 0;
		}
	}
}
