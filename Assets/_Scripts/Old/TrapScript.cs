using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void destroy()
    {
        Animator animator = GetComponent<Animator>();
        animator.SetInteger("OnDestroy", 10);
        AudioSource audio = GetComponent<AudioSource>();
        audio.Play();
        Destroy(this.gameObject, .8f);
    }

    
}
