using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map3_1TowerRotation : MonoBehaviour {
    public float Speed = 1;
    // Use this for initialization
    void Start () {
        MoveTo.Start(new MoveTo.MoveOptions(gameObject, transform.position + new Vector3(0, 20, 0), 10));
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.Rotate(new Vector3(0, -Time.deltaTime * Speed, 0));
        Speed += 0.3f;
    }
}
