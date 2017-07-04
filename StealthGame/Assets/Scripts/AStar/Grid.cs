using System;
using System.Collections;
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
	private GameObject[] visualNodes;

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


		visualNodes = new GameObject[Width * Length];
		for (int i = 0; i < Width * Length; i++) {
			visualNodes [i] = Instantiate (Prefab);
			visualNodes [i].GetComponent<MeshRenderer> ().enabled = false;
		
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
					visualNodes [n].transform.position = nodes[i, j].WorldPosition;
					visualNodes [n].transform.localScale *= 0.9f;
					visualNodes [n].GetComponent<MeshRenderer> ().enabled = true;
					n++;
				}
			}
		} else {
			for (int i = 0; i < Width * Length; i++) { // Loops GameObject array visualNodes
					visualNodes [i].GetComponent<MeshRenderer> ().enabled = false;
			}
		}
			
	}

	public List<Node> GetNeighbours(Node node){
		List<Node> neighbours = new List<Node>();
		int x = (int) node.GridPosition.x;
		int y = (int) node.GridPosition.y;
		/*
		if (x > 0 && x < Width - 1 && y > 0 && y < Length - 1) {
			neighbours.Add(nodes[x - 1, y - 1]);
		}
		*/

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
	public bool Walkable { get; set;}
	public int GCost { get; set;}
	public int HCost { get; set;}
	public int FCost { get{ return HCost + GCost; } }
	public Node Parent { get; set;}

	public Node(Vector2 gridPosition, Vector3 worldPosition, bool walkAble)
	{
		GridPosition = gridPosition;
		WorldPosition = worldPosition;
		Walkable = walkAble;
		Parent = null;
	}
}




/*
 * if (x > 0 && y > 0) {
			//Left Top
			neighbours.Add(nodes[x - 1, y + 1 ]);
		}
		if (y > 0) {
			//Top
			neighbours.Add(nodes[x, y + 1]);
		}
		if (x < Width - 1 && y > 0) {
			//Right Top
			neighbours.Add(nodes[x + 1, y + 1]);
		}
		if (x < Width - 1) {
			//Right
			neighbours.Add(nodes[x + 1, y]);
		}
		if (x < Width - 1 && y < Length - 1 && y > 0) {
			//Right Bottom
			neighbours.Add(nodes[x + 1, y - 1]);
		}
		if (y < Length - 1) {
			//Bottom
			neighbours.Add(nodes[x, y - 1]);
		}
		if (x > 0 && y < Length - 1) {
			//Left Bottom
			neighbours.Add(nodes[x - 1, y - 1]);
		}
*/