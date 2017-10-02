using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Surface : MonoBehaviour {
    public GameObject Waypoint;
	// Use this for initialization
	void Start () {
        var waypoint = this.transform.Find("Waypoint");
        if (waypoint != null)
            Waypoint = waypoint.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseUp()
    {

        if (Waypoint)
        {
            GameObject.Find("Player").GetComponent<Player>().Destination = Waypoint;
        }
    }
}
