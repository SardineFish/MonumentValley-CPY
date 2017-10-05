using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map2_1GameSystem : GameSystem
{
    public GameObject StartPosition;
    public GameObject Player;
    public override void GameEndingCallback()
    {
        var player = Player.GetComponent<Player>();
        player.Docking = true;
        player.dockedDepth = 0;
        var body = Player.transform.Find("Body").gameObject;
        var options = new MoveTo.MoveOptions(body, body.transform.position - new Vector3(0, player.DockDepth, 0), player.DockTime);
        options.OnFinished += (sender, e) =>
        {
            player.Docking = false;
            EndGame();
        };
        MoveTo.Start(options);
    }
    // Use this for initialization
    void Start ()
    {
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
