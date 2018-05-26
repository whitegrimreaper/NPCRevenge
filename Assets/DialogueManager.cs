using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

    public Text nameText;
    public Text dialogueText;


    private bool dialogueActive;
    private Queue<string> sentences;

	// Use this for initialization
	void Start () {
        sentences = new Queue<string>();
        nameText = GameObject.FindGameObjectWithTag("Canvas").GetComponentsInChildren<Text>()[1];
        dialogueText = GameObject.FindGameObjectWithTag("Canvas").GetComponentsInChildren<Text>()[2];
        dialogueActive = false;
        

        nameText.text = "";
        dialogueText.text = "";
	}

    public void StartDialogue(Dialogue dialogue)
    {
        Debug.Log("Starting convo with " + dialogue.name);
        dialogueActive = true;

        nameText.text = dialogue.name;

        sentences.Clear();

        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
        Debug.Log(sentence);
    }

    void EndDialogue()
    {
        dialogueActive = false;
        //reee
    }
}
