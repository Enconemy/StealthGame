using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StateContext{

	public iEnemyState CurrentState{
		get{ return currentState; }
		set{
			currentState = value;
			currentState.OnStateEntry (enemy);

		}

	}


	private iEnemyState currentState;
	private EnemyBehaviour enemy;
	private DetectPlayer detect;
	private float counter;

	public StateContext(EnemyBehaviour enemy, DetectPlayer detect)
	{
		this.enemy = enemy;
		this.detect = detect;
		CurrentState = EnemyStates.Patrol;
	}

	public void Update(){
		
		/*
		if (enemy.InRange == true) {
			CurrentState.HandleInput (E_Input.ReachPlayer, this);

		}

		else if(detect.detected == true) {
			CurrentState.HandleInput (E_Input.DetectedPlayer, this);
			counter = 0.0f;
		} 

		else if (detect.detected == false && counter >= 3.0f) {
			CurrentState.HandleInput (E_Input.NoVisionForTime, this);
			counter = 0.0f;
		}

		else if (detect.detected == false) {
			counter += Time.deltaTime;
			CurrentState.HandleInput (E_Input.NoVision, this);

		} 

		else if (enemy.InRange == false) {
			CurrentState.HandleInput (E_Input.OutOfRange, this);

		}
		*/
		currentState.Update (enemy);
	}
}
