using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameObject playerLeft;
    public GameObject playerRight;
    public float speed;
    public GameObject laserRed;
    public GameObject laserBlue;
    public GameObject laserPurple;
    public GameObject enemy;
    public Transform[] spawnPoints;
    public bool spawnEnemies;


    // Use this for initialization
    void Start () {
        StartCoroutine(makeEnemies());
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        float horizontalLeft = Input.GetAxis("HorizontalLeft");
        float horizontalRight = Input.GetAxis("HorizontalRight");
        float verticalLeft = Input.GetAxis("VerticalLeft");
        float verticalRight = Input.GetAxis("VerticalRight");

        bool fireLeft = Input.GetButton("Fire1");
        bool fireRight = Input.GetButton("Fire2");

        if (horizontalLeft != 0.0f)
        {
            playerLeft.transform.position = new Vector2(speed * horizontalLeft * Time.deltaTime + playerLeft.transform.position.x, playerLeft.transform.position.y);
        }
        if (horizontalRight != 0.0f)
        {
            playerRight.transform.position = new Vector2(speed * horizontalRight * Time.deltaTime + playerRight.transform.position.x, playerRight.transform.position.y);
        }
        if (verticalLeft != 0.0f)
        {
            playerLeft.transform.position = new Vector2(playerLeft.transform.position.x, speed * verticalLeft * Time.deltaTime + playerLeft.transform.position.y);
        }
        if (verticalRight != 0.0f)
        {
            playerRight.transform.position = new Vector2(playerRight.transform.position.x, speed * verticalRight * Time.deltaTime + playerRight.transform.position.y);
        }

        float AngleRad = Mathf.Atan2(playerLeft.transform.position.y - playerRight.transform.position.y, playerLeft.transform.position.x - playerRight.transform.position.x);
        float AngleDeg = (180 / Mathf.PI) * AngleRad;
        playerRight.transform.rotation = Quaternion.Euler(0, 0, AngleDeg - 90);
        playerLeft.transform.rotation = Quaternion.Euler(0, 0, AngleDeg + 90);

        laserRed.SetActive(false);
        laserBlue.SetActive(false);
        laserPurple.SetActive(false);

        if (fireRight && !fireLeft)
        {
            FireRedLaser();
        }
        if (fireLeft && !fireRight)
        {
            FireBlueLaser();
        }
        if (fireLeft && fireRight)
        {
            FirePurpleLaser();
        }

        // rotate and resize each laser
        Quaternion r = playerLeft.transform.rotation;
        Vector3 p = new Vector3((playerRight.transform.position.x + playerLeft.transform.position.x) / 2.0f, (playerRight.transform.position.y + playerLeft.transform.position.y) / 2.0f, (playerRight.transform.position.z + playerLeft.transform.position.z) / 2.0f);
        Vector3 s = new Vector3(laserRed.transform.localScale.x, Vector2.Distance(playerLeft.transform.position, playerRight.transform.position), laserRed.transform.localScale.z);

        laserRed.transform.rotation = r;
        laserRed.transform.position = p;
        laserRed.transform.localScale = s;

        laserBlue.transform.rotation = r;
        laserBlue.transform.position = p;
        laserBlue.transform.localScale = s;

        laserPurple.transform.rotation = r;
        laserPurple.transform.position = p;
        laserPurple.transform.localScale = s;



    }

    void FireRedLaser()
    {
        laserRed.SetActive(true);
        //turn on sprite and hitbox
    }

    void FireBlueLaser()
    {
        laserBlue.SetActive(true);
        //turn on sprite and hitbox

    }

    void FirePurpleLaser()
    {
        laserPurple.SetActive(true);
        //turn on sprite and hitbox

    }

    IEnumerator makeEnemies()
    {
        while (spawnEnemies)
        {
            for (int i = 0; i < 1; i++)
            {
                int r = Random.Range(0, spawnPoints.Length);
                Instantiate(enemy, spawnPoints[r].position, Quaternion.identity);
            }
            yield return new WaitForSeconds(3.0f);
        }
    }
}
