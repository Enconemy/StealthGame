using UnityEngine;

public class DetectPlayer : MonoBehaviour {

    public GameObject Enemy;
    public bool Detected { get; private set; }

	private float counter = 0;
	private GameObject player;
	private float rotationSpeed = 10.0f;

	void Start(){
		player = GameObject.FindGameObjectWithTag("Player");
        Detected = false;
	}

	void OnTriggerEnter(Collider col){
		if(col.tag == "Player"){
			Vector3 direction = col.transform.position - transform.position;
//??????? Testing with layer mask. Necessary???
            //int layerMask = ~(2);
			bool noVision = Physics.Raycast (transform.position, direction, Vector3.Magnitude (direction)); //layerMask);
			if(!noVision)
				Detected = true;
		}
	}


	void OnTriggerExit(Collider col){
		if(col.tag == "Player"){
			Detected = false;
		}
	}

	void Update(){
		
		if (Detected == true){
			counter += Time.deltaTime;

			Vector3 direction = player.transform.position - Enemy.transform.position;
			direction.Normalize();
			float dot = Vector3.Dot (Enemy.transform.forward, direction);
			// Check if enemy is already pointing towards the player, with a treshold.
			if (dot < 0.95f) {
				Enemy.transform.Rotate(new Vector3(0.0f, rotationSpeed * Time.deltaTime, 0.0f));
                // Check if enemy rotates in the correct direction.
                if (Vector3.Dot (transform.forward, direction) < dot) {
					Enemy.transform.Rotate(new Vector3(0.0f, -rotationSpeed * Time.deltaTime * 2.0f, 0.0f));
				}
			}
		} 
	}
}
