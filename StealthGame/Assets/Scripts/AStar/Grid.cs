using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {

	public int Width = 10;
	public int Length = 10;
	public float NodeSize = 1;
	public bool ShowNodes = false;
	public GameObject Prefab;

	private bool oldShowNodes = false;
	private Node[,] nodes;
	private GameObject[] visualNodes;

	// Use this for initialization
	void Start () {
		nodes = new Node[Width, Length];
		Vector2 offset;
		offset.x = -(Width * NodeSize) / 2;
		offset.y = (Length * NodeSize) / 2;

		for (int i = 0; i < Width; i++) 
		{
			for (int j = 0; j < Length; j++) 
			{
				bool b = true;
				nodes [i, j] = new Node(new Vector2(i,j), new Vector2(offset.x + i * NodeSize, offset.y - j * NodeSize), b);
			}
		}


		visualNodes = new GameObject[Width * Length];
		for (int i = 0; i < Width * Length; i++) {
			visualNodes [i] = Instantiate (Prefab);
			//visualNodes [i].AddComponent<MeshRenderer> ();
			visualNodes [i].GetComponent<MeshRenderer> ().enabled = true;
			//visualNodes [i].GetComponent<MeshRenderer> ().enabled = true;
		}
		showNodes (ShowNodes);
	}

	private void showNodes(bool show)
	{
		if (show == true) {
			for (int k = 0; k < visualNodes.Length; k++) { // Loops GameObject array visualNodes.
				for (int i = 0; i < Width; i++) { // Loops Node array nodes, first field.
					for (int j = 0; j < Length; j++) { // Loops Node array nodes, second field.
						bool enabled = visualNodes[k].GetComponent<MeshRenderer> ().enabled;
						Vector2 position = nodes [i, j].WorldPosition;
						visualNodes [k].transform.position = new Vector3 (position.x, 0.0f, position.y);
						visualNodes [k].transform.localScale *= 0.9f;
						visualNodes [k].GetComponent<MeshRenderer> ().enabled = true;
						enabled = visualNodes[k].GetComponent<MeshRenderer> ().enabled;
					}
				}
			}
		} else {
		for (int k = 0; k < Width * Length; k++) { // Loops GameObject array visualNodes
				visualNodes [k].GetComponent<MeshRenderer> ().enabled = false;
			}
		}
			
	}

	// Update is called once per frame
	void Update () {
		/*
		if (oldShowNodes != ShowNodes) {
			showNodes (ShowNodes);
		}
		oldShowNodes = ShowNodes;
		*/
	}
}


public class Node
{
	public Vector2 GridPosition{ get; private set;}
	public Vector2 WorldPosition{ get; private set;}
	public bool Walkable { get; set;}
	public int GCost { get; set;}
	public int HCost { get; set;}
	public int FCost { get{ return HCost + GCost; } }
	public Node Parent { get; set;}

	public Node(Vector2 gridPosition, Vector2 worldPosition, bool walkAble)
	{
		GridPosition = gridPosition;
		WorldPosition = worldPosition;
		Walkable = walkAble;
		Parent = null;
	}
}