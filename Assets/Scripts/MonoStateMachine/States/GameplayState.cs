using UnityEngine;
using pokoro;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameplayState : UIState 
{
	[SerializeField] PauseMenuState pauseMenuState;
	public float pauseTimescale = 0;
	public float unpauseTimescale = 1f;
	
	
	// [SerializeField] UnityEvent OnStartGame, OnEndGame, OnCovered, OnUncovered;

	GameplayStateMachine gameplayStateMachine;

	internal override void Awake()
	{
		base.Awake();
	
		//Get reference to state machine
		gameplayStateMachine = GetComponent<GameplayStateMachine>();
	}
	
	void Start()
	{
		//Pause scene on startup
		Time.timeScale = pauseTimescale;		
	}

	public override void OnEnterState()
	{
		base.OnEnterState();    //show/hide gui
	
		Time.timeScale = unpauseTimescale;
	}
	public override void OnUncoverState()
	{
		base.OnUncoverState();  //show/hide gui
	
		Time.timeScale = unpauseTimescale;
	}

	public override void OnUpdate()
	{
		//Pause
		if (Input.GetKeyDown(KeyCode.P))
		{
			this.Pause();
		}
	}

	public override void OnCoverState()
	{
		base.OnCoverState();    //show/hide gui
		
		Time.timeScale = pauseTimescale;
	}
	public override void OnExitState()
	{
		base.OnExitState();     //show/hide gui
		
		Time.timeScale = pauseTimescale;
	}

	//----------------------------------------------------------
	public void Pause()
	{
		// OnPause.Invoke();	//Is this necessary since PauseState.OnEnterState() already has
		gameplayStateMachine.Stack(pauseMenuState);
	}

}
