using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void openMenu(string name)
    {
        GameObject menu = GameObject.FindGameObjectWithTag(name);
        RectTransform rekt = menu.GetComponent<RectTransform>();
        //menu.gameObject.SetActive(true);
        //this.gameObject.SetActive(false);
        Debug.Log("Old x is " + rekt.anchoredPosition.x.ToString());
        rekt.anchoredPosition = new Vector2(rekt.anchoredPosition.x * -1, rekt.anchoredPosition.y);
        Debug.Log("New x is " + rekt.anchoredPosition.x.ToString());
    }
}
