using UnityEngine;
using UnityEngine.SceneManagement;

public class Countdown : MonoBehaviour {

    public float StartTime = 180.0f;

    private float countdown = 0.0f;


	// Use this for initialization
	void Start () {
        countdown = StartTime;
	}
	
	// Update is called once per frame
	void Update () {
        countdown -= Time.deltaTime;
        if (countdown <= 0.0f)
        {
            SceneManager.LoadScene(2);
        }
	}

    void OnGUI()
    {
        GUI.TextField(new Rect(0,0,50, 20), countdown.ToString());
    }
}
