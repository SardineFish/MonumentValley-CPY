using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class Waypoint : MonoBehaviour {
    public bool Visible = false;
    public List<GameObject> Next = new List<GameObject>();
    public bool EnableShadowPad = false;
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
        if(NextWaypoint.Count < Next.Count)
        {
            NextWaypoint = new List<Waypoint>();
            foreach (var waypoint in Next)
            {
                NextWaypoint.Add(waypoint.GetComponent<Waypoint>());
            }
        }

	}

    public void AddWaypoint(GameObject waypoint)
    {
        if (!waypoint.GetComponent<Waypoint>().Next.Contains(this.gameObject))
        {
            waypoint.GetComponent<Waypoint>().Next.Add(this.gameObject);
        }
        if (!waypoint.GetComponent<Waypoint>().NextWaypoint.Contains(this))
        {
            waypoint.GetComponent<Waypoint>().NextWaypoint.Add(this);
        }
        if (!this.Next.Contains(waypoint))
            this.Next.Add(waypoint);
        if (!this.NextWaypoint.Contains(waypoint.GetComponent<Waypoint>()))
            this.NextWaypoint.Add(waypoint.GetComponent<Waypoint>());
    }

    public void RemoveWaypoint(GameObject waypoint)
    {
        // Remove this waypoint from that waypoint.
        if (waypoint.GetComponent<Waypoint>().Next.Contains(gameObject))
            waypoint.GetComponent<Waypoint>().Next.Remove(gameObject);
        if (waypoint.GetComponent<Waypoint>().NextWaypoint.Contains(this))
            waypoint.GetComponent<Waypoint>().NextWaypoint.Remove(this);

        if (Next.Contains(waypoint))
            this.Next.Remove(waypoint);
        if (this.NextWaypoint.Contains(waypoint.GetComponent<Waypoint>()))
            this.NextWaypoint.Remove(waypoint.GetComponent<Waypoint>());
    }

    public void ClearWaypoint()
    {
        foreach (var waypoint in Next)
        {
            RemoveWaypoint(waypoint);
        }
    }
}
