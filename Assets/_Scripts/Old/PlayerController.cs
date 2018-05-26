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

    private GameObject canvas;
    private ManagerScript manager;
    private Rigidbody2D rb;
    private WeaponScript weapon;
    private bool attacking = false;
    private float timeElapsed = 0.0f;
    private int currRotation = 0;

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
   }

    void Update() {
        //Pauses/unpauses if there's no UI pause
        if(Input.GetKeyUp(KeyCode.P) && !manager.UIPaused)
        {
            manager.pauseGame();
        }
        if(manager.UIPaused && Input.GetKeyUp(KeyCode.Return))
        {

        }

        // If we aren't paused at all, we can do normal shit
        if(!manager.paused && !manager.UIPaused) {
           if (Input.GetMouseButtonUp(0) && money >= 10)
           {
                var mousePos = Input.mousePosition;

                var objectPos = Camera.main.ScreenToWorldPoint(mousePos);
                Instantiate(Trap, new Vector3(Mathf.Floor(objectPos.x), Mathf.Ceil(objectPos.y), 0), Quaternion.identity);
                changeMoney(-10);
           }

           if (Input.GetKeyUp(KeyCode.Alpha1) && money >= 50)
           {
                int x_offset = 0, y_offset = 0;
                var mousePos = Input.mousePosition;

                var objectPos = Camera.main.ScreenToWorldPoint(mousePos);

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

            if (Input.GetMouseButtonUp(1) && money >= 10)
            {
                var mousePos = Input.mousePosition;

                var objectPos = Camera.main.ScreenToWorldPoint(mousePos);
                Instantiate(Block, new Vector3(Mathf.Floor(objectPos.x), Mathf.Ceil(objectPos.y), 0), Quaternion.identity);
                changeMoney(-10);

                AstarPath pathfinder = GameObject.FindGameObjectWithTag("Pathfinder").GetComponent<AstarPath>();
                pathfinder.Scan();
            }

            if (Input.GetKeyUp(KeyCode.L) && Vector3.Distance(this.transform.position, GameObject.FindGameObjectWithTag("NPCInteraction").transform.position) < 3.0)
            {
                FindObjectOfType<DialogueTrigger>().TriggerDialogue();
            }

            if (Input.GetKeyUp(KeyCode.K))
            {
                FindObjectOfType<DialogueManager>().DisplayNextSentence();
            }

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
        //Vector2 velocity = movement * speed * Time.deltaTime/* * 10.0f*/;
        if (!manager.paused && !manager.UIPaused)
        {
            gameObject.transform.Translate(movement * speed * Time.deltaTime);
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
            rb.AddForce(new Vector2(5.0f,5.0f), ForceMode2D.Impulse);
            is_invuln = true;
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
}
