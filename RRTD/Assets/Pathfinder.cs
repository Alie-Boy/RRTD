using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour {

	[SerializeField] Waypoint start, end;

	Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();
	Queue<Waypoint> queue = new Queue<Waypoint>();
	bool isRunning = true;

	Vector2Int[] directions =
	{
		Vector2Int.up,
		Vector2Int.right,
		Vector2Int.down,
		Vector2Int.left
	};

	// Use this for initialization
	void Start () {
		LoadAllWaypoints();
		ColorStartAndEnd();
		Pathfind();
	}

	private void Pathfind()
	{
		queue.Enqueue(start);
		while (queue.Count > 0 && isRunning)
		{
			Waypoint searchCenter = queue.Dequeue();
			print("Searching from: " + searchCenter);
			if (searchCenter == end)
			{
				print("End waypoint found.");
				isRunning = false;
				return;
			}
			ExploreNeighbors(searchCenter);
			searchCenter.isExplored = true;
		}
	}

	private void ExploreNeighbors(Waypoint from)
	{
		if (isRunning == false) return;

		Vector2Int neighborCoords;
		foreach (Vector2Int direction in directions)
		{
			neighborCoords = from.GetGridPos() + direction;
			if (grid.ContainsKey(neighborCoords))
			{
				QueueNewNeighbor(neighborCoords);
			}
		}
	}

	private void QueueNewNeighbor(Vector2Int neighborCoords)
	{
		Waypoint neighbor = grid[neighborCoords];
		if (neighbor.isExplored) return;
		neighbor.SetWaypointColor(Color.yellow);
		queue.Enqueue(neighbor);
		print("Queueing: " + neighborCoords);
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
