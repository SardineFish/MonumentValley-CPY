using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map3_1TowerRotation : MonoBehaviour {
    public float Speed = 1;
    // Use this for initialization
    void Start () {
        var options = new MoveTo.MoveOptions(gameObject, transform.position + new Vector3(0, -10, 0), 20);
        options.OnFinished += (sender, e) =>
        {
            GameObject.Find("GameSystem").GetComponent<GameSystem>().EndGame();
        };
        MoveTo.Start(options);
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.Rotate(new Vector3(0, -Time.deltaTime * Speed, 0));
        Speed += 0.3f;
    }
}
