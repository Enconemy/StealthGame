using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {

	public int Width = 10;
	public int Length = 10;
	public float NodeSize = 1;

	private Node[,] nodes;

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
	}
	
	// Update is called once per frame
	void Update () {
		
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
