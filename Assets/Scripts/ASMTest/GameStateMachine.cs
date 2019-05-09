using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AnimatorStateMachine;

namespace pokoro
{
	[RequireComponent(typeof(Animator))]
    public class GameStateMachine : MonoBehaviour
    {
		Animator anim;

		void Awake()
		{
			anim = GetComponent<Animator>();
			this.ConfigureAllStateBehaviours(anim);
		}
	}
}