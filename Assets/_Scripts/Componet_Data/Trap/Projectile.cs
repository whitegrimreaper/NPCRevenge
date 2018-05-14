using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    public int damage;
    public string damageType;
    private int range;
    private float effectDuration;
    private Vector3 startPos;

    // Use this for initialization
    void Start () {
	    startPos = gameObject.transform.position;	
	}
	
	// Update is called once per frame
	void Update () {
        if (Vector3.Distance(gameObject.transform.position, startPos) >= range)
            Destroy(this.gameObject);
	}

    public void setProjectile(int dmg, string dmgT, int r, float ed) {
        damage = dmg;
        damageType = dmgT;
        range = r;
        effectDuration = ed;
    }
}
