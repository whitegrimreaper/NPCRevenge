using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSystem : MonoBehaviour {

	private	Movement[] movements_;

	// Use this for initialization
	void Start () {
		UpdateMovements();
		//Debug.Log(movements_);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//For Physics Updates
	void FixedUpdate(){
		//UpdateMovements();
		//Debug.Log(movements_.);
		foreach (Movement m in movements_) {
			m.gameObject.transform.Translate(m.forward_ * m.speed_ * Time.deltaTime);
		}
	}

	public void UpdateMovements(){
		movements_ = (Movement[]) Object.FindObjectsOfType(typeof(Movement));
	}
}
