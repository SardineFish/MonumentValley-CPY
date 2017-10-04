using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMove : WaypointCallback {
    public GameObject[] Waypoints;
    public override void ConnectCallback(int param)
    {
        if (param == 0)
        {
            Waypoints[0].GetComponent<Waypoint>().AddWaypoint(Waypoints[1]);
        }
        else if (param == 1)
        {
            Waypoints[0].GetComponent<Waypoint>().AddWaypoint(Waypoints[2]);
            Waypoints[0].GetComponent<Waypoint>().AddWaypoint(Waypoints[3]);
        }
    }
    public override void DisConnectCallback(int param)
    {
        Waypoints[0].GetComponent<Waypoint>().ClearWaypoint();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
