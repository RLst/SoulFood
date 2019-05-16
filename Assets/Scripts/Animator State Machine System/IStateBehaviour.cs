using System;
using UnityEngine;

namespace ASM
{
    public interface IStateBehaviour<TStateMachine>
    {
        void InitializeWithContext(Animator animator, TStateMachine stateMachine);
        void Enable();
        void Disable();
    }
}