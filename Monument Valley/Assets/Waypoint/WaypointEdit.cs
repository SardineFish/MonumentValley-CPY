﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class WaypointEdit : MonoBehaviour {
    Waypoint waypoint;
    public Dictionary<GameObject, GameObject> ConnectLines = new Dictionary<GameObject, GameObject>();
    public bool TwoWay = true;
    // Use this for initialization
    void Start ()
    {
        if (waypoint == null)
        {
            waypoint = gameObject.GetComponent<Waypoint>();
        }
    }

    void Awake()
    {
        for (var connect = GameObject.Find("__connection"); connect != null; connect = GameObject.Find("__connection"))
        {
            GameObject.DestroyImmediate(connect);
        }
        if (waypoint == null)
        {
            waypoint = gameObject.GetComponent<Waypoint>();
        }
    }

    // Update is called once per frame
    void Update ()
    {
        if (waypoint == null)
        {
            waypoint = gameObject.GetComponent<Waypoint>();
        }
        foreach (var connect in ConnectLines.Keys)
        {
            if (!waypoint.Next.Contains(connect))
            {
                GameObject.DestroyImmediate(ConnectLines[connect]);
                ConnectLines.Remove(connect);
            }
        }
        for(var i = 0; i < waypoint.Next.Count; i++)
        {
            var connect = waypoint.Next[i];
            if (connect == null)
                continue;
            if (TwoWay)
            {
                try
                {
                    if (!connect.GetComponent<Waypoint>().Next.Contains(gameObject))
                        connect.GetComponent<Waypoint>().Next.Add(gameObject);
                }
                catch (Exception ex)
                {
                    var x = ex;
                }
            }
            if (connect == null)
                continue;
            if(!ConnectLines.ContainsKey(connect))
            {
                var cntLines = GameObject.Find("__cntLines");
                if (!cntLines)
                    cntLines = new GameObject("__cntLines");
                var line = new GameObject();
                line.transform.SetParent(cntLines.transform);
                line.name = "__connection";
                line.AddComponent<ConnectLine>();
                line.AddComponent<LineRenderer>();
                var lineRender = line.GetComponent<LineRenderer>();
                lineRender.startColor = Color.red;
                lineRender.endColor = Color.red;
                lineRender.startWidth = 0.05f;
                lineRender.endWidth = 0.05f;
                lineRender.SetPosition(0, gameObject.transform.position);
                lineRender.SetPosition(1, connect.transform.position);
                ConnectLines[connect] = line;
            }
        }
    }
}
