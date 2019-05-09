using System;
using System.Collections;
using UnityEngine;

namespace AnimatorStateMachine
{
    //Inherit this class to make your own state machine behaviours
    public class AdvancedStateMachineBehaviour<TStateMachine> : StateMachineBehaviour, IStateBehaviour<TStateMachine>
    {
        bool isActive = false;
        bool isEnabled = true;
        // TStateMachine mStateMachine;
        protected TStateMachine stateMachine { get; private set; }


        //StateMachineBehaviour Lifecycle
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            //Initialize the state
            if (isActive || !isEnabled) return;
            OnStateEntered();
            isActive = true;
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            //Shutdown the state
            if (!isActive || !isEnabled) return;
            isActive = false;
            OnStateExited();
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            //Run the state
            if (!isEnabled) return;
            OnStateUpdated();
        }

        //Animator does not call OnStateExit() upon being disabled or destroyed
        //Must handle it here
        void OnDisable()
        {
            DisableAndExit();
        }

        void DisableAndExit()
        {
            if (!isEnabled)
            {
                return;
            }

            if (isActive)
            {
                OnStateExited();
                isActive = false;
            }
            isEnabled = false;
        }


        //IStateBehaviour<TStateMachine> Implementation
        void IStateBehaviour<TStateMachine>.InitializeWithContext(Animator animator, TStateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
            OnInitialized();
        }

        void IStateBehaviour<TStateMachine>.Enable()
        {
            isEnabled = true;
        }

        void IStateBehaviour<TStateMachine>.Disable()
        {
            DisableAndExit();
        }


        //----------- Override these to implement your own logic ----------
        protected virtual void OnInitialized() { }

        protected virtual void OnStateEntered() { }

        protected virtual void OnStateExited() { }

        protected virtual void OnStateUpdated() { }
    }
}