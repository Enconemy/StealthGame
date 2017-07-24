public class PatrolState : iEnemyState {

	public void OnStateEntry (EnemyBehaviour enemy){}

	public void HandleInput (E_Input input, StateContext stateCon){
		if (input == E_Input.DetectedPlayer) {
			stateCon.CurrentState = EnemyStates.Chase;
		}
	}

	public void Update (EnemyBehaviour enemy){
		enemy.Patrol ();
	}
}
