using pokoro;
using UnityEngine;

public class BState : State
{
	[SerializeField] Canvas UI;

	public override void OnEnterState()
	{
		Debug.Log("B state Entered!");
		UI.enabled = true;
	}
	public override void OnUpdate()
	{
		Debug.Log("B state ticking!");
	}
	public override void OnExitState()
	{
		Debug.Log("B state Exiting!");
		UI.enabled = false;
	}
}
