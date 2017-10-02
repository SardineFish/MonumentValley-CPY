using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public GameObject StartPos;
    public GameObject NextPos;
    public GameObject StandPos;
    public GameObject Destination;
    public float Speed = 1;
    float moveTime = 0;
    
    // Use this for initialization
    void Start () {
	    if(StartPos !=null)
        {
            StandPos = StartPos;
            var position = StandPos.transform.position;
            var rotation = StandPos.transform.rotation;
            gameObject.transform.SetPositionAndRotation(position, rotation);
            
        }

	}
	
	// Update is called once per frame
	void Update () {
        if (Destination)
        {
            if (!NextPos)
            {
                NextPos = FindNext(Destination);
                moveTime = 0;
            }
            else
            {
                if (moveTime >= Speed)
                {
                    StandPos = NextPos;
                    NextPos = null;

                    var position = StandPos.transform.position;
                    var rotation = StandPos.transform.rotation;
                    gameObject.transform.SetPositionAndRotation(position, rotation);
                }
                else
                {
                    var move = NextPos.transform.position - StandPos.transform.position;
                    move /= Time.deltaTime / Speed;
                    gameObject.transform.Translate(move);
                    moveTime += Time.deltaTime;
                }
            }
        }
	}
    public Waypoint BFS(Waypoint from,Waypoint to,float time)
    {
        List<Waypoint> visitList = new List<Waypoint>();
        visitList.Add(from);
        List<Waypoint> nextList;

        Next:
        nextList = new List<Waypoint>();
        foreach(var visit in visitList)
        {
            if (visit == to)
                goto Found;
            if (visit.visitTime >= time)
                continue;
            visit.visitTime = time;
            foreach(var next in visit.NextWaypoint)
            {
                // not visited.
                if (next.visitTime < time)
                {
                    next.from = visit;
                    nextList.Add(next);
                }
            }
        }

        // No way
        if (nextList.Count <= 0)
            return null;

        visitList = nextList;
        goto Next;

        Found:
        var p = to;
        for(p=to; p.from != from; p = p.from)
        {

        }
        return p;
    }
    public GameObject FindNext(GameObject dst)
    {
        var to = dst.GetComponent<Waypoint>();
        var from = StandPos.GetComponent<Waypoint>();
        return BFS(from, to, Time.time).gameObject;
    }
}
