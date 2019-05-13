using UnityEngine;
using pokoro;

public class TestStackState : StackState
{
	public override void OnEnterState()
	{
		Debug.Log("TestStackState Entered!");
	}
	public override void OnUpdate()
	{
		Debug.Log("TestStackState Updating!");
	}
	public override void OnExitState()
	{
		Debug.Log("TestStackState Exited!");
	}
	public override void OnCoverState()
	{
		Debug.Log("TestStackState Covered!");
	}
	public override void OnUncoverState()
	{
		Debug.Log("TestStackState Uncovered!");
	}
}
