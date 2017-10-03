using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScript : MonoBehaviour {
    public float Speed = 1f;
    public float WaitTime = 10;
    float endTime;
	// Use this for initialization
	void Start () {
        endTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        var dy = Time.deltaTime * Speed;
        transform.Translate(new Vector3(0, dy, 0));
        if (Time.time - endTime >= WaitTime)
        {
            GameObject.Find("GUI").GetComponent<GUI>().EndGame();
        }
	}
}
