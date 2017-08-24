using System;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {

	public int Width = 10;
	public int Length = 10;
	public float NodeSize = 1.0f;
	public bool ShowNodes = false;
	public GameObject Prefab;

	private bool oldShowNodes = false;
	private Node[,]nodes;
	private GameObject[,] visualNodes;

	// Use this for initialization
	private void Start () {
		nodes = new Node[Width, Length];
		Vector2 offset;
		offset.x = -(Width * NodeSize) / 2;
		offset.y = (Length * NodeSize) / 2;

        for (int i = 0; i < Width; i++) 
		{
			for (int j = 0; j < Length; j++) 
			{
                nodes [i, j] = new Node(new Vector2(i,j), new Vector3(offset.x + i * NodeSize, 0.0f, offset.y - j * NodeSize), true);
			}
		}


        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        Vector3 oScale;
        Vector3 oPos, nPos;
        Rect oRect, nRect;
    
        foreach(Node n in nodes)
        {
            nPos = n.WorldPosition;
            nRect = new Rect(nPos.x - NodeSize / 2, nPos.z - NodeSize / 2, NodeSize, NodeSize);
            foreach (GameObject g in obstacles)
            {
                oPos = g.transform.position;
                oScale = g.transform.localScale;
                oRect = new Rect(oPos.x - oScale.x / 2, oPos.z - oScale.z / 2, oScale.x, oScale.z);
                if(nRect.Overlaps(oRect))
                  n.IsWalkable = false;
            }
        }
        
        
        /*
        for (int i = 0; i < obstacles.Length; i++)
        {
            Vector3 tmp;
            Transform t = obstacles[i].transform;
            Vector3 pos = t.position;
            Vector3 scale = t.localScale;
            GetNodeAt(pos).IsWalkable = false;

            tmp = pos;
            float x = scale.x / 2.0f;


            tmp.x += x;
            GetNodeAt(tmp).IsWalkable = false;

            //if (scale.x - x > 0.0f)
            //x++;

            //for (int j = 0; j < x; j++)
            //{
            //    tmp.x += NodeSize;
            //    GetNodeAt(tmp).IsWalkable = false;
            //}
        }
        */


		visualNodes = new GameObject[Width, Length];
		for (int i = 0; i < Width; i++) {
            for (int j = 0; j < Length; j++)
            {
                visualNodes[i, j] = Instantiate(Prefab);
                visualNodes[i, j].GetComponent<MeshRenderer>().enabled = false;
                if (nodes[i, j].IsWalkable == false)
                    visualNodes[i, j].GetComponent<MeshRenderer>().material.color = Color.red;
            }
            
		}
		showNodes (ShowNodes);
	}
	private void showNodes(bool show) {
		if (show == true) {
			int n = 0;
			for (int i = 0; i < Width; i++) { // Loops Node array nodes, first field.
				for (int j = 0; j < Length; j++) { // Loops Node array nodes, second field.
					//Vector2 position = nodes [i, j].WorldPosition;
					//visualNodes [n].transform.position = new Vector3 (position.x, 0.0f, position.y);
					visualNodes [i, j].transform.position = nodes[i, j].WorldPosition;
					visualNodes [i, j].transform.localScale *= 0.9f;
					visualNodes [i, j].GetComponent<MeshRenderer> ().enabled = true;
					n++;
				}
			}
		} else {
			for (int i = 0; i < Width; i++) { // Loops GameObject array visualNodes
                for (int j = 0; j < Length; j++)
                {
                    visualNodes[i, j].GetComponent<MeshRenderer>().enabled = false;
                }
			}
		}
			
	}

	public List<Node> GetNeighbours(Node node){
		List<Node> neighbours = new List<Node>();
		int x = (int) node.GridPosition.x;
		int y = (int) node.GridPosition.y;

		if (x > 0 && y < Length - 1) {
			//Left Top
			neighbours.Add(nodes[x - 1, y + 1 ]);
		}
		if (y < Length - 1) {
			//Top
			neighbours.Add(nodes[x, y + 1]);
		}
		if (x < Width - 1 && y < Length - 1) {
			//Right Top
			neighbours.Add(nodes[x + 1, y + 1]);
		}
		if (x < Width - 1) {
			//Right
			neighbours.Add(nodes[x + 1, y]);
		}
		if (x < Width - 1 && y > 0) {
			//Right Bottom
			neighbours.Add(nodes[x + 1, y - 1]);
		}
		if (y > 0) {
			//Bottom
			neighbours.Add(nodes[x, y - 1]);
		}
		if (x > 0 && y > 0) {
			//Left Bottom
			neighbours.Add(nodes[x - 1, y - 1]);
		}

		return neighbours;
	}

    // Returns the node at the given world coordinates.
    public Node GetNodeAt(Vector3 worldPosition)
    {
        float extend = NodeSize / 2;

        Node node = null;
        for (int i = 0; i < Length; i++)
        {
            for (int j = 0; j < Width; j++)
            {
                Vector3 pos = GetNode(i, j).WorldPosition;
                if (worldPosition.x < pos.x + extend && worldPosition.x > pos.x - extend && worldPosition.z < pos.z + extend && worldPosition.z > pos.z - extend)
                {
                    node = GetNode(i, j);
                    goto finished;
                }
            }
        }

        finished:
        return node;
    }

	// Update is called once per frame
	private void Update () {
		if (oldShowNodes != ShowNodes) {
			showNodes (ShowNodes);
		}
		oldShowNodes = ShowNodes;
	}

	// Only for testing
	public Node GetNode(int x, int y)
	{
		return nodes [x, y];
	}
}


public class Node
{
	public Vector2 GridPosition{ get; private set;}
	public Vector3 WorldPosition{ get; private set;}
	public bool IsWalkable { get; set;}
	public int GCost { get; set;}
	public int HCost { get; set;}
	public int FCost { get{ return HCost + GCost; } }
	public Node Parent { get; set;}

	public Node(Vector2 gridPosition, Vector3 worldPosition, bool walkAble)
	{
		GridPosition = gridPosition;
		WorldPosition = worldPosition;
		IsWalkable = walkAble;
		Parent = null;
	}
}