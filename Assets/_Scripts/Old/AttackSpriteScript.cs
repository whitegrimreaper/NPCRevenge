using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSpriteScript : MonoBehaviour {

	public float lifespan;
	public float knockback;
	public float speed;
	public Vector3 forward;
    public int damage;
    public int dropRate;

	private float timeElapsed = 0.0f;
	//private Rigidbody2D rb;
	// Use this for initialization
	void Start () {
		//rb = GetComponent<Rigidbody2D>();

		//Vector3 force = forward * Time.deltaTime * speed;
		//rb.AddForce(force, ForceMode2D.Impulse);
	}
	
	// Update is called once per frame
	void Update () {
		timeElapsed += Time.deltaTime;

		transform.position += forward * Time.deltaTime * speed; 

		if (timeElapsed > lifespan){
			Destroy(this.gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.tag == "Enemy"){
			other.gameObject.GetComponent<Rigidbody2D>().AddForce(forward * knockback, ForceMode2D.Impulse);
			other.gameObject.GetComponent<EnemyScript>().takeDamage(damage);
			Destroy(this.gameObject);
		}
		if (other.gameObject.tag == "Player"){
			other.gameObject.GetComponent<Rigidbody2D>().AddForce(forward * knockback, ForceMode2D.Impulse);
			other.gameObject.GetComponent<PlayerController>().takeDamage(damage);
			//Destroy(this.gameObject);
		}
        if (other.gameObject.tag == "Block")
        {
            //Debug.Log("REEEEEEEEEEEEEE");
            Destroy(this.gameObject);
        }
	}

    private void OnCollisionStay2D(Collision2D other)
    {
        if(other.gameObject.tag == "Block")
        {
            //Debug.Log("REEEEEEEEEEEEEE");
            Destroy(this.gameObject);
        }
    }

    public void setWeaponStats(Weapon weaponStats) {
        damage = weaponStats.damage;
        lifespan = weaponStats.range;
        speed = weaponStats.attackSpeed;
        this.gameObject.GetComponent<SpriteRenderer>().sprite = weaponStats.sprite;
    }
}
