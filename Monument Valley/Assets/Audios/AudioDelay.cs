using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioDelay : MonoBehaviour {
    public float Delay = 0;
    float startTime = 0;
	// Use this for initialization
	void Start () {
        startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time-startTime >= Delay)
        {
            gameObject.GetComponent<AudioSource>().enabled = true;
            this.enabled = false;
        }
	}
}
