using UnityEngine;

public class TerrainCutter : MonoBehaviour
{
	public GameObject gameObjectToCut;


	private void Start()
	{
		CutMesh();
	}

	private void Update()
	{

	}

	public void CutMesh()
	{
		/*
		 * 1. Pobierz dotknietego mesha
		 * 2. Przetnij go w miejscu gdzie byl dotkniety
		 *
		 * ?. Przetnij mesha w pół
		 */
		MeshFilter meshFilter = gameObjectToCut.GetComponent<MeshFilter>();
		Mesh mesh = meshFilter.sharedMesh;

		/*foreach(Vector3 vec in mesh.vertices)
		{
			Debug.Log(vec);
		}*/
		//Debug.Log("vertices: "+mesh.vertices.Length+", triangles: "+mesh.triangles.Length);

		GameObject firstPiece = new GameObject(gameObjectToCut.name+"-FirstPiece");
		MeshFilter firstPieceMeshFilter = firstPiece.AddComponent<MeshFilter>();
		firstPiece.AddComponent<MeshRenderer>();

		firstPieceMeshFilter.sharedMesh = new Mesh();
		firstPieceMeshFilter.sharedMesh.vertices = new Vector3[mesh.vertices.Length];

		int i = 0;
		foreach(Vector3 vec in mesh.vertices)
		{
			if(mesh.vertices[i].x < 0)
			{
				firstPieceMeshFilter.sharedMesh.vertices[i] = mesh.vertices[i];
			}
			i++;
		}
	}
}
