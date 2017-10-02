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
    public GameObject ShadowPad;
    
    // Use this for initialization
    void Start () {
	    if(StartPos !=null)
        {
            StandPos = StartPos;
            var position = StandPos.transform.position;
            var rotation = StandPos.transform.rotation;
            gameObject.transform.SetPositionAndRotation(position, rotation);
            
        }
        ShadowPad = gameObject.transform.Find("ShadowPad").gameObject;
        ShadowPad.SetActive(false);

	}
	
	// Update is called once per frame
	void Update () {
        if (Destination)
        {
            StartMove:
            if (StandPos == Destination)
            {
                Destination = null;
                goto SkipMove;
            }
            if (!NextPos)
            {
                NextPos = FindNext(Destination);
                moveTime = 0;
            }
            // Display the shadow pad
            if (NextPos.GetComponent<Waypoint>().EnableShadowPad || StandPos.GetComponent<Waypoint>().EnableShadowPad)
                ShadowPad.SetActive(true);
            else
                ShadowPad.SetActive(false);
            // Skip the VirtualWaypoint
            if (StandPos.GetComponent<Waypoint>() is VirtualWaypoint && NextPos.GetComponent<Waypoint>() is VirtualWaypoint)
            {
                StandPos = NextPos;
                NextPos = null;

                var position = StandPos.transform.position;
                var rotation = StandPos.transform.rotation;
                gameObject.transform.SetPositionAndRotation(position, rotation);
                goto StartMove;
            }

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
                var rotate = NextPos.transform.rotation.eulerAngles - StandPos.transform.rotation.eulerAngles;
                move *= Time.deltaTime / Speed;
                rotate *= Time.deltaTime / Speed;
                if(StandPos.GetComponent<Waypoint>() is SubWaypoint)
                {
                    var distance = (StandPos.GetComponent<Waypoint>() as SubWaypoint).Distance;
                    move /= distance;
                    rotate /= distance;
                    moveTime += Time.deltaTime / distance;
                }
                else if(NextPos.GetComponent<Waypoint>() is SubWaypoint)
                {
                    var distance = (NextPos.GetComponent<Waypoint>() as SubWaypoint).Distance;
                    move /= distance;
                    rotate /= distance;
                    moveTime += Time.deltaTime / distance;
                }
                else
                {
                    moveTime += Time.deltaTime;
                }
                move = gameObject.transform.InverseTransformVector(move);
                //rotate = gameObject.transform.InverseTransformVector(rotate);
                gameObject.transform.Translate(move);
                gameObject.transform.Rotate(rotate);
            }
        }
        SkipMove:;
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
                if (next == null)
                    continue;
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
        bool x = false;
        for(p=to; p.from != from; p = p.from)
        {
            if (x)
                return null;
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
