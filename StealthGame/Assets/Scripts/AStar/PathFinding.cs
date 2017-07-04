using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour {

	private List<Node> openList;
	private List<Node> closeList;
	private Grid grid;
	private int distance = 10;


	public List<Node> FindingPath(Node startNode, Node endNode){
		//openList.Add (startNode);
		Node currentNode = startNode;


		while(currentNode.GridPosition != endNode.GridPosition){
			List<Node> neighbours = grid.GetNeighbours(currentNode);
			for(int i = 0; i < neighbours.Count; i++){
				if (neighbours [i].Walkable == false) {
					closeList.Add(neighbours[i]);
				}
				if(closeList.Contains(neighbours[i]) == false){
					int gCost = currentNode.GCost + distance;

					if (neighbours [i].GCost == neighbours [i].FCost) {
						neighbours [i].HCost = manhattanDistance (neighbours [i], endNode);
					}
					if (openList.Contains (neighbours [i]) == true) {
						if(gCost < neighbours[i].GCost){
							neighbours [i].GCost = gCost;
							neighbours [i].Parent = currentNode;
						}		
					} else {
						neighbours [i].GCost = gCost;
						neighbours [i].Parent = currentNode;
						openList.Add(neighbours[i]);
					}
				}
			}
			closeList.Add (currentNode);
			openList.Remove (currentNode);

			/*
			for (int i = 0; i < openList.Count; i++) {
				openList [i].GCost = openList [i].Parent.GCost + distance;
				openList [i].HCost = manhattanDistance (openList[i], endNode);
			}
			*/

			if (openList.Count <= 0) {
				break;
			}
			int lowestFCost = openList [0].FCost;
			int index = 0;
			for (int i = 0; i < openList.Count; i++) {
				
				if (openList [i].FCost < lowestFCost) {
					lowestFCost = openList [i].FCost;
					index = i;
				}
			}
			//closeList.Add (openList[index]);
			//openList.Remove (openList[index]);

			currentNode = openList [index];
		}

		List<Node> path = new List<Node> ();
		Node n = endNode;
		path.Add (n);
		while (n.Parent != null) {
			if (path.Count > 200) {
				Debug.Log ("");
			}
			path.Add (n.Parent);
		}
		openList = new List<Node>();
		closeList = new List<Node> ();

		return path;
	}

	private int manhattanDistance(Node start, Node end)
	{
		return Mathf.Abs ((int)start.GridPosition.x - (int)end.GridPosition.x) + Mathf.Abs ((int)start.GridPosition.y - (int)end.GridPosition.y);
	}

	// Use this for initialization
	void Start () {
		grid = GameObject.Find ("A*").GetComponent<Grid> ();
		openList = new List<Node> ();
		closeList = new List<Node> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
