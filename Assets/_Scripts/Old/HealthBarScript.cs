using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour {

    private GameObject player;
    public RectTransform healthBar;
    //public float barDisplay; //current progress
    //public Vector2 pos = new Vector2(200, 400);
    //public Vector2 size = new Vector2(600, 200);
    //public Texture2D emptyTex;
    //public Texture2D fullTex;

    //void OnGUI()
    //{
    //    //draw the background:
    //    GUI.BeginGroup(new Rect(pos.x, pos.y, size.x, size.y));
    //    GUI.Box(new Rect(0, 0, size.x, size.y), emptyTex);

    //    //draw the filled-in part:
    //    GUI.BeginGroup(new Rect(0, 0, size.x * barDisplay, size.y));
    //    GUI.Box(new Rect(0, 0, size.x, size.y), fullTex);
    //    GUI.EndGroup();
    //    GUI.EndGroup();
    //}

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        //healthBar = this.GetComponentInChildren<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () {
        //if (!healthBar)
        //{
            //healthBar = this.GetComponentInChildren<RectTransform>();
        //}
        player = GameObject.FindGameObjectWithTag("Player");
        //barDisplay = Time.time * 0.05f;
        healthBar.sizeDelta = new Vector2(
            player.GetComponent<PlayerController>().getHP(),
            healthBar.sizeDelta.y);
    }
}
