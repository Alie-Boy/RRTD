using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {
	
	void Start () {
		Pathfinder pathfinder = FindObjectOfType<Pathfinder>();
		List<Waypoint> path = pathfinder.GetPath();
		StartCoroutine(FollowPath(path));
	}

	IEnumerator FollowPath(List<Waypoint> path)
	{
		print("Starting Patrol...");
		foreach (Waypoint waypoint in path)
		{
			transform.position = waypoint.transform.position;
			print("Visiting block: " + waypoint.name);
			yield return new WaitForSeconds(1f);
		}
		print("Ending Patrol.");
	}

}
