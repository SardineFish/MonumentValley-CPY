using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class Waypoint : MonoBehaviour {
    public bool Visible = false;
    public List<GameObject> Next = new List<GameObject>();
    internal List<Waypoint> NextWaypoint = new List<Waypoint>();
    internal float visitTime = 0;
    internal Waypoint from;
	// Use this for initialization
	protected void Start () {
        gameObject.GetComponent<Renderer>().enabled = Visible;
        NextWaypoint = new List<Waypoint>();
        foreach (var waypoint in Next)
        {
            NextWaypoint.Add(waypoint.GetComponent<Waypoint>());
        }
	}

    // Update is called once per frame
    protected void Update () {
		
	}
}
