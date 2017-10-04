using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_2_1_Start : MonoBehaviour {
    public GameObject StartPosition;
	// Use this for initialization
	void Start () {
        var player = GameObject.Find("Player");
        var options = new MoveTo.MoveOptions(player, StartPosition.transform.position, 1);
        options.OnFinished += (sender, e) =>
        {
            player.GetComponent<Player>().enabled = true;
            var spawn = GameObject.Find("Spawn");
            MoveTo.Start(new MoveTo.MoveOptions(spawn, new Vector3(0, -100, 0), 100));
        };
        MoveTo.Start(options);
	}

    // Update is called once per frame
    void Update () {
		
	}
}
