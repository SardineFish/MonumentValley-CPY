using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour {

    public Vector3 DragVector;
    public GameObject DragObject;
    public Vector3[] AlignPosition;
    public float AlignTime = 1;
    bool mouseHold = false;
    Vector3 holdPos;
    Vector3 startPos;

	// Use this for initialization
	void Start () {
		
	}
	// Update is called once per frame
	void Update () {
        
        if (mouseHold && Input.GetMouseButtonUp(0))
        {
            mouseHold = false;
            var minDst = float.MaxValue;
            var minAlign = DragObject.transform.position;
            var idx = 0;
            for(var i=0;i<AlignPosition.Length;i++)
            {
                var align = AlignPosition[i];
                var dst = (DragObject.transform.position - align).magnitude;
                if(dst<minDst)
                {
                    idx = i;
                    minDst = dst;
                    minAlign = align;
                }
            }
            MoveTo.MoveOptions options = new MoveTo.MoveOptions(DragObject, minAlign, AlignTime);
            options.OnFinished += (sender, e) =>
            {
                var callback = DragObject.GetComponent<WaypointCallback>();
                if (callback != null)
                    callback.ConnectCallback(idx);
            };
            MoveTo.Start(options);
        }
        if (mouseHold)
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            var dv = new Vector3();
            if (Physics.Raycast(ray, out hit,500,1<<8))
            {
                dv.y = hit.point.y - holdPos.y;
                print(hit.point);
                dv.y *= DragVector.y;
            }
            if (Physics.Raycast(ray, out hit, 500, 1 << 9))
            {
                dv.x = hit.point.x - holdPos.x;
                dv.x *= DragVector.x;
            }
            if (Physics.Raycast(ray, out hit, 500, 1 << 10))
            {
                dv.z = hit.point.z - holdPos.z;
                dv.z *= DragVector.z;
            }
            DragObject.transform.position = startPos + dv;

            var callback = DragObject.GetComponent<WaypointCallback>();
            if (callback != null)
                callback.DisConnectCallback(0);
        }
	}

    private void OnMouseDown()
    {
        if (mouseHold)
            return;
        mouseHold = true;
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        holdPos = new Vector3 ();
        startPos = DragObject.transform.position;
        if (Physics.Raycast(ray, out hit, 500, 1 << 8))
        {
            holdPos.y = hit.point.y;
        }
        if (Physics.Raycast(ray, out hit, 500, 1 << 9))
        {
            holdPos.x = hit.point.x;
        }
        if (Physics.Raycast(ray, out hit, 500, 1 << 10))
        {
            holdPos.z = hit.point.z;
        }
    }
}
