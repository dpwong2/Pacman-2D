using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostScript : MonoBehaviour {
    // Used Weeklycoders code
    //used unity documentation/ Forums
    public float speed = 3.0f;
    public Node StartPos;
    public int scatterT = 7;
    public int chaseT = 20;
    public int ScatterModeTimer1 = 7;
    public int ChaseModeTimer1 = 20;
    public int ScatterModeTimer2 = 7;
    public int ChaseModeTimer2 = 20;
    public int ChaseModeTimer3 = 20;
    // public int ChaseModeTimer4 = 20;
    public int ScatterModeTimer3 = 5;
    public int ScatterModeTimer4 = 5;
    public enum mode
    {
        chase,
        scatter,
        frieghtened
    }
    mode currentmode = mode.scatter;
    mode previousmode;

    private GameObject pacman;
    private Node pacmanCurr, pacmanTarget, pacmanPrevious;
    private Vector2 direction, NDirection;
    private int modeChangeIteration = 1;
    private float modeChangeTime = 0;
    // Use this for initialization
    void Start() {
        pacman=GameObject.FindGameObjectWithTag("Pacman");
        Node node = GetNodePos(transform.localPosition);//was local
        if (node != null)
        {
            pacmanCurr = node;
        }
        pacmanPrevious = pacmanCurr;
        Vector2 PacPos = pacman.transform.position;//was position
        Vector2 TargetTile = new Vector2((PacPos.x), (PacPos.y));
      //  Debug.Log(TargetTile.x + " " + TargetTile.y);
        pacmanTarget = GetNodePos(TargetTile);
        //Debug.Log(pacmanTarget);
        direction = Vector2.right;
       
    }

    // Update is called once per frame
    void Update() {
        modeUpdate();
        move();
    }

    void modeUpdate()
    {
        if (currentmode != mode.frieghtened)
        {
            modeChangeTime += Time.deltaTime;
            if (modeChangeIteration == 1)
            {
                if (currentmode == mode.scatter && modeChangeTime > ScatterModeTimer1)
                {
                    changemode(mode.chase);
                    modeChangeTime = 0;
                }
                if (currentmode == mode.chase && modeChangeTime > ChaseModeTimer1)
                {
                    modeChangeIteration = 2;
                    changemode(mode.scatter);
                    modeChangeTime = 0;
                }
            }
            else if (modeChangeIteration == 2)
            {
                if (currentmode == mode.scatter && modeChangeTime > ScatterModeTimer2)
                {
                    changemode(mode.chase);
                    modeChangeTime = 0;
                }
                if (currentmode == mode.chase && modeChangeTime > ChaseModeTimer2)
                {
                    modeChangeIteration = 3;
                    changemode(mode.scatter);
                    modeChangeTime = 0;
                }
            }
            else if (modeChangeIteration == 3)
            {
                if (currentmode == mode.scatter && modeChangeTime > ScatterModeTimer3)
                {
                    changemode(mode.chase);
                    modeChangeTime = 0;
                }
                if (currentmode == mode.chase && modeChangeTime > ChaseModeTimer3)
                {
                    modeChangeIteration = 4;
                    changemode(mode.scatter);
                    modeChangeTime = 0;
                }
            }
            else if (modeChangeIteration == 4)
            {
                if (currentmode == mode.scatter && modeChangeTime > ScatterModeTimer4)
                {
                    changemode(mode.chase);
                    modeChangeTime = 0;
                }
            }
        }
        else if (currentmode == mode.frieghtened)
        {

        }

    }
    void move()
    {
        if(pacmanTarget!= pacmanCurr&&pacmanTarget!=null)
        {
            if (overshot())
            {
                //Debug.Log("Overshot");
                pacmanCurr = pacmanTarget;
                transform.localPosition = pacmanCurr.transform.position;
               /* GameObject otherPortal = GetPortal(pacmanCurr.transform.position);
                if (otherPortal != null)
                {
                    transform.localPosition = otherPortal.transform.position;
                    pacmanCurr = otherPortal.GetComponent<Node>();
                }*/
                pacmanTarget = ChooseNextNode();
               // Debug.Log(ChooseNextNode());
                pacmanPrevious = pacmanCurr;
                pacmanCurr = null;
            }
            else
            {
                //Debug.Log("move");
                transform.localPosition += (Vector3)direction * speed * Time.deltaTime;
            }
        }
    }
    void changemode(mode m)
    {
        currentmode = m;
    }

    Node ChooseNextNode()
    {
        Vector2 TargetTile = Vector2.zero;
        Vector2 PacPos = pacman.transform.position;
        TargetTile = new Vector2(Mathf.RoundToInt(PacPos.x),Mathf.RoundToInt(PacPos.y));

        Node MoveToNode = null;
        Node[] foundnodes = new Node[4];
        Vector2[] foundnodesdirection=new Vector2[4];

        int nodecounter = 0;
        for(int i =0;i<pacmanCurr.friends.Length;i++)
        {
            if(pacmanCurr.moves[i] != direction *-1)
            {
                foundnodes[nodecounter] = pacmanCurr.friends[i];
                foundnodesdirection[nodecounter] = pacmanCurr.moves[i];
                nodecounter++;
            }
        }
        if(foundnodes.Length==1)
        {
            MoveToNode = foundnodes[0];
            direction = foundnodesdirection[0];
        }
        if(foundnodes.Length>1)
        {
            float leastDistance = 100000f;
            for(int i =0;i<foundnodes.Length;i++)
            {
                if(foundnodesdirection[i]!= Vector2.zero)
                {
                    float distance = getdistance(foundnodes[i].transform.position,TargetTile);
                    if(distance<leastDistance)
                    {
                        leastDistance = distance;
                        MoveToNode = foundnodes[i];
                        direction = foundnodesdirection[i];
                    }
                }
            }
        }
        return MoveToNode;
    }
    Node GetNodePos(Vector2 pos)
    {
        //Debug.Log(pos.y);
        GameObject tile = GameObject.FindWithTag("Game").GetComponent<GameBoard>().board[(int)pos.x, (int)pos.y];
        //Debug.Log(tile);
        if (tile != null)
        {
            return tile.GetComponent<Node>();
        }
        return null;
    }
    GameObject GetPortal(Vector2 posi)
    {
        //Debug.Log(posi);
        GameObject tile = GameObject.Find("game").GetComponent<GameBoard>().board[(int)posi.x, (int)posi.y];
        Debug.Log("aftertile");
        if(tile!=null)
        {
            if(tile.GetComponent<Tile>().portal)
            {
                GameObject otherportal = tile.GetComponent<Tile>().portalrec;
                return otherportal;
            }
        }
        return null;
    }
   float LengthFromNode(Vector2 targetP)
    {
        Vector2 vec = targetP - (Vector2)pacmanPrevious.transform.position;
        return vec.sqrMagnitude;
    }
    bool overshot()
    {
        float targetNode = LengthFromNode(pacmanTarget.transform.position);
        float nodetoself = LengthFromNode(transform.localPosition);
        return nodetoself > targetNode;
    }

    float getdistance(Vector2 posA,Vector2 posB)
    {
        float dx = posA.x - posB.x;
        float dy = posA.x - posB.x;
        float distance = Mathf.Sqrt((dx * dx) + (dy * dy));
        return distance;

    }
}



