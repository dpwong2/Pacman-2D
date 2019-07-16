using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {
    public Node[] friends;
    public Vector2[] moves;
	// Use this for initialization
	void Start () {
        moves = new Vector2[friends.Length];
        for (int i =0;i<friends.Length;i++)
        {
            Vector2 distance = friends[i].transform.localPosition - transform.localPosition;
            moves[i] = distance.normalized;
            moves[i].x=Mathf.Round(moves[i].x);
            moves[i].y=Mathf.Round(moves[i].y);
        }
	}
	
	
}
