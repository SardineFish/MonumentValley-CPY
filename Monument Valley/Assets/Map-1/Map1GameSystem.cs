using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map1GameSystem : GameSystem {
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
            GameObject.Find("Main Camera").GetComponent<ShakeCamera>().Enable = true;
            GameObject.Find("Map-End").GetComponent<EndScript>().enabled = true;
            GameObject.Find("Map-End").transform.Find("Blocks-Drop").gameObject.SetActive(true);
        };
        MoveTo.Start(options);
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
