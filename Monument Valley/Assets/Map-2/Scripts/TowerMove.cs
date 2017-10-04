using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerMove : WaypointCallback {
    public GameObject WaypointHigh;
    public GameObject WaypointLow;
    public GameObject[] WaypointList;

    public override void ConnectCallback(int param)
    {
        if (param == 0)
        {
            WaypointLow.GetComponent<Waypoint>().AddWaypoint(WaypointList[0]);
        }
        else if (param == 1)
        {
            WaypointHigh.GetComponent<Waypoint>().AddWaypoint(WaypointList[0]);
            WaypointLow.GetComponent<Waypoint>().AddWaypoint(WaypointList[1]);
        }
        else if (param == 2)
        {
            WaypointHigh.GetComponent<Waypoint>().AddWaypoint(WaypointList[1]);
        }
    }

    public override void DisConnectCallback(int param)
    {
        WaypointHigh.GetComponent<Waypoint>().ClearWaypoint();
        WaypointLow.GetComponent<Waypoint>().ClearWaypoint();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
