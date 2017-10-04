using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_2_1_Move : WaypointCallback {
    public GameObject WaypointTop;
    public GameObject WaypointNearPillar;
    public GameObject WaypointLow;
    public GameObject[] WaypointList;
    public override void ConnectCallback(int param)
    {
        if (param == 0)
        {
            WaypointNearPillar.GetComponent<Waypoint>().AddWaypoint(WaypointList[2]);
        }
        if (param == 1)
        {
            WaypointTop.GetComponent<Waypoint>().AddWaypoint(WaypointList[0]);
            WaypointLow.GetComponent<Waypoint>().AddWaypoint(WaypointList[1]);
            WaypointNearPillar.GetComponent<Waypoint>().AddWaypoint(WaypointList[2]);
        }
    }

    public override void DisConnectCallback(int param)
    {
        for(var i = 0; i < WaypointList.Length; i++)
        {
            WaypointTop.GetComponent<Waypoint>().RemoveWaypoint(WaypointList[i]);
            WaypointLow.GetComponent<Waypoint>().RemoveWaypoint(WaypointList[i]);
            WaypointNearPillar.GetComponent<Waypoint>().RemoveWaypoint(WaypointList[i]);
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
