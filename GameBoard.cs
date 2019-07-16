using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoard : MonoBehaviour {
    // code used from Weekly Coder's pacman tutorial
    // code used from unity documentation
    private static  int width = 45;//24
    private static int height = 45;//27

    public GameObject[,] board = new GameObject[width, height];
	// Use this for initialization
	void Start () {
        Object[] objects = GameObject.FindObjectsOfType(typeof(GameObject));
        foreach (GameObject k in objects)
        {
            Vector2 pos = k.transform.position;
            if(k.tag!="Pacman")
            {
              //  if (pos.x < 0) { Debug.Log((int)pos.x); }
                board[Mathf.Abs((int)pos.x), Mathf.Abs((int)pos.y)] = k;
            }
            else
            {
               // Debug.Log("Found Pacman at" + pos);
            }
        }

        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
