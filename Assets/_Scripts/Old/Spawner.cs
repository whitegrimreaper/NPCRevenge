﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Spawner : MonoBehaviour {

	public GameObject enemy;
    public GameObject enemy2;
    public GameObject enemy3;
	public float spawnInterval;
	private float timeElapsed = 0.0f;
	private float faster = 0.0f;
	private System.Random random;
	private int spawns = 0;
    private ManagerScript manager;

	// Use this for initialization
	void Start () {
		random = new System.Random(/*(int) Vector3.Angle(this.transform.position, Vector3.forward)*/);
		timeElapsed += random.Next(0, 4);

        manager = GameObject.FindGameObjectWithTag("Manager").GetComponentInChildren<ManagerScript>();
	}
	
	Vector3 RandomTranslate(){
		Vector3 newVector = new Vector3(random.Next(0, 5), random.Next(0, 5), 0.0f);
		return newVector.normalized;
	}

	// Update is called once per frame
	void Update () {
		Vector3 spawnMove = RandomTranslate();

        if(!manager.paused && !manager.UIPaused)
        {
            timeElapsed += Time.deltaTime;
        }

		if	(timeElapsed >= spawnInterval){
			GameObject newEnemy;
			newEnemy = (GameObject) Instantiate(enemy, this.transform.position + spawnMove, Quaternion.identity);
			newEnemy.transform.parent = this.transform;

            //spawnEnemy();

			timeElapsed = 0.0f;
            if(spawnInterval >= 5)
            {
                spawnInterval -= 1;
            }
		}	
	}

	public void Disable(){
		enabled = false;
	}
}
