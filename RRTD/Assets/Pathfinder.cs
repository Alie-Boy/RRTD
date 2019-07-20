using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour {

	[SerializeField] Waypoint start, end;
	List<Waypoint> path = new List<Waypoint>();

	Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();
	Queue<Waypoint> queue = new Queue<Waypoint>();
	bool isRunning = true;
	Waypoint searchCenter;

	Vector2Int[] directions =
	{
		Vector2Int.up,
		Vector2Int.right,
		Vector2Int.down,
		Vector2Int.left
	};

	public List<Waypoint> GetPath()
	{
		LoadAllWaypoints();
		ColorStartAndEnd();
		BreadthFirstSearch();
		CreatePath();
		return path;
	}

	private void CreatePath()
	{
		path.Add(end);
		Waypoint previous = end.exploredFrom;
		while (previous != start)
		{
			path.Add(previous);
			previous = previous.exploredFrom;
		}
		path.Add(start);
		path.Reverse();
	}

	private void BreadthFirstSearch()
	{
		queue.Enqueue(start);
		while (queue.Count > 0 && isRunning)
		{
			searchCenter = queue.Dequeue();
			if (searchCenter == end)
			{
				isRunning = false;
				return;
			}
			ExploreNeighbors();
			searchCenter.isExplored = true;
		}
	}

	private void ExploreNeighbors()
	{
		if (isRunning == false) return;

		Vector2Int neighborCoords;
		foreach (Vector2Int direction in directions)
		{
			neighborCoords = searchCenter.GetGridPos() + direction;
			if (grid.ContainsKey(neighborCoords))
			{
				QueueNewNeighbor(neighborCoords);
			}
		}
	}

	private void QueueNewNeighbor(Vector2Int neighborCoords)
	{
		Waypoint neighbor = grid[neighborCoords];
		if (neighbor.isExplored || queue.Contains(neighbor)) return;
		queue.Enqueue(neighbor);
		neighbor.exploredFrom = searchCenter;
	}

	private void ColorStartAndEnd()
	{
		start.SetWaypointColor(Color.blue);
		end.SetWaypointColor(Color.red);
	}

	private void LoadAllWaypoints()
	{
		Waypoint[] waypoints = FindObjectsOfType<Waypoint>();
		foreach (Waypoint waypoint in waypoints)
		{
			if (grid.ContainsKey(waypoint.GetGridPos()))
			{
				Debug.LogWarning(waypoint.GetGridPos().ToString() + " already loaded.");
				continue;
			}
			grid.Add(waypoint.GetGridPos(), waypoint);
		}
		print("Loaded " + grid.Count + " Waypoints");
	}
	
}
