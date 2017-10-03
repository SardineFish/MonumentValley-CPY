using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GUI : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void PauseGame()
    {
        Time.timeScale = 0;
        transform.Find("PauseMenu").gameObject.SetActive(true);
    }

    public void EndGame()
    {
        //Time.timeScale = 0;
        transform.Find("EndMenu").gameObject.SetActive(true);
    }
    
    public void ResumeGame()
    {
        transform.Find("PauseMenu").gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void ShowBackground(Action callback)
    {
        transform.Find("Background").gameObject.GetComponent<ShowBackground>().Callback = callback;
        transform.Find("Background").gameObject.GetComponent<ShowBackground>().enabled = true;
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Map-1", LoadSceneMode.Single);
    }
}
