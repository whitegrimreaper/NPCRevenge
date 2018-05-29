using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour {

	public float aggroDistance; 
	public float speed;
    public _Item[] loot;
    //public float boost;
    //public float rushTime;
    //public float cooldown;
    //public float orbit;
    
    public  int hp_max;
    private int hp_cur;

    //private float timeElapsed;
    //private bool go = true;

    //private EnemyWeaponScript weapon;
    //private bool attacking = false;

	private GameObject player;
	private Rigidbody2D rb;

    // Use this for initialization
    void Start() {
        hp_cur = hp_max;
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");

        //weapon = transform.GetChild(0).gameObject.GetComponent<EnemyWeaponScript>();
    }    
	
	// Update is called once per frame
	void Update () {
    	
        GetComponentInParent<Pathfinding.AIDestinationSetter>().target = getTarget();
        //boi he ded
        if (hp_cur <= 0) { 
            onDeath();
            Destroy(this.gameObject);
        }

        //timeElapsed += Time.deltaTime;
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        //frost_traps.jpg
        if (other.gameObject.tag == "Trap")
        {
            onDeath();
            //TODO: This should probably play animations instead of instantly destroying both
            Destroy(this.gameObject);
            Destroy(other.gameObject);
        }
    }

    //Returns Transform of target for pathfinding functions. This fcn will determine
    //a lot of how the AI works so I'll be working on it a lot
    public Transform getTarget()
    {
        GameObject target = GameObject.FindGameObjectWithTag("Player");
        return target.transform;
    }
    //self_documenting_code.jpg
    public void takeDamage(int amount){
        hp_cur -= amount;
    }
    
    //Drops loot on enemy death
    public void onDeath()
    {
        if (Random.Range(0, 3) == 2)
        {
            if (loot.Length > 0)
            {
                int itemNum = Random.Range(0, loot.Length);
                //GameObject item = Instantiate(loot[itemNum], this.gameObject.transform.position, Quaternion.identity) as GameObject;
            }
        }
    }   
}
