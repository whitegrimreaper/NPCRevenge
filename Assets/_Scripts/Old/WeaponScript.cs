using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : ItemScript {

	public float cooldown;
	public GameObject attackSprite;

	private bool active;
	private float timeElapsed;

	private Vector3 forward;
	private Vector3 mouse;

	//used for the player attacking
	public void Attack(){
		if(!active){
			active = true;

			GameObject newAttack = (GameObject) Instantiate(attackSprite, transform.position + forward, Quaternion.identity);
			newAttack.GetComponent<AttackSpriteScript>().forward = forward;
			Physics2D.IgnoreCollision(transform.parent.gameObject.GetComponent<Collider2D>(), newAttack.GetComponent<Collider2D>());
		}
	}

	// Use this for initialization
	void Start () {
		timeElapsed = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {

		mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		forward = (new Vector3(mouse.x - transform.position.x, mouse.y - transform.position.y, 0.0f)).normalized;

		if (active && timeElapsed < cooldown){
			timeElapsed += Time.deltaTime;
		}
		else{
			active = false;
			timeElapsed = 0.0f;
		}
	}
}
