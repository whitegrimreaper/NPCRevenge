using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerDialogueHolder : MonoBehaviour {

    public Dialogue[] dialogues;
    private int idx;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void TriggerDialogue(int idx)
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogues[idx]);
    }
}
