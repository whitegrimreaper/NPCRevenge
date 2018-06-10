using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StashScript : MonoBehaviour {

    private bool open = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void interact()
    {
        if (!open)
        {
            this.GetComponent<Animator>().SetInteger("ChestAnimState", 1);
            open = true;
        }
        else
        {
            this.GetComponent<Animator>().SetInteger("ChestAnimState", 2);
            open = false;
        }
    }
}
