using System.Collections.Generic;
using UnityEngine;

namespace pokoro
{
	//Basic state machine that only has ability to switch and get current state
	public class CoreStateMachine : MonoBehaviour
	{
		public virtual State currentState { get; private set; }

		//--------------------------------
		protected virtual void Awake()
		{
			//Init standard state list
		}

		//Run current state each frame 
		protected virtual void Update()
		{
			//If current state exists then UPDATE
			currentState?.OnUpdate();
		}

		//Change current state
		public virtual void Switch(State newState)
		{
			//If current state exists then EXIT
			currentState?.OnExitState();

			currentState = newState;

			newState.OnEnterState();
		}

	}
}