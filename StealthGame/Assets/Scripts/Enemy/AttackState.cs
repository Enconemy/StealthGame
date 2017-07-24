public class AttackState : iEnemyState {
	
	public void OnStateEntry (EnemyBehaviour enemy){
		enemy.ArrestingCountdown = 0.0f;
	}

	public void HandleInput (E_Input input, StateContext stateCon){
		if (input == E_Input.OutOfRange) {
			stateCon.CurrentState = EnemyStates.Chase;
		}
	}

	public void Update (EnemyBehaviour enemy){
		enemy.Attack ();
	}
}
