using System;
using UnityEngine;

namespace pokoro
{
//Stackable state
public abstract class StackState : State
{
	//Called right BEFORE another state is stacked on top of this one
	public virtual void OnCoverState() { }

	//Called right AFTER this state has become the topmost state on the stack
	public virtual void OnUncoverState() { }
}
}
