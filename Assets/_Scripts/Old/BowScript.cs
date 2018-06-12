using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowScript : MonoBehaviour {
    public Object arrow;

    public int currRotation = 0; //wow this is jank
    private double resetTime = 3.0;
    private double timeShot = 5.0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        timeShot += Time.deltaTime;
		if(Input.GetKeyUp(KeyCode.F))
        {
            GameObject player = GameObject.FindWithTag("Player");
            if (Vector3.Distance(this.gameObject.transform.position, player.transform.position) < 4 && timeShot >= resetTime) {
                fireArrow();
                timeShot = 0.0;
            }
        }
	}

    void fireArrow()
    {
        Vector2 offset = placementOffset();
        Vector2 force = findForce();
        GameObject arrowO = (GameObject)Instantiate(arrow, new Vector2(this.transform.position.x - offset.x, this.transform.position.y - offset.y), this.transform.rotation);
        arrowO.GetComponent<Rigidbody2D>().AddForce(new Vector2(force.x, force.y), ForceMode2D.Impulse);
    }

    Vector2 findForce()
    {
        int force = 20; //Magic
        Vector2 standard = new Vector2(-force, 0);
        if (currRotation == 3)
        {
            standard = new Vector2(0, force);
        }
        if (currRotation == 2)
        {
            standard = new Vector2(force, 0);
        }
        if (currRotation == 1)
        {
            standard = new Vector2(0, -force);
        }
        return standard;
    }

    Vector2 placementOffset()
    {
        float y_offset = 0.5f, x_offset = -0.5f;
        if (currRotation == 1)
        {
            y_offset -= 1;
        }
        else if (currRotation == 2)
        {
            y_offset -= 1;
            x_offset +=1;
        }
        else if (currRotation == 3)
        {
            x_offset += 1;
        }
        return new Vector2(x_offset, y_offset);
    }
    public void setRotation(int rotation)
    {
        currRotation = rotation;
    }
}
