using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
[SelectionBase]
[RequireComponent(typeof(Waypoint))]
public class EditorSnap : MonoBehaviour {

	Waypoint waypoint;

	void Awake()
	{
		waypoint = GetComponent<Waypoint>();
	}

	void Update()
	{
		SnapToGrid();
		UpdateLabel();
	}

	private void SnapToGrid()
	{
		int gridSize = waypoint.GetGridSize();
		transform.position = new Vector3(
				waypoint.GetGridPos().x * waypoint.GetGridSize(),
				0f,
				waypoint.GetGridPos().y * waypoint.GetGridSize()
			);
	}

	private void UpdateLabel()
	{
		TextMesh textMesh = GetComponentInChildren<TextMesh>();
		int gridSize = waypoint.GetGridSize();
		string labelText = "(" + waypoint.GetGridPos().x + "," + waypoint.GetGridPos().y + ")";
		textMesh.text = labelText;
		gameObject.name = labelText;
	}
}
