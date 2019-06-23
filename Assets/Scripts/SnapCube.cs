using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[SelectionBase]
[RequireComponent(typeof(Waypoint))]
public class SnapCube : MonoBehaviour {

    TextMesh label;
    Waypoint waypoint;

	// Use this for initialization
	void Awake () {
        waypoint = GetComponent<Waypoint>();
        label = GetComponentInChildren<TextMesh>();
    }
	
	// Update is called once per frame
	void Update () {
        SnapToGrid();
        UpdateLabel();
    }

    private void SnapToGrid()
    {
        Vector3 snapPos;

        snapPos.x = waypoint.GetGridpos().x;
        snapPos.z = waypoint.GetGridpos().y;
        snapPos.y = 0f;

        transform.position = snapPos;
    }

    private void UpdateLabel()
    {
        if (label)
        {
            label.text = "(" + waypoint.GetGridCartesian().x + ", " + waypoint.GetGridCartesian().y + ")";
            gameObject.name = "GC "+ label.text;
        }
    }
}
