using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathSystem : MonoBehaviour {

    private static DeathSystem instance = null;

    // Use this for initialization and enforcing singleton
    void Awake() {
        if (instance == null)
            instance = this;

        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public static void onDeath(GameObject actor) {
        if (actor.GetComponent<Death>().dropItems_)

        //play death animation
        

        Destroy(actor);
    }
}
