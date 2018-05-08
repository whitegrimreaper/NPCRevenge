using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {

	public GameObject current_obj_;
	
	private Movement player_movement_;

	// Use this for initialization
	void Start () {
		player_movement_ = current_obj_.GetComponent<Movement>();
	}

	void Update () {
		if(current_obj_){
			Camera.main.transform.position = new Vector3(current_obj_.transform.position.x,
														current_obj_.transform.position.y,
														-10);
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//Take in player Inputs
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");
			
		Vector2 forward = (new Vector2(h, v)).normalized;

		player_movement_.forward_ = forward;
	}

	public void ReAssignCurrent(GameObject new_obj) {
		current_obj_ = new_obj;
	}
}
