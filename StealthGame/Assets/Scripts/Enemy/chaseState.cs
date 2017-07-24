public class ChaseState : iEnemyState {

	public void OnStateEntry (EnemyBehaviour enemy){}

	public void HandleInput (E_Input input, StateContext stateCon){
		switch (input)
        {
		case E_Input.ReachPlayer:
			stateCon.CurrentState = EnemyStates.Attack;
			break; 
			
		case E_Input.NoVision:
			stateCon.CurrentState = EnemyStates.Search;
			break;
		}
	}

	public void Update (EnemyBehaviour enemy){
		enemy.Chase ();
	}
}
