using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map2GameSystem : GameSystem {

    public override void GameEndingCallback()
    {
        NextMap();
        GameObject.Find("Map-2").SetActive(false);
        
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
