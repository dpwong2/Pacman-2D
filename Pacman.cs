using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pacman : MonoBehaviour {
    //Code used from unity forums
    // xstart 14.999 y start 6.967998
    public float  speed=4.0f;
    private Vector2 direction = Vector3.zero;
    private Node curr, target,previous;
    private Vector2 NextDirection;
    public int PacmanScore=0;
	// Use this for initialization
	void Start () {
        Node startPos = GetNodePos(transform.localPosition);
        if (startPos != null) { curr = startPos;  }
      //  direction = Vector2.left;
       // changepos(direction);

	}
	
	// Update is called once per frame
	void Update () {
        CheckInput();
        move();
        ori();
        /*
       // if (Input.GetAxis("Horizontal") !=0)
       // {
            if (Input.GetKeyDown("right")) { direction = new Vector3(1, 0, 1); transform.localRotation = Quaternion.Euler(0, 0, 0); transform.localScale = new Vector3(1, 1, 1); }
            if (Input.GetKeyDown("left"))  { direction = new Vector3(-1, 0, 1); transform.localRotation = Quaternion.Euler(0, 0, 0); transform.localScale = new Vector3(-1, 1, 1); }
            
           // changepos(direction);move();
            //transform.localPosition +=new Vector3(Input.GetAxis("Horizontal")*speed*Time.deltaTime,0,0);
       // }
        // if (Input.GetAxis("Vertical") !=0)
        //{
            if (Input.GetKeyDown("up")) { direction = new Vector3(0, 1, 1);  transform.localRotation = Quaternion.Euler(0, 0, 90); transform.localScale = new Vector3(1, 1, 1); }
            if (Input.GetKeyDown("down")) { direction = new Vector3(0, -1, 1);  transform.localRotation = Quaternion.Euler(0, 0, -90); transform.localScale = new Vector3(1, -1, 1); }
            changepos(direction); move();
            // transform.localPosition += new Vector3(0,Input.GetAxis("Vertical") * speed * Time.deltaTime,0);
      //  }

        //transform.localScale = direction;
        */
    }
     Node GetNodePos(Vector2 pos)
    {
        GameObject tile = GameObject.FindWithTag("Game").GetComponent<GameBoard>().board[(int)pos.x, (int)pos.y];

        if(tile != null)
        {
           // Debug.Log("pos.x is "+ pos.x+"pos.y is "+pos.y+"tile is "+ tile);
            return tile.GetComponent<Node>();
        }
        return null;
    }
    Node validmove(Vector2 d)
    {
        Node next = null;
        for(int i=0;i<curr.friends.Length;i++)
        {
            //Debug.Log("moves "+curr.moves[i]+" d is "+d);
            
            if(curr.moves[i]==d)
            {
               
                next = curr.friends[i];break;
            }
        }
        return next;
    }
    void MoveToNode(Vector2 d)
    {
        
        Node next = validmove(d);
        if(next!=null)
        {
            transform.localPosition = next.transform.position;
            curr = next;
        }
       
    }
    void changepos(Vector2 d)
    {
        if (d != direction)
        { NextDirection = d; }
        if(curr!=null)
        {
            Node movetonode = validmove(d);
            if(movetonode!=null)
            {
                direction = d; target = movetonode;
                previous = curr;
                curr = null;
            }
        }
    }
    bool over()
    {
        float nodetotarget = lengthFromnode(target.transform.position);
        float self = lengthFromnode(transform.localPosition);
        return self > nodetotarget;
    }
    float lengthFromnode(Vector2 d)
    {
        Vector2 temp = d - (Vector2)previous.transform.position;
        return temp.sqrMagnitude;
    }
    void move()
    {
        if(target!=curr&&target!=null)
        {
            if(NextDirection==direction*-1)
            {
                direction *= -1;
                Node tempnode = target;
                target = previous;
                previous = tempnode;
            }
            if(over())
            {
                curr = target;
                transform.localPosition = curr.transform.position;
                GameObject otherport = getPort(curr.transform.position);//teleport
                if(otherport!=null)
                {
                    transform.localPosition = otherport.transform.position;
                    curr = otherport.GetComponent<Node>();
                }
                Node temp = validmove(NextDirection);
                if (temp != null) { direction = NextDirection; }
                if (temp == null) { temp = validmove(direction); }
                if(temp!=null)
                {
                    target = temp;
                    previous = curr;
                    curr = null;
                }
                else
                {
                    direction = Vector2.zero;
                }
               
                
            }
            else
            {
                transform.localPosition += (Vector3)(direction * speed) * Time.deltaTime;
            }
        }
    }
    void CheckInput()
    {
        if(/*Input.GetKeyDown("left")*/Input.GetAxis("Horizontal")<0)
        {
            changepos(Vector2.left);
        }
        if (/*Input.GetKeyDown("right")*/Input.GetAxis("Horizontal") > 0)
        {
            changepos(Vector2.right);
        }
        if (/*Input.GetKeyDown("down")*/Input.GetAxis("Vertical") < 0)
        {
            changepos(Vector2.down);
        }
        if (/*Input.GetKeyDown("up")*/Input.GetAxis("Vertical") > 0)
        {
            changepos(Vector2.up
);
        }

    }
    void ori()
    {
        if (direction == Vector2.left)
        {
            transform.localScale= new Vector3(-1,1,1);
            transform.localRotation = Quaternion.Euler(0,0,0);
        }
        if (direction == Vector2.right)
        {
            transform.localScale = new Vector3(1, 1, 1);
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        if (direction == Vector2.up)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            transform.localRotation = Quaternion.Euler(0, 0, 270);
        }
        if (direction == Vector2.down)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            transform.localRotation = Quaternion.Euler(0, 0, 90);
        }
    }
    GameObject getPort(Vector2 pos)
    {
        GameObject tile = GameObject.FindWithTag("Game").GetComponent<GameBoard>().board[(int)pos.x, (int)pos.y];
        if (tile != null)
        {
            if (tile.GetComponent<Tile>() != null)
            {

                if (tile.GetComponent<Tile>().portal)
                {
                    GameObject otherport = tile.GetComponent<Tile>().portalrec;
                    return otherport;
                }
            }
        }
        return null;
    }
    GameObject GetCurrentTile(Vector2 d)
    {
        return null;
    }
}

