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
        
        if (Input.GetMouseButtonUp(0))
        {
            mouseHold = false;
            var minDst = float.MaxValue;
            var minAlign = DragObject.transform.position;
            foreach (var align in AlignPosition)
            {
                var dst = (DragObject.transform.position - align).magnitude;
                if(dst<minDst)
                {
                    minDst = dst;
                    minAlign = align;
                }
            }
            MoveTo.Start(new MoveTo.MoveOptions(DragObject, (minAlign - DragObject.transform.position), AlignTime));
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
        }
	}
    private void OnMouseDown()
    {
        if (mouseHold)
            return;
        mouseHold = true;
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 500, 1 << 8))
        {
            holdPos = hit.point;
            startPos = DragObject.transform.position;
        }
    }
    private void OnMouseUp()
    {
        mouseHold = false;
    }
    private void OnMouseDrag()
    {
    }
}
