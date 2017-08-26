using UnityEngine;

public class CameraMovement : MonoBehaviour {

	public float speed = 10;
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKey (KeyCode.A)) {
			transform.position = new Vector3 (transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
		}
		if (Input.GetKey (KeyCode.W)) {
			transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z + speed * Time.deltaTime);
		}
		if (Input.GetKey (KeyCode.S)) {
			transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z - speed * Time.deltaTime);
		}
		if (Input.GetKey (KeyCode.D)) {
			transform.position = new Vector3 (transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
		}
		if (Input.GetKey (KeyCode.LeftControl)) {
			transform.position = new Vector3 (transform.position.x, transform.position.y - speed * Time.deltaTime, transform.position.z);
		}
		if (Input.GetKey (KeyCode.LeftShift)) {
			transform.position = new Vector3 (transform.position.x, transform.position.y + speed * Time.deltaTime, transform.position.z);
		}
	}
}
