using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public float Speed = 10;
	private Grid grid;


	private PathFinding pathFinder;
	private List<Node> path;
	//private Camera camera;

	// Use this for initialization
	void Start () {
		pathFinder = GameObject.Find ("A*").GetComponent<PathFinding> ();
		grid = GameObject.Find ("A*").GetComponent<Grid> ();
		path = new List<Node> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(1)){
			//Vector3 pos = camera.ScreenToWorldPoint (Input.mousePosition);
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			Vector3 pos = ray.origin + (ray.direction * Camera.main.transform.position.y);

			WalkPath (pos);
		}
		if (path.Count > 0) {
			Vector3 targetPos = path [0].WorldPosition;
			targetPos.y = 1.0f;
			// tranforming Vector2 to Vector3
			//Vector3 targetPos = new Vector3 (tmpPos.x, 1.0f, tmpPos.y);

			if (targetPos != transform.position) {
				transform.position = Vector3.MoveTowards (transform.position, targetPos, Speed * Time.deltaTime);
			} else {
				path.RemoveAt (0);
			}
		}
	}

	void WalkPath(Vector3 targetPos)
	{
		if (targetPos.x < ((grid.Width * grid.NodeSize) / 2) - grid.NodeSize / 2 && targetPos.z < ((grid.Length * grid.NodeSize) / 2) - grid.NodeSize / 2) {
			//Debug.Log ("Inside");
		} else {
			//Debug.Log ("Outside");
		}

		float size = grid.NodeSize / 2;
		Node node = null;
		for (int i = 0; i < grid.Length; i++) {
			for (int j = 0; j < grid.Width; j++) {
				//node = grid.GetNode (i, j);
				Vector3 pos = grid.GetNode (i, j).WorldPosition;
				if (targetPos.x < pos.x + size && targetPos.x > pos.x - size && targetPos.z < pos.z + size && targetPos.z > pos.z - size) {
					//Debug.Log("Node gefunden " + pos);
					//Debug.Log ("Target " + targetPos);
					node = grid.GetNode (i, j);
					goto finished;
				}
			}
		}

		finished:
		if (node != null) {
			Node start = grid.GetNode (0, 0);
			Node end = node;
			path = pathFinder.FindingPath (start, end);
		}
	}
}
