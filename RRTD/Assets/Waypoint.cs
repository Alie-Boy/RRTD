using UnityEngine;

public class Waypoint : MonoBehaviour {

	public bool isExplored = false;
	public Waypoint exploredFrom;

	Vector2Int gridPos;
	const int gridSize = 10;
	
	public int GetGridSize()
	{
		return gridSize;
	}

	public Vector2Int GetGridPos()
	{
		return new Vector2Int(
				Mathf.RoundToInt((int)transform.position.x / gridSize),
				Mathf.RoundToInt((int)transform.position.z / gridSize)
			);
	}

	public void SetWaypointColor(Color color)
	{
		MeshRenderer waypointMeshRen = transform.Find("Top").GetComponent<MeshRenderer>();
		waypointMeshRen.material.color = color;
	}
}
