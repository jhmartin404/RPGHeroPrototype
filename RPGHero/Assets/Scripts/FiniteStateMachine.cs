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

	public void DoStateImmediate(OnState state)
	{
		state ();
	}
	public OnState PopState ()
	{
		return states.Pop ();
	}
	public void PushState (OnState state)
	{
		if (GetCurrentState () != state)
			states.Push (state);
	}
	public OnState GetCurrentState ()
	{
		return states.Count > 0 ? states.Peek() : null;
	}
}
