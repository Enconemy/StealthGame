using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public float Speed = 10;

    private Grid grid;
	private PathFinding pathFinder;
	private List<Node> path;

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

			WalkTo (pos);
		}
		if (path.Count > 0) {
			Vector3 targetPos = path [path.Count - 1].WorldPosition;
			targetPos.y = 1.0f;
			// tranforming Vector2 to Vector3
			//Vector3 targetPos = new Vector3 (tmpPos.x, 1.0f, tmpPos.y);

			if (targetPos != transform.position) {
				transform.position = Vector3.MoveTowards (transform.position, targetPos, Speed * Time.deltaTime);
			} else {
				path.RemoveAt (path.Count - 1);
			}
		}
	}

	void WalkTo(Vector3 targetPos)
	{
		if (targetPos.x < ((grid.Width * grid.NodeSize) / 2) - grid.NodeSize / 2 && targetPos.z < ((grid.Length * grid.NodeSize) / 2) - grid.NodeSize / 2) {
			//Debug.Log ("Inside");
		} else {
			//Debug.Log ("Outside");
		}

        Node target = grid.GetNodeAt(targetPos);
        Node start = grid.GetNodeAt(transform.position);

		if (start != null && target != null)
        {
			path = pathFinder.FindingPath (start, target);
            if(path  == null)
            {
                Debug.LogWarning("A* WARNING: No path found.");
            }
        }
        else
        {
            Debug.LogWarning("A* WARNING: Target or start not on the Grid.");
        }
	}
}
