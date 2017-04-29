using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoodbyeWorld : MonoBehaviour {

    public GameObject text;

	// Use this for initialization
	void Start () {
        text.GetComponent<Text>().text = "ENEMIES SPILT: " + PlayerPrefs.GetInt("Score");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
