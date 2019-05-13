using System.Collections.Generic;
using UnityEngine;

namespace pokoro
{
	//Stack based state machine that runs based on the active state
	public class StackStateMachine : CoreStateMachine
	{
		Stack<StackState> states;

		//Current state is now directly linked to states stack
		public new StackState currentState 	//Hide State.currentState
		{
			get { return states.Peek(); }
		} 

		//--------------------------------
		internal override void Awake()
		{
			states = new Stack<StackState>();
		}
		
		//Stack new state on top
		public void Push(StackState state)
		{
			//If a state already exists, it's about to be COVERED
			currentState?.OnCoverState();

			states.Push(state);

			//New state ENTERED
			currentState.OnEnterState();
		}

		//Run current state each frame
		internal override void Update()
		{
			//Run the current top most state in the stack
			currentState.OnUpdate();
		}

		//Remove current state
		public State Pop()
		{
			//If there aren't any states then warn and exit
			if (!currentState)
			{
				Debug.LogWarning("No states to pop in state machine!");
				return null;
			}

			//Current state about to be EXITED
			currentState.OnExitState();
			var result = states.Pop();

			//Call state that was just UNCOVERED
			currentState.OnUncoverState();

			//Return the state that had just been popped
			return result;
		}

		//Clear the entire stack
		public void Clear()
		{
			states.Clear();
		}

	}
}