using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour {

    private PlayerController pc;

    public int value;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player" && this.gameObject.tag == "Item")
        {
            //Debug.Log("I can't believe you've done this");
            pc = coll.gameObject.GetComponent<PlayerController>();
            pc.changeMoney(value);
            Destroy(this.gameObject);
        }

        else if (coll.gameObject.tag == "Player" && this.gameObject.tag == "HealingItem")
        {
            pc = coll.gameObject.GetComponent<PlayerController>();
            pc.heal(value);
            Destroy(this.gameObject);
        }
        
    }
}
