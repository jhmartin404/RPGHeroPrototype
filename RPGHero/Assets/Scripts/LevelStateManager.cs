using UnityEngine;
using System.Collections;

//States for the level
public enum LevelState
{
	Start,
	Running,
	Lost,
	Won,
	Invalid
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
			fsm.DoState();
			break;
		case LevelState.Running:
			fsm.PushState(OnLevelRunningEvent);
			break;
		case LevelState.Start:
			fsm.PushState(OnLevelStartEvent);
			break;
		case LevelState.Won:
			fsm.PushState(OnLevelWonEvent);
			fsm.DoState();
			break;
		}
	}
	
	public static void PopState ()
	{
		fsm.PopState ();
	}

	public static LevelState GetCurrentState ()
	{
		LevelState currentLevelState = LevelState.Invalid;
		if(fsm.GetCurrentState().Equals(OnLevelStartEvent))
		{
			currentLevelState = LevelState.Start;
		}
		else if(fsm.GetCurrentState().Equals(OnLevelRunningEvent))
		{
			currentLevelState = LevelState.Running;
		}
		else if(fsm.GetCurrentState().Equals(OnLevelLostEvent))
		{
			currentLevelState = LevelState.Lost;
		}
		else if(fsm.GetCurrentState().Equals(OnLevelWonEvent))
		{
			currentLevelState = LevelState.Won;
		}
		return currentLevelState;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//execute the current state if level state is running
		if(fsm.GetCurrentState() == OnLevelRunningEvent)
		{
			fsm.DoState ();
		}
	}
}
