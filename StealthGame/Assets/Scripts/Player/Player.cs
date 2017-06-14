using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public float speed = 10;
	private Grid grid;

	private PathFinding pathFinder;
	private List<Node> path;

	// Use this for initialization
	void Start () {
		pathFinder = GameObject.Find ("A*").GetComponent<PathFinding> ();
		grid = GameObject.Find ("A*").GetComponent<Grid> ();
		WalkPath ();
	}
	
	// Update is called once per frame
	void Update () {
		if (path.Count > 0) {
			Vector2 pos = path [0].WorldPosition;
			transform.position =  new Vector3(pos.x, 0.0f, pos.y);
			path.RemoveAt (0);
		}
	}

	void WalkPath()
	{
		Node start = grid.GetNode (0, 0);//new Node ();
		Node end = grid.GetNode(50,50);
		path = pathFinder.FindingPath (start, end);
	}
}
