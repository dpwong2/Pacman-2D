using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PelletDestroyer : MonoBehaviour {
    public int score;
	// Use this for initialization
	void Start () {
		
	}
     void OnTriggerEnter2D(Collider2D other)
    {
        this.GetComponent<SpriteRenderer>().enabled = false;
       // GameObject.FindWithTag("pacman").GetComponent<Pacman>().PacmanScore++;//should get score
            }
    // Update is called once per frame
    void Update () {
		
	}
}
