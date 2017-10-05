using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map3_1GameSystem : GameSystem {

    public GameObject StartPosition;
    public GameObject Player;
    // Use this for initialization
    void Start ()
    {
        var options = new MoveTo.MoveOptions(Player, StartPosition.transform.position, 1);
        options.OnFinished += (sender, e) =>
        {
            Player.GetComponent<Player>().enabled = true;
        };
        MoveTo.Start(options);
    }
	
	// Update is called once per frame
	void Update () {

    }
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
}
