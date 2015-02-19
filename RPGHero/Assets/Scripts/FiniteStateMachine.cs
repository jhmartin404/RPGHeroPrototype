using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public delegate void OnState();


public class FiniteStateMachine
{
	Stack<OnState> states;

	public FiniteStateMachine ()
	{
		this.states = new Stack<OnState> ();
	}
	public void DoState ()
	{
		OnState currentStateFunction = GetCurrentState ();
		if (currentStateFunction != null)
			currentStateFunction ();
	}

	//Do state automatically without putting the state on the stack
	public void DoStateImmediate(OnState state)
	{
		state ();
	}

	//Pop the state on top off the stack
	public OnState PopState ()
	{
		return states.Pop ();
	}

	//Push state onto the stack if state is not the current state
	public void PushState (OnState state)
	{
		if (GetCurrentState () != state)
			states.Push (state);
	}

	//Get the current state without popping the state off
	public OnState GetCurrentState ()
	{
		return states.Count > 0 ? states.Peek() : null;
	}
}
