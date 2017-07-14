using System.Collections;
using System.Collections.Generic;

public class SearchState : iEnemyState {

	public void OnStateEntry (EnemyBehaviour enemy){

	}

	public void HandleInput (E_Input input, StateContext stateCon){
		switch (input) {
		case E_Input.DetectedPlayer:
			stateCon.CurrentState = EnemyStates.Chase;
			break; 

		case E_Input.NoVisionForTime:
			stateCon.CurrentState = EnemyStates.Patrol;
			break;
		}
	}

	public void Update (EnemyBehaviour enemy){
		enemy.Search ();
	}

}
