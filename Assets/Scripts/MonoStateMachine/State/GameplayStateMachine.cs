using UnityEngine;
using UnityEngine.SceneManagement;

namespace pokoro
{

	public class GameplayStateMachine : MonoBehaviour
	{
		public StackStateMachine gameplayStateMachine;

		// private event Action OnKeyPressed = delegate {};

		// [Header("First State MUST be Entry State ie. Main Menu")]

		[SerializeField] StackState entryState;

		// private StackState[] states;
		// StackState currentState;

		void Awake()
		{
			gameplayStateMachine = GetComponent<StackStateMachine>();
			gameplayStateMachine.Push(entryState);
			//Init with the first state if present
		}
		void Start()
		{

			// foreach (var s in states)
			// {
			// 	if (s.name == "MainMenu")
			// 	{
			// 		gameplayStateMachine.Push(s);
			// 	}
			// }
			// // stateMachine.Switch(states[0]);
		}

		//------------------ Transitions? -------------------
		public void GoBack()
		{
			gameplayStateMachine.Pop();
		}

		public void EnterState(StackState state)
		{
			gameplayStateMachine.Push(state);
		}

		public void Quit()
		{
			Application.Quit();
		}

		public void RestartGame()
		{
			//Reload scene?
			// var sceneToRestart = Application.loadedLevel;
			var sceneToRestart = SceneManager.GetActiveScene();
			SceneManager.LoadScene(sceneToRestart.buildIndex);
		}

		public void ReturnToMainMenu()
		{
			//Return to main menu hopefully
			while (gameplayStateMachine.currentState != entryState)
			{
				gameplayStateMachine.Pop();
			}
		}

		// void Update()
		// {
		// 	// if (Input.anyKeyDown) OnKeyPressed.Invoke();

		// 	if (Input.GetKeyDown(KeyCode.A))
		// 	{
		// 		gameplayStateMachine.Switch(states?[0]);
		// 	}
		// 	else if (Input.GetKeyDown(KeyCode.B))
		// 	{
		// 		gameplayStateMachine.Switch(states?[1]);
		// 	}
		// }
	}

}