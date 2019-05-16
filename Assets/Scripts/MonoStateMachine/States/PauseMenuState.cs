using pokoro;
using UnityEngine;
using UnityEngine.Events;

public class PauseMenuState : UIState 
{
	public override void OnEnterState()
	{
		base.OnEnterState();
		
		//Pause
		Time.timeScale = 0;
	}
	
	public override void OnExitState()
	{
		base.OnExitState();
		
		//UnPause
		Time.timeScale = 1;
	}
}
