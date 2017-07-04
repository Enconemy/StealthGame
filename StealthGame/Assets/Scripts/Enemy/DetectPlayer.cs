using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayer : MonoBehaviour {

	private bool detected = false;
	private float counter = 0;
	private float detectionTime = 2f;
	private GameObject player;
	private float rotationSpeed = 10.0f;

	public GameObject Enemy;

	void Start(){
		player = GameObject.FindGameObjectWithTag("Player");
	}

	void OnTriggerEnter(Collider col){
		if(col.tag == "Player"){
			Vector3 direction = col.transform.position - transform.position;
			//int layerMask = ~(2);
			bool noVision = Physics.Raycast (transform.position, direction, Vector3.Magnitude (direction)); //layerMask);
			if(!noVision)
				detected = true;
		}
	}


	void OnTriggerExit(Collider col){
		if(col.tag == "Player"){
			detected = false;
		}
	}

	void Update(){
		
		if (detected == true){
			counter += Time.deltaTime;
			if (counter >= detectionTime) {
				//Debug.Log ("YOU GOT DETECTED");
			}

			Vector3 direction = player.transform.position - Enemy.transform.position;
			direction.Normalize();
			float dot = Vector3.Dot (Enemy.transform.forward, direction);
			// Check if enemy is already pointing towards the player, with a treshold.
			if (dot < 0.95f) {
				Enemy.transform.Rotate(new Vector3(0.0f, rotationSpeed * Time.deltaTime, 0.0f));
				if (Vector3.Dot (transform.forward, direction) < dot) {
					Enemy.transform.Rotate(new Vector3(0.0f, -rotationSpeed * Time.deltaTime * 2.0f, 0.0f));
				}
			}
		} else{
			counter = 0;
		}
			
	}


}
