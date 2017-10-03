using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ShakeCamera : MonoBehaviour {
    public bool Enable = false;
    public float Range = 1;
    public float Strength = 1f;
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (Enable)
        {
            var x = Mathf.PerlinNoise(Time.time * Strength, 1);
            var y = Mathf.PerlinNoise(1, Time.time * Strength);
            x -= 0.5f;
            y -= 0.5f;
            x *= Range;
            y *= Range;
            transform.localPosition = new Vector3(x, y, 0);
        }
	}
}
