using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealTask : MonoBehaviour {
    public void healPlayer(int healingAmount) {
        GameObject player = GameObject.FindWithTag("Player");
        player.GetComponent<PlayerController>().heal(healingAmount);
    }
}
