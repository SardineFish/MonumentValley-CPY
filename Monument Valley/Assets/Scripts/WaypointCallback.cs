using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WaypointCallback: MonoBehaviour {
    public abstract void ConnectCallback(int param);
    public abstract void DisConnectCallback(int param);
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
