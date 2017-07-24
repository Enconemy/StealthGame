using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {

	public bool InRange = false;
	public Player Player;
    public GameObject WayA;
    public GameObject WayB;
    public float Speed = 5.0f;
    public float RotationSpeed = 10.0f;
    public float ChaseBonus = 0.2f;
    public float ArrestingTime = 300.0f;
    public float ArrestingCountdown { get; set; }

    private GameObject currentWay;
	private StateContext stateCon;

	
	// Use this for initialization
	void Start () {
		currentWay = WayA;
		stateCon = new StateContext(this, GetComponentInChildren<DetectPlayer>());
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 direction = Player.transform.position - transform.position;
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
        // Check if Enemy has waypoints to go to.
        if (WayA != null || WayB != null)
        {
            if (currentWay.transform.position != transform.position)
            {
                transform.forward = Vector3.Normalize(currentWay.transform.position - transform.position);
                transform.position = Vector3.MoveTowards(transform.position, currentWay.transform.position, Speed * Time.deltaTime);
            }
            else if (currentWay == WayA)
            {
                currentWay = WayB;
            }
            else
            {
                currentWay = WayA;
            }
        }
	}

	public void Search(){
		transform.Rotate (new Vector3(0.0f, RotationSpeed * Time.deltaTime, 0.0f));
	}

	public void Chase(){
		transform.forward = Vector3.Normalize(Player.transform.position - transform.position);
		transform.position = Vector3.MoveTowards (transform.position, Player.transform.position, (Speed + ChaseBonus) * Time.deltaTime);
	}

	public void Attack(){
		ArrestingCountdown += Time.deltaTime;
		if (ArrestingCountdown >= ArrestingTime) {
			Debug.Log ("Arrested!");
		}
	}
}
