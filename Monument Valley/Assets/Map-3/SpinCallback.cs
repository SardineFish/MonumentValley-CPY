using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinCallback : WaypointCallback {
    public Waypoint[] WaypointList;
    public Waypoint[] WaypointOnPivot;
    public Waypoint[] WaypointOnTop;
    public override void ConnectCallback(int param)
    {
        //throw new System.NotImplementedException();
    }

    public override void DisConnectCallback(int param)
    {
        //throw new System.NotImplementedException();
    }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
