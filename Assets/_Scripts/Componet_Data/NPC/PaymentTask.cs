using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaymentTask : MonoBehaviour {
    public void payPlayer(int paymentAmount) {
        GameObject player = GameObject.FindWithTag("Player");
        player.GetComponent<PlayerController>().changeMoney(paymentAmount);
    }
}
