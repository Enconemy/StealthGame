using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour {

	private List<Node> openList;
	private List<Node> closeList;
	Grid grid;


	void findingPath(Node startNode, Node endNode){
		openList.Add (startNode);
		Node currentNode;

		while(currentNode == endNode){
			List<Node> neighbours = grid.GetNeighbours(currentNode);
			for(int i =0; i < neighbours.Count; i++){
				if (neighbours [i].Walkable == false) {
					closeList.Add(neighbours[i]);
				}
				if(closeList.Contains(neighbours[i]) == false){
					openList.Add(neighbours[i]);
				}
					
			}
			//openList.Add(grid.GetNeighbours(currentNode));

			
		}

	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
