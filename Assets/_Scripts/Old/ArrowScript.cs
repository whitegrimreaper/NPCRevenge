using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour {

    private float timeToDie = 0.0f;
    private bool despawnb = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(despawnb)
        {
            timeToDie += Time.deltaTime;

            if (timeToDie >= .5f)
            {
                Destroy(this.gameObject);
            }
        }
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(this.GetComponent<Rigidbody2D>());
        Destroy(this.GetComponent<CapsuleCollider2D>());
        despawnb = true;
    }
}
