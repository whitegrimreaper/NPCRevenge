using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectMap {
	public Color color;
	public GameObject prefab;
}

public class MapGenerator : MonoBehaviour {

	public Texture2D tiles;	   	   //image of level floor
	public Texture2D objects;      //image of level objects
    public Texture2D items;        //image of level items
	public ObjectMap[] objectKeys; //array that maps colors to objects (obstacles)
	public ObjectMap[] tileKeys;   //array that maps colors to tiles (ground)
    public ObjectMap[] itemKeys;   //array that maps colors to items

	private GameObject[,] tileRef;
	private GameObject[,] objectRef;
    private GameObject[,] itemRef;

	private GameObject tileParent;
	private GameObject objectParent;
    private GameObject itemParent;
    private GameObject spawnerParent;
    private GameObject pathfinder;

    public Sprite[] tileSprites;

	public GameObject GetTile(int row, int col){
		return tileRef[row, col];
	}

	public void GenerateLevel(){
		//create a 2D array to hold any instantiated tiles 
		tileRef = new GameObject[tiles.width, tiles.height];
		objectRef = new GameObject[tiles.width, tiles.height];
        itemRef = new GameObject[tiles.width, tiles.height];

		//loop through image
		for (int x = 0; x < tiles.width; x++){
			for (int y = 0; y < tiles.height; y++){
				//create tile corresponding to the pixel
				GenerateAtPosition(x, y);
			}
		}
	}

	void GenerateAtPosition(int x, int y){
		//create the tile based on the current pixel
		Color tilePixel = tiles.GetPixel(x, y);
		Color objectPixel = objects.GetPixel(x, y);
        Color itemPixel = items.GetPixel(x, y);

		//loop through tile keys
		foreach (ObjectMap tileKey in tileKeys) {
			if (tileKey.color.Equals(tilePixel)){
				tileRef[x, y] = (GameObject) Instantiate(tileKey.prefab, new Vector2(x, y), Quaternion.identity);
                //int rand = (int)Mathf.Floor(Random.Range(0, 4));
                tileRef[x, y].GetComponent<SpriteRenderer>().sprite = getRandomSprite("Grass");
                tileRef[x, y].transform.parent = tileParent.transform;
			}
		}

		//loop through object keys
		foreach (ObjectMap objectKey in objectKeys){
			if (objectPixel.a == 0){
				break;
			}
			else if (objectKey.color.Equals(objectPixel)){
				objectRef[x, y] = (GameObject) Instantiate(objectKey.prefab, new Vector2(x, y), Quaternion.identity);
                if(objectKey.prefab.name != "Player" && objectKey.prefab.name != "Spawner")
				objectRef[x, y].transform.parent = objectParent.transform;
			}
		}

        //loop through item keys
        foreach (ObjectMap itemKey in itemKeys)
        {
            if (itemPixel.a == 0)
            {
                
                break;
            }
            else if (itemKey.color.Equals(itemPixel))
            {
                //Debug.Log("re");
                itemRef[x, y] = (GameObject)Instantiate(itemKey.prefab, new Vector2(x, y), Quaternion.identity);
                itemRef[x, y].transform.parent = itemParent.transform;
            }
        }
    }

    public void UpdatePathfinding()
    {
        //pathfinder.GetComponent<AstarPath>().
        pathfinder.GetComponent<AstarPath>().Scan();
    }

	// Use this for initialization
	void Start () {
		tileParent = new GameObject("TileParent");
		objectParent = new GameObject("ObjectParent");
        itemParent = new GameObject("ItemParent");

        pathfinder = GameObject.FindGameObjectWithTag("Pathfinder");
        //for(int i = 0; i < 4; i++)
        //{
        //    tileSprites[i] = Resources.Load("Sprites/grass") as Sprite;
        //}
        //tileSprites = Resources.LoadAll<Sprite>("Sprites");
		
		GenerateLevel();
        UpdatePathfinding();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    Sprite getRandomSprite(string SpriteType)
    {
        switch (SpriteType)
        {
            case "Grass":
                //Debug.Log("Range is "+tileSprites.Length);
                return tileSprites[Random.Range(0,4)];
                
            default:
                return null;
        }

    }
}