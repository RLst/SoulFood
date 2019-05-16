using UnityEngine;
using pokoro;
public class AState : State 
{
	[SerializeField] Canvas UI;

	public override void OnEnterState()
	{
		Debug.Log("A state Entered!");
		UI.enabled = true;
	}
	public override void OnUpdate()
	{
		Debug.Log("A state ticking!");
	}
	public override void OnExitState()
	{
		Debug.Log("A state Exiting!");
		UI.enabled = false;
	}
}