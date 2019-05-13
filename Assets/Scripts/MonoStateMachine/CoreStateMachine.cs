using System.Collections.Generic;
using UnityEngine;

namespace pokoro
{
	//Basic state machine that only has ability to switch and get current state
	public class CoreStateMachine : MonoBehaviour
	{
		public virtual State currentState { get; private set; }

		//--------------------------------
		internal virtual void Awake()
		{
			//Init standard state list
		}

		//Run current state each frame 
		internal virtual void Update()
		{
			currentState?.OnUpdate();
		}

		//Change current state
		public virtual void Switch(State newState)
		{
			currentState?.OnExitState();

			currentState = newState;

			newState.OnEnterState();
		}

	}
}