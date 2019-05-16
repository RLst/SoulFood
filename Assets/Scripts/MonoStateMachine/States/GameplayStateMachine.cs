using UnityEngine;
using UnityEngine.SceneManagement;

namespace pokoro
{
	public class GameplayStateMachine : StackStateMachine
	{
		[SerializeField] StackState entryState;

		void Start()
		{
			this.Push(entryState);
		}

		// void LateUpdate()
		// {
		// 	Debug.Log("Timescale = " + Time.timeScale);
		// }

		//------------------ Transitions Assist Methods -------------------
		//Pop off the current state
		public void UnStack()
		{
			this.Pop();
		}

		//Push on a new state stack
		public void Stack(StackState state)
		{
			this.Push(state);
		}

		public void ReturnToMainMenu()
		{
			//Return to main menu hopefully
			while (this.currentState != entryState)
			{
				this.Pop();
				
				//Failsafe
				if (states.Count == 0)
				{
					Debug.LogWarning("No main menu state object found!");
					
					//Make a new entryState and get out
					currentState = entryState;
					return;
				}
			}
		}

		public void RestartGame()
		{
			//Reload scene?
			var sceneToRestart = SceneManager.GetActiveScene();
			SceneManager.LoadScene(sceneToRestart.buildIndex);
		}

		public void Quit()
		{
			Application.Quit();
		}
	}
}