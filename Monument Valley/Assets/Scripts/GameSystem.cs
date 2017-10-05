using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSystem : MonoBehaviour
{
    public GameObject GUI;
    public string ThisLevel;
    public string NextLevel;
    public LoadSceneMode NextLoadMode;

    public event EventHandler OnGameEnd;

    public void EndGame()
    {
        Time.timeScale = 0;
        GUI.transform.Find("EndMenu").gameObject.SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void NextMap()
    {
        SceneManager.LoadScene(NextLevel, NextLoadMode);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        GUI.transform.Find("PauseMenu").gameObject.SetActive(true);
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(ThisLevel, LoadSceneMode.Single);
    }

    public void ResumeGame()
    {
        GUI.transform.Find("PauseMenu").gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void StartGame()
    {
        throw new System.NotImplementedException();
    }

    public virtual void GameEndingCallback()
    {
        EndGame();
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
