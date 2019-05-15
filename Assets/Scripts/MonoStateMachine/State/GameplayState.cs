using UnityEngine;
using pokoro;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameplayState : StackState 
{
	[SerializeField] GameObject uiItem;
	[SerializeField] PauseMenuState pauseMenuState;
	[SerializeField] UnityEvent OnStartGame, OnEndGame, OnPause;

	GameplayStateMachine gameplayStateMachine;

	void Awake()
	{
		//Get reference to state machine
		gameplayStateMachine = GetComponent<GameplayStateMachine>();
		// pauseMenuState = GetComponent<PauseMenuState>();
	}

	public override void OnEnterState()
	{
		//Enable controllers
		OnStartGame.Invoke();
		//Load scene?
	}

	public override void OnUpdate()
	{
		if (Input.GetKeyDown(KeyCode.P))
		{
			this.Pause();
		}
	}

	public override void OnUncoverState()
	{
		//Could delay or resume speed slowly for a few seconds here ie. from unpause so that player can get bearings etc.
	}

	public override void OnExitState()
	{
		//Disable controllers
		OnEndGame.Invoke();
	}

	//----------------------------------------------------------
	public void Pause()
	{
		OnPause.Invoke();	//Is this necessary since PauseState.OnEnterState() already has
		gameplayStateMachine.EnterState(pauseMenuState);
	}

	// public void RestartGame()
	// {
	// 	//Reload scene?
	// 	// var sceneToRestart = Application.loadedLevel;
	// 	var sceneToRestart = SceneManager.GetActiveScene();
	// 	SceneManager.LoadScene(sceneToRestart.buildIndex);
	// }

}
