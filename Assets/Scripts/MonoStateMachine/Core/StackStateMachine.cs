using System;
using System.Collections.Generic;
using UnityEngine;

namespace pokoro
{
	//Stack based state machine that runs based on the active state
	public abstract class StackStateMachine : CoreStateMachine
	{
		protected Stack<StackState> states = new Stack<StackState>();

		//Current state is now directly linked to states stack
		public new StackState currentState //Hide State.currentState
		{
			get { 
				if (states.Count > 0)    //NOTE!!! if stack is empty then Peek will throw exception
					return states.Peek(); 
				else
					return null;
			}
			
			protected set {
				currentState = value;
			}
		}

		//--------------------------------
		//Stack new state on top
		public void Push(StackState state)
		{
			if (state == null) {
				Debug.Log("Invalide state pass in!");
			}
		
			//If there is at least one state, it's about to be COVERED
			currentState?.OnCoverState();

			states.Push(state);

			//New state ENTERED
			state.OnEnterState();
		}
		public new void Switch(State state) //DANGER! Input state lacks StackState methods, maybe cause troubles down the line. Use Push() instead
		{
			Debug.LogError("This state machine is unable to use states of type 'State'");
		}

		//Run current state each frame
		internal override void Update()
		{
			//Run the current top most state in the stack
			currentState?.OnUpdate();
		}

		//Remove current state
		public StackState Pop() //TODO should this return State or StackState?
		{
			//If there aren't any states then warn and exit
			if (!currentState)
			{
				Debug.LogWarning("No states to pop in state machine!");
				return null;
			}

			//If it exists, current state about to be EXITED
			currentState?.OnExitState();
			var result = states.Pop();

			//If it exists, state underneath was just UNCOVERED
			currentState?.OnUncoverState();

			//Return the state that had just been popped
			return result;
		}

		//Clear all the states in the machine
		public void Clear()
		{
			states.Clear();
		}

	}
}