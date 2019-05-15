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
		public void Switch(StackState state)
		{
			this.Push(state);
		}
		public override void Switch(State state)	//DANGER! Input state lacks StackState methods, maybe cause troubles down the line. Use Push() instead
		{
			this.Push(state as StackState);	//What happens if state can't be casted to StackState?
		}

		//Run current state each frame
		internal override void Update()
		{
			//Run the current top most state in the stack
			currentState.OnUpdate();
		}

		//Remove current state
		public StackState Pop()		//TODO should this return State or StackState?
		// public void Pop()		//TODO should this return State or StackState?
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