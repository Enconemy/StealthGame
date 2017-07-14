using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {

	public bool InRange = false;
	public Player p;
	private GameObject currentWay;
	public GameObject wayA;
	public GameObject wayB;
	public float speed = 5.0f;
	private StateContext stateCon;
	// Use this for initialization
	void Start () {

		currentWay = wayA;
		stateCon = new StateContext(this, GetComponentInChildren<DetectPlayer>());
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 direction = p.transform.position - transform.position;
		// length is squared
		float length = (direction.x * direction.x) + (direction.y * direction.y) + (direction.z * direction.z);

		if (length <= 1.0f) {
			InRange = true;
		} else {
			InRange = false;
		}

		stateCon.Update();
	}


	public void Patrol(){
		
		if (currentWay.transform.position != transform.position) {
			transform.position = Vector3.MoveTowards (transform.position, currentWay.transform.position, speed * Time.deltaTime);
		} else if (currentWay == wayA) {
			currentWay = wayB;
		} else {
			currentWay = wayA;
		}
	}

	public void Search(){
		
	}

	public void Chase(){
		
	}

	public void Attack(){
		
	}
}
