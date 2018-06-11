using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    //stats;
    public  int hp_max; //Should be saved
    private int hp_cur; //Should be saved
    public  float invuln_time;
    private bool is_invuln = false;

	public Object Trap;
    public Object Shoot;
    public Object Block;
    public Object Enemy;
	public float speed;
    public float money = 100; // Should be saved
    public Text moneyText;
    public float facingPosition;
    public List<Weapon> weaponInventory = new List<Weapon>(); //Should be saved
    public List<Armor> armorInventory = new List<Armor>(); //Should be saved
    public List<Consumable> consumableInventory = new List<Consumable>(); //Should be saved

    private GameObject canvas;
    private ManagerScript manager;
    private Rigidbody2D rb;
    private WeaponScript weapon;
    private bool attacking = false;
    private float timeElapsed = 0.0f; // Might need to save this as well, might remove it with game flow
    private int currRotation = 0;
    private int weaponIndex = 0;
    private int armorIndex = 0;
    private int consumableIndex = 0;
    private bool onItem = false;
    private GameObject touchedItem;

    private Quaternion[] Rotations = new Quaternion[4];


    private Text ScoreText;

	// Use this for initialization
	void Start () {
        Camera.main.transform.parent = transform;
        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, -10.0f);

        Quaternion test = Quaternion.identity;
        Rotations[0] = test * Quaternion.Euler(0, 0, 0);
        Rotations[1] = test * Quaternion.Euler(0, 0, 90);
        Rotations[2] = test * Quaternion.Euler(0, 0, 180);
        Rotations[3] = test * Quaternion.Euler(0, 0, 270);


        // meme
        hp_cur = hp_max;

		rb = GetComponent<Rigidbody2D>();
        weapon = transform.GetChild(0).gameObject.GetComponent<WeaponScript>();

        canvas = GameObject.Find("Canvas");

        ScoreText = GameObject.FindGameObjectWithTag("Canvas").GetComponentInChildren<Text>();
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponentInChildren<ManagerScript>();
        setActiveWeapon();
   }

    void Update() {
        //Pauses/unpauses if there's no UI pause
        if(Input.GetKeyUp(KeyCode.P) && !manager.UIPaused)
        {
            manager.pauseGame();
        }
        if(manager.UIPaused && Input.GetKeyUp(KeyCode.Return))
        {
            //idk why i made this but i'm not removing it yet
        }

        //Since we need to be able to move through dialogue while UIpaused, this is up here
        if (Input.GetKeyUp(KeyCode.K))
        {
            FindObjectOfType<DialogueManager>().DisplayNextSentence();
        }

        // If we aren't paused at all, we can do normal shit
        if (!manager.paused && !manager.UIPaused) {
            var mousePos = Input.mousePosition;
            var objectPos = Camera.main.ScreenToWorldPoint(mousePos);

            //Cases used for character positioning
            //setFacingPositionMouse(mousePos);

            if (Input.GetMouseButtonUp(0) && money >= 10)
           {
                Instantiate(Trap, new Vector3(Mathf.Floor(objectPos.x), Mathf.Ceil(objectPos.y), 0), Quaternion.identity);
                changeMoney(-10);
           }

           if (Input.GetKeyUp(KeyCode.Alpha1) && money >= 50)
           {
                int x_offset = 0, y_offset = 0;

                //This block is super jank, it makes sure the bows get placed correctly
                if (currRotation == 1)
                {
                    y_offset = -1;
                }
                else if (currRotation == 2)
                {
                    y_offset = -1;
                    x_offset = 1;
                }
                else if (currRotation == 3)
                {
                    x_offset = 1;
                }
                // end jank block

                GameObject reference = (GameObject)Instantiate(Shoot, new Vector3(Mathf.Floor(objectPos.x) + x_offset, Mathf.Ceil(objectPos.y) + y_offset, 0), Quaternion.identity * Rotations[currRotation]);
                reference.GetComponent<BowScript>().setRotation(currRotation);
                changeMoney(-50);
            }

            if (Input.GetKeyUp(KeyCode.E))
            {
                if (currRotation == 0)
                {
                    currRotation = 3;
                }
                else
                {
                    currRotation--;
                }
            }

            if (Input.GetKeyUp(KeyCode.Q))
            {
                if (currRotation == 3)
                {
                    currRotation = 0;
                }
                else
                {
                    currRotation++;
                }
            }

            //use consumable
            if (Input.GetKeyUp(KeyCode.H)) {
                if (consumableInventory.Count > 0)
                    heal(consumableInventory[0].amount);
            }

            //switch weapons
            if (Input.GetKeyUp(KeyCode.R))  {
                if (weaponInventory.Count > 0) {
                    weaponIndex++;
                    if (weaponIndex >= weaponInventory.Count)
                        weaponIndex = 0;
                    
                    setActiveWeapon();
                }
            }

            //pick up items
            if (Input.GetKeyUp(KeyCode.F)) {
                if (onItem) {
                    if (touchedItem.tag == "Weapon" && weaponInventory.Count < 10) {
                        if (!weaponInventory.Contains((Weapon)touchedItem.GetComponent<LootContainer>().itemStats))
                            weaponInventory.Add((Weapon)touchedItem.GetComponent<LootContainer>().itemStats);

                        else
                            changeMoney(touchedItem.GetComponent<LootContainer>().itemStats.value);
                    }
                    else if (touchedItem.tag == "Armor" && armorInventory.Count < 5)
                        armorInventory.Add((Armor)touchedItem.GetComponent<LootContainer>().itemStats);

                    Destroy(touchedItem);
                    touchedItem = null;
                    onItem = false;
                }
            }

            //place block
            /*if (Input.GetMouseButtonUp(1) && money >= 10)
            {
                Instantiate(Block, new Vector3(Mathf.Floor(objectPos.x), Mathf.Ceil(objectPos.y), 0), Quaternion.identity);
                changeMoney(-10);

                AstarPath pathfinder = GameObject.FindGameObjectWithTag("Pathfinder").GetComponent<AstarPath>();
                pathfinder.Scan();
            }*/

            //NPC interaction
            if (Input.GetKeyUp(KeyCode.L) && Vector3.Distance(this.transform.position, GameObject.FindGameObjectWithTag("NPCInteraction").transform.position) < 3.0)
            {
                FindObjectOfType<DialogueTrigger>().TriggerDialogue();
            }

            if (Input.GetKeyUp(KeyCode.Z) && Vector3.Distance(this.transform.position, GameObject.FindGameObjectWithTag("Interactable").transform.position) < 2.0)
            {
                GameObject.FindGameObjectWithTag("Interactable").GetComponent<StashScript>().interact();
            }

            //attack
            if (Input.GetAxis("Jump") > 0.0f && !attacking) {
                weapon.Attack();
                attacking = true;
            }
            else if (Input.GetAxis("Jump") <= 0.0f) {
                attacking = false;
            }
        }

        if (hp_cur <= 0){
            //Camera.main.transform.parent = transform.root;
            //Destroy(this.gameObject);
            SceneManager.LoadScene("YOU DED");
        }

        if (is_invuln){
            timeElapsed += Time.deltaTime;
        }
        if(timeElapsed >= invuln_time){
            is_invuln = false;
            timeElapsed = 0.0f;
        }
    }

	// FixedUpdate is called once per frame
	void FixedUpdate () {
		float xMove = Input.GetAxis("Horizontal");
		float yMove = Input.GetAxis("Vertical");

		Vector2 movement = (new Vector3(xMove, yMove).normalized);
        setFacingPositionMove(xMove, yMove);
        //Vector2 velocity = movement * speed * Time.deltaTime/* * 10.0f*/;
        if (!manager.paused && !manager.UIPaused)
        {
            gameObject.transform.Translate(movement * speed * Time.deltaTime);

            //set onItem to false if true and player moved away
            if (onItem && (xMove > 0.0f || yMove > 0.0f)) {
                touchedItem = null;
                onItem = false;
                //Debug.Log("Off Item!\n");
            }
        }
        //rb.velocity = velocity;
    }

    void OnCollisionEnter2D(Collision2D other){
        if (other.gameObject.tag == "Enemy"){
            takeDamage(10);
            //I'm changing this line so it's not based on enemy velocity, that's kinda a dumb way to do it
            //at least in the final product
            //rb.AddForce(other.gameObject.GetComponent<Rigidbody2D>().velocity * 5.0f, ForceMode2D.Impulse);
            //In the future I'd like to make this dependent on a variable stored in the enemy's stats
            rb.AddForce(new Vector2(10.0f,10.0f), ForceMode2D.Impulse);
            is_invuln = true;
            //Debug.Log("Hit By enemy!!\n");
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Weapon" || other.gameObject.tag == "Armor") {
            //Debug.Log("On Item!\n");
            onItem = true;
            touchedItem = other.gameObject;
        }

        //auto pick up money
        else if (other.gameObject.tag == "Currency") {
            Currency money = (Currency)other.gameObject.GetComponent<LootContainer>().itemStats;
            changeMoney(money.value);
            Destroy(other.gameObject);
        }

        //auto pick up health potions
        else if (other.gameObject.tag == "Consumable") {
            Consumable healing = (Consumable)other.gameObject.GetComponent<LootContainer>().itemStats;
            if (hp_cur < hp_max) {
                heal(healing.amount);
                Destroy(other.gameObject);
            }
        }
    }

    public void changeMoney(int amount)
    {
        money += amount;
        if(!ScoreText)
        {
            ScoreText = GameObject.FindGameObjectWithTag("Canvas").GetComponentInChildren<Text>();
        }
        ScoreText.text = "Money: " + money.ToString();
        AudioSource audio = GetComponent<AudioSource>();
        if (amount > 0) {
            audio.Play();
        }
    }

    public void setActiveWeapon() {
        weapon.setWeaponStats(weaponInventory[weaponIndex]);
    }

    public void takeDamage(int amount){
        if (!is_invuln){
            hp_cur -= amount;
            is_invuln = true;
        }
    }

    public int getHP()
    {
        return hp_cur;
    }

    public void heal(int amount)
    {
        hp_cur += amount;

        if (hp_cur > hp_max)
            hp_cur = hp_max;
    }

    //Might do this later, don't delete. It's just wonky as hell and I don't feel like messing with it
    void setFacingPositionMouse(Vector2 mousePos)
    {
        Vector2 vec = mousePos - new Vector2(Screen.width*0.5f, Screen.height*0.5f);
        float angleRadians = Mathf.Atan2(vec.y, vec.x);
        float angleDegrees = angleRadians * Mathf.Rad2Deg;
        facingPosition = angleDegrees + 180;
        // 0 is at left side x axis, 90 is vertical
        if((facingPosition >= 0 && facingPosition <= 12.5) || (facingPosition <= 360 && facingPosition >= 347.5))
        {
            //Facing full left
        }
        else if(facingPosition >= 30 && facingPosition <= 60)
        {
            //upper left
        }
        else if(facingPosition >= 60 && facingPosition <= 90)
        {

        }
    }

    void setFacingPositionMove(float xAxis, float yAxis)
    {
        Animator anim = GetComponent<Animator>();
        if(xAxis > 0)
        {
            if(yAxis > 0)
            {
                //right and up
                anim.SetInteger("Facing", 4);
            }
            else if (yAxis == 0)
            {
                //right
                anim.SetInteger("Facing", 5);
            }
            else
            {
                //right and down
                anim.SetInteger("Facing", 6);
            }
        }
        else if (xAxis < 0)
        {
            if (yAxis > 0)
            {
                //left and up
                anim.SetInteger("Facing", 2);
            }
            else if (yAxis == 0)
            {
                //left
                anim.SetInteger("Facing", 1);
            }
            else
            {
                //left and down
                anim.SetInteger("Facing", 8);
            }
        }
        else
        {
            if(yAxis > 0)
            {
                //up
                anim.SetInteger("Facing", 3);
            }
            else
            {
                //down
                anim.SetInteger("Facing", 7);
            }
        }
    }
}
