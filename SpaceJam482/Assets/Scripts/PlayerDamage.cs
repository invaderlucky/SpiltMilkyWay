using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour {
    public GameObject playerShip;
    public float health = 200f;

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Enemy") {
            health -= 10f * Time.deltaTime;
        }

        if (health <= 0) {
            Destroy(playerShip);
        }
    }

}
