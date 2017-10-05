using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GUI : MonoBehaviour {

    public GameObject GameSystem;

	// Use this for initialization
	void Start () {
        Time.timeScale = 1;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void PauseGame()
    {
        GameSystem.GetComponent<GameSystem>().PauseGame();
    }

    public void EndGame()
    {
        GameSystem.GetComponent<GameSystem>().EndGame();
    }
    
    public void ResumeGame()
    {
        GameSystem.GetComponent<GameSystem>().ResumeGame();
    }

    public void ShowBackground(Action callback)
    {
        transform.Find("Background").gameObject.GetComponent<ShowBackground>().Callback = callback;
        transform.Find("Background").gameObject.GetComponent<ShowBackground>().enabled = true;
    }

    public void Restart()
    {
        GameSystem.GetComponent<GameSystem>().Restart();
    }

    public void Next()
    {
        GameSystem.GetComponent<GameSystem>().NextMap();
    }

    public void Exit()
    {
    }
}
