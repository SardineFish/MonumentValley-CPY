using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour {
    public Vector3[] AlignRotation;
    public GameObject RotateObject;
    public Vector3 Center;
    public Vector3 DirVector;
    public Vector3 RotateVector;
    public float AlignTime = 1;
    bool hold = false;
    float holdAngle;
    Vector3 startRotation;
    Vector3 holdPos;
	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        if (hold)
        {
            if (Input.GetMouseButtonUp(0))
            {
                hold = false;

                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 500, 1 << 11))
                {
                    var v = ToVector2(Multi((hit.point - Center), DirVector));
                    var angle = Mathf.Atan2(v.x, v.y) - holdAngle;
                    angle = (angle / (2 * Mathf.PI)) * 360;
                    var rotation = RotateVector * angle;
                    rotation = rotation + new Vector3(360, 360, 360);
                    rotation += startRotation;

                    var minDR = float.MaxValue;
                    var idx = 0;
                    for (var i = 0; i < AlignRotation.Length; i++)
                    {
                        var align = AlignRotation[i];
                        if (DR(align, rotation) < minDR)
                        {
                            minDR = DR(align, rotation);
                            idx = i;
                        }
                    }
                    var option = new RotateTo.RotateOptions(RotateObject, AlignRotation[idx], AlignTime);
                    option.OnFinished += (sender, e) =>
                    {
                        var callback = RotateObject.GetComponent<WaypointCallback>();
                        if (callback != null)
                            callback.ConnectCallback(idx);
                    };
                    RotateTo.Start(option);
                }
                transform.Find("SpinPlane").gameObject.SetActive(false);
            }
            else
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 500, 1 << 11))
                {
                    var v = ToVector2(Multi((hit.point - Center), DirVector));
                    var angle = Mathf.Atan2(v.x, v.y) - holdAngle;
                    angle = (angle / (2 * Mathf.PI)) * 360;
                    var rotation = RotateVector * angle;
                    rotation += startRotation;
                    RotateObject.transform.Rotate(rotation - RotateObject.transform.rotation.eulerAngles    );
                }
            }
        }
    }

    private void OnMouseDown()
    {
        transform.Find("SpinPlane").gameObject.SetActive(true);
        hold = true;
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        holdPos = new Vector3();
        if(Physics.Raycast(ray, out hit, 500, 1 << 11))
        {
            holdPos = hit.point;
            var v = ToVector2(Multi((hit.point - Center), DirVector));
            holdAngle = Mathf.Atan2(v.x, v.y);
            startRotation = RotateObject.transform.rotation.eulerAngles;
        }
    }

    Vector3 Multi(Vector3 n,Vector3 m)
    {
        return new Vector3(n.x * m.x, n.y * m.y, n.z * m.z);
    }

    Vector2 ToVector2(Vector3 v)
    {
        if (v.x == 0)
        {
            return new Vector2(v.y, v.z);
        }
        if(v.y==0)
        {
            return new Vector2(v.x, v.z);
        }
        if(v.z==0)
        {
            return new Vector2(v.x, v.y);
        }
        return new Vector2(0, 0);
    }

    public float DR(Vector3 n,Vector3 m)
    {
        var v = m - n;
        v = new Vector3(Mathf.Abs(v.x), Mathf.Abs(v.y), Mathf.Abs(v.z));
        v.x = v.x - (((int)(v.x / 360)) * 360);
        v.y = v.y - (((int)(v.y / 360)) * 360);
        v.z = v.z - (((int)(v.z / 360)) * 360);
        return v.magnitude;
    }
}
