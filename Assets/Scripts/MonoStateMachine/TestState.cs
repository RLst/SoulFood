using pokoro;
using UnityEngine;

public class TestState : State
{
	public override void OnEnterState()
	{
		Debug.Log("TestState Entered!");
	}
	public override void OnUpdate()
	{
		Debug.Log("TestState Updating!");
	}
	public override void OnExitState()
	{
		Debug.Log("TestState Exited!");
	}
}
