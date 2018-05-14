using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGenerator : MonoBehaviour {
    private GameObject canvasGO;
    private GameObject scoreTextGO;
    private GameObject itemBarGO;
    private GameObject npcMenuGO;
    private GameObject player;

    private bool ranStartUp = false;

    public Font myFont;
    public GameObject statusBar;
    public GameObject itemBar;
    public GameObject npcMenu;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");


        canvasGO = new GameObject("Canvas");
        canvasGO.tag = "Canvas";
        RectTransform canvasRT = canvasGO.AddComponent<RectTransform>();
        Canvas canvasCV = canvasGO.AddComponent<Canvas>();
        CanvasScaler scaler = canvasGO.AddComponent<CanvasScaler>();
        scaler.referencePixelsPerUnit = 1000;
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920,1080);
        GraphicRaycaster raycaster = canvasGO.AddComponent<GraphicRaycaster>();
        canvasCV.renderMode = RenderMode.ScreenSpaceOverlay;

        //  We can just instantiate a prefab like this, as long as we specify it takes the position and rotation of the
        //  object we're instantiating. Why it doesn't do this automatically is anyone's guess
        scoreTextGO = Instantiate(statusBar, statusBar.transform.position, statusBar.transform.rotation, canvasRT);
        scoreTextGO.GetComponent<RectTransform>().anchoredPosition = new Vector2(scoreTextGO.GetComponent<RectTransform>().anchoredPosition.x, -42);

        //item hotbar
        itemBarGO = Instantiate(itemBar, itemBar.transform.position, itemBar.transform.rotation, canvasRT);
        itemBarGO.GetComponent<RectTransform>().anchoredPosition = new Vector2(0.0f, -467.0f);

        //npc menu
        npcMenuGO = Instantiate(npcMenu, npcMenu.transform.position, npcMenu.transform.rotation, canvasRT);
        npcMenuGO.GetComponent<RectTransform>().anchoredPosition = new Vector2(0.0f, 0.0f);
    }

    // I originally added this function because Unity doesn't have a way to specify what order Start() functions are run,
    // so this just runs after Start() lmao
    void startUp()
    {
        
    }
	
	// Update is called once per frame
	void Update () {
        if(!ranStartUp)
        {
            startUp();
            ranStartUp = true;
        }

        if(!player)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        //scoreTextGO.GetComponent<Text>().text = "Money: " + player.GetComponent<PlayerController>().money.ToString();
	}
}


// HERE BE DEPRECATED CODE I DON'T WANT TO DELETE YET

//canvasGO.AddComponent(scoreTexts);



//scoreTextGO = new GameObject("ScoreText");
//scoreTextGO.transform.parent = GameObject.FindGameObjectWithTag("Canvas").transform;
//scoreTextGO = Resources.Load("Prefabs/ScoreText") as GameObject;


//RectTransform scoreRT = scoreTextGO.AddComponent<RectTransform>();
//Text textBox = scoreTextGO.AddComponent<Text>();
//scoreRT.SetParent(canvasRT);
//scoreRT.anchorMin = new Vector2(0, 1);
//scoreRT.anchorMax = new Vector2(0, 1);
//scoreRT.anchoredPosition = new Vector2(175f, -75f);
//scoreRT.sizeDelta = new Vector2(215, 50);
////scoreRT.SetPositionAndRotation(new Vector3(100, 668, 0), Quaternion.identity);
//Text scoreText = scoreTextGO.AddComponent<Text>();
//scoreText.font = myFont;
//scoreText.text = "Score: 0";
//scoreText.fontSize = 34;
//scoreText.color = new Color(0, 0, 0);