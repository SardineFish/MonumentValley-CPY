using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map3GameSystem : GameSystem
{

    public override void GameEndingCallback()
    {
        NextMap();
        GameObject.Find("Map-3").SetActive(false);

    }

    // Use this for initialization
    void Start ()
    {
        Time.timeScale = 1;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
