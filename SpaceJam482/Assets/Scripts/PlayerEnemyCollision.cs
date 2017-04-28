using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnemyCollision : MonoBehaviour {

	public GameObject player;
	
	void OnTriggerStay2D(Collider2D other) {
		player.SendMessage("TakeDamage", 1.0f);
	}
	
}
