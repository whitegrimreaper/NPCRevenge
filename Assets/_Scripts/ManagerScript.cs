using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerScript : MonoBehaviour {
    private GameObject ting;

    public bool paused = false;
    public bool UIPaused = false;
    public bool playedTutorial = false;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		if(!playedTutorial)
        {
            this.GetComponent<DialogueTrigger>().TriggerDialogue();
            playedTutorial = true;
        }
	}

    public void pauseGame()
    {
        if (paused)
        {
            paused = false;
        }
        else
            paused = true;
    }

    public void pauseForUI()
    {
        if (UIPaused == true)
        {
            Debug.Log("Tried to pause for UI while already paused");
        }
        else
            UIPaused = true;
    }

    public void unUIPause()
    {
        if (UIPaused == false)
        {
            Debug.Log("Tried to unpause UI while not already paused");
        }
        else
            UIPaused = false;
    }
}
