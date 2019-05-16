using System;
using UnityEngine;

namespace pokoro
{
	public class StateMachineController : MonoBehaviour
	{
		public CoreStateMachine stateMachine;

		// private event Action OnKeyPressed = delegate {};

		public State[] states;
		State currentState;

		void Awake()
		{
			stateMachine = GetComponent<CoreStateMachine>();
			//Init with the first state if present
		}

		void Start()
		{
			stateMachine.Switch(states[0]);
		}

		void Update()
		{
			// if (Input.anyKeyDown) OnKeyPressed.Invoke();

			if (Input.GetKeyDown(KeyCode.A))
			{
				stateMachine.Switch(states?[0]);
			}
			else if (Input.GetKeyDown(KeyCode.B))
			{
				stateMachine.Switch(states?[1]);
			}
		}

	}
}