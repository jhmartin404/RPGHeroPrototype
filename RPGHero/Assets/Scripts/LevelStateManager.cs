using UnityEngine;
using System.Collections;

//States for the level
public enum LevelState
{
	Start,
	Running,
	Lost,
	Won
};

public class LevelStateManager : MonoBehaviour 
{

	private static FiniteStateMachine fsm;//fsm to manage the state of the level
	public static event OnState OnLevelStartEvent;//Event for registering Start methods
	public static event OnState OnLevelRunningEvent;//Event for registering Running methods
	public static event OnState OnLevelLostEvent;//Event for registering Lost methods
	public static event OnState OnLevelWonEvent;//Event for registering Won methods
	
	void Awake()
	{
		fsm = new FiniteStateMachine ();
	}
	
	// Use this for initialization
	void Start () 
	{
		//Start the level
		fsm.PushState (OnLevelStartEvent);
		fsm.DoState ();
		//Enter Running State
		fsm.PushState (OnLevelRunningEvent);
	}
	
	public static void PushState (LevelState state)
	{
		switch(state)
		{
		case LevelState.Lost:
			fsm.PushState(OnLevelLostEvent);
			break;
		case LevelState.Running:
			fsm.PushState(OnLevelRunningEvent);
			break;
		case LevelState.Start:
			fsm.PushState(OnLevelStartEvent);
			break;
		case LevelState.Won:
			fsm.PushState(OnLevelWonEvent);
			break;
		}
	}
	
	public static void PopState ()
	{
		fsm.PopState ();
	}

	//public static LevelState GetCurrentState ()
	//{
	//	LevelState currentLevel;
	//	switch(fsm.GetCurrentState())
	//	{
	//	case OnLevelLostEvent:
	//		currentLevel = LevelState.Lost;
	//		break;
	//	case OnLevelRunningEvent:
	//		currentLevel = LevelState.Running;
	//		break;
	//	case OnLevelStartEvent:
	//		currentLevel = LevelState.Start;
	//		break;
	//	case OnLevelWonEvent:
	//		currentLevel = LevelState.Won;
	//		break;
	//	}
	//}
	
	// Update is called once per frame
	void Update () 
	{
		//execute the current state
		fsm.DoState ();
	}
}
