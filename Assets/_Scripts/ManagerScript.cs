using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerScript : MonoBehaviour {
    private GameObject ting;

    public bool paused = false;
    public bool UIPaused = false;
    public bool playedTutorial = false;
    public bool nextRoundReady = true;

    public int currRound = -1;
    public bool roundActive = false;

    public GameFlow flow;

    public float timer;
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
        else if(playedTutorial && !roundActive && !UIPaused && nextRoundReady)
        {
            currRound++;
            roundActive = true;
            nextRoundReady = false;
            GameObject.FindGameObjectWithTag("Spawner").GetComponent<Spawner>().SpawnEnemy(flow.rounds[currRound].enemy);
            Debug.Log("Starting round " + currRound);
        }

        if(!nextRoundReady && !roundActive)
        {
            timer -= Time.deltaTime;
            if(timer < 0)
            {
                nextRoundReady = true;
            }
        }
	}

    public void enemyKilled()
    {
        roundActive = false;
        //nextRoundReady = false;
        timer = flow.rounds[currRound].postWaitTime;
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
