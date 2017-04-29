using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnemyCollision : MonoBehaviour {

	public GameObject player;
    public GameObject gameManager;

    void Start()
    {
        gameManager = GameObject.Find("GameManager");
    }
	
	void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.tag.Equals("Enemy"))
        {
            player.SendMessage("TakeDamage", 25.0f);
            Destroy(other.gameObject);
            GetComponent<AudioSource>().Play();
            gameManager.GetComponent<GameManager>().score++;
        }
	}
	
}
