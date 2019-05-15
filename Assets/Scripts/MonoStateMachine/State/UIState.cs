using UnityEngine;
using pokoro;

public abstract class UIState : StackState 
{
	public GameObject uiItem;

	public virtual void Awake()
	{
		//Always hide UI at the start
		uiItem.SetActive(false);
	}

	public override void OnEnterState()
	{
		//Show UI on enter
		uiItem.SetActive(true);
	}

	public override void OnExitState()
	{
		//Hide UI on exit
		uiItem.SetActive(false);
	}

}
