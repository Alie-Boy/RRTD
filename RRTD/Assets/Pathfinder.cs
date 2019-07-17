using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour {

	[SerializeField] Waypoint start, end;

	Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();

	// Use this for initialization
	void Start () {
		LoadAllWaypoints();
		ColorStartAndEnd();
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
		print(grid.Count);
	}

	// Update is called once per frame
	void Update () {
		
	}
}
