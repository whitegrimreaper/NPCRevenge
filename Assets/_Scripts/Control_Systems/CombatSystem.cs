using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatSystem : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//Returns the amount of damage done to the defender by the attacker
	public static int ResolveCombat(GameObject attacker, GameObject defender) {
		//references to attacker and defender stats
		Combat attacker_stats = attacker.GetComponent<Combat>();
		Combat defender_stats = defender.GetComponent<Combat>();

		//step variables for damage
		int a = attacker_stats.atk_; //atk + weapon power + misc
		int d = defender_stats.def_; //def + armor power + misc

		//damage dealt
		int damage = a - d; // (a - d) * (crit * res)
		
		//deal damage to defender
		defender_stats.hpc_ = Mathf.Clamp(defender_stats.hpc_ - damage, 0, defender_stats.hpm_);
		
		return damage;
	}
}
