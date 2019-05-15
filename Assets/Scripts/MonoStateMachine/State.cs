using System;
using UnityEngine;

namespace pokoro
{
//Stackable state
public abstract class State : MonoBehaviour
{
	public new string name;

	//Called AFTER the state has been placed on the state machine
	public virtual void OnEnterState() { }

	//Called right BEFORE the state has been removed rom the state machine
	public virtual void OnExitState() { }

	//Called every frame the state is active on the machine
	public virtual void OnUpdate() { }

	// ////Maybe abstract these two out on a StackState class?
	// //Called right BEFORE another state is stacked on top of this one
	// public virtual void OnCoverState() { }

	// //Called right AFTER this state has become the topmost state on the stack
	// public virtual void OnUncoverState() { }
}
}
