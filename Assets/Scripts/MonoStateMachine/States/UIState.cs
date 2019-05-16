using pokoro;
using UnityEngine;

public abstract class UIState : StackState
{
	public GameObject uiItem;

	internal virtual void Awake()
	{
		//Always hide UI at the start
		uiItem.SetActive(false);
	}

	//Show UI on enter and uncovered
	public override void OnEnterState()
	{
		uiItem.SetActive(true);
		Debug.Log("Entered " + uiItem.name.ToString());
	}
	public override void OnUncoverState()
	{
		uiItem.SetActive(true);
		Debug.Log("Uncovered " + uiItem.name.ToString());
	}

	//Hide UI on exit and covered
	public override void OnCoverState()
	{
		uiItem.SetActive(false);
		Debug.Log("Covered " + uiItem.name.ToString());
	}
	public override void OnExitState()
	{
		uiItem.SetActive(false);
		Debug.Log("Exited " + uiItem.name.ToString());
	}

}