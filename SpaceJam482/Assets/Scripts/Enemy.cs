using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public GameObject blueShip;
    public GameObject redShip;
    public bool color;
    public bool target;
    public float speed = 4f;
    public float health = 100f;

    private bool beingDamaged = false;

    // Use this for initialization
    void Start () {
        blueShip = GameObject.Find("PlayerBlue");
        redShip = GameObject.Find("PlayerRed");
        int r = Random.Range(0, 2);
        if (r == 1)
        {
            // Blue
            color = true;
            gameObject.GetComponent<SpriteRenderer>().color = new Color32(51, 72, 210, 255);
        }
        else
        {
            // Red
            color = false;
            gameObject.GetComponent<SpriteRenderer>().color = new Color32(210, 51, 51, 255);
        }
        float distanceBlue = Vector3.Distance(transform.position, blueShip.transform.position);
        float distanceRed = Vector3.Distance(transform.position, redShip.transform.position);
        if (distanceBlue <= distanceRed)
        {
            target = true;
        }
        else
        {
            target = false;
        }

    }
	
	// Update is called once per frame
	void Update () {
        Vector2 pos = new Vector2(0, 0);
		if (target == true)
        {
            pos = blueShip.transform.position;

        }
        else
        {
            pos = redShip.transform.position;
        }
        Vector2 current = (pos - new Vector2(transform.position.x, transform.position.y)).normalized * speed * Time.deltaTime;
        //transform.position = new Vector2(transform.position.x + current.x, transform.position.y + current.y);
        transform.position = new Vector2(transform.position.x, transform.position.y + current.y);

        if (beingDamaged)
        {
            health -= 50f * Time.deltaTime;
            if (health <= 0)
            {
                Destroy(this.gameObject);
            }
        }

        // Destroy after hitting the boundary
        if (this.transform.position.y <= -20) {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        print("Colliding");
        /* //Turn beingDamaged to true
        if (col.gameObject.tag.Equals("PurpleLaser"))
        {
            beingDamaged = true;
        }
        //Red
        if (col.gameObject.tag.Equals("RedLaser") && !color)
        {
            beingDamaged = true;
        }
        //Blue
        if (col.gameObject.tag.Equals("BlueLaser") && color)
        {
            beingDamaged = true;
        } */

        // Only damage if right laser color
        if (col.gameObject.tag == "PurpleLaser" || (col.gameObject.tag == "RedLaser" && !color) 
            || (col.gameObject.tag == "BlueLaser" && color)) {
                health -= 10f * Time.deltaTime;
        }

        // DEATH
        if (health <= 0) {
            Destroy(this.gameObject);
        }
    }

    /* void OnTriggerExit(Collider col)
    {
        //Turn beingDamaged to false
        if (col.gameObject.tag.Equals("PurpleLaser"))
        {
            beingDamaged = false;
        }
        //Red
        if (col.gameObject.tag.Equals("RedLaser") && !color)
        {
            beingDamaged = false;
        }
        //Blue
        if (col.gameObject.tag.Equals("BlueLaser") && color)
        {
            beingDamaged = false;
        }
    } */
}
