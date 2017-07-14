using System.Collections;
using System.Collections.Generic;


public interface iEnemyState  {
	void OnStateEntry (EnemyBehaviour enemy);
	void HandleInput (E_Input input, StateContext stateCon); 

	void Update (EnemyBehaviour enemy);

}

public static class EnemyStates{
	
	public static AttackState Attack = new AttackState();
	public static ChaseState Chase = new ChaseState();
	public static PatrolState Patrol = new PatrolState();
	public static SearchState Search = new SearchState();

}

public enum E_Input{DetectedPlayer, ReachPlayer, NoVision, OutOfRange, NoVisionForTime}