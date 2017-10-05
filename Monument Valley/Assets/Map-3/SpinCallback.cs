using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinCallback : WaypointCallback {
    public GameObject[] WaypointList;
    public GameObject[] WaypointOnPivot;
    public GameObject[] WaypointOnTop;
    public override void ConnectCallback(int param)
    {
        if(0<param && param < 4)
            Waypoint.Get(WaypointOnPivot[param]).AddWaypoint(WaypointList[0]);
        if (param == 0)
        {
            Waypoint.Get(WaypointOnTop[0]).AddWaypoint(WaypointList[1]);
        }
        else if (param == 2)
        {
            Waypoint.Get(WaypointOnTop[1]).AddWaypoint(WaypointList[2]);
        }
    }

    public override void DisConnectCallback(int param)
    {
        foreach (var waypointObj in WaypointList)
        {
            var waypoint = Waypoint.Get(waypointObj);
            for (var i = 0; i < WaypointOnPivot.Length; i++)
                Waypoint.Get(WaypointOnPivot[i]).RemoveWaypoint(waypointObj);
            for (var i = 0; i < WaypointOnTop.Length; i++)
                Waypoint.Get(WaypointOnTop[i]).RemoveWaypoint(waypointObj);

        }
    }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
