using UnityEngine;
using ASM;

namespace ASMTest
{
    public class ResultsState : AdvancedStateMachineBehaviour<GameStateMachine>
    {
        GameStateMachine gsm;
    
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            gsm = animator.GetComponent<GameStateMachine>();
            
            //Display the UI
            gsm.resultsUI.SetActive(true);
        }
        
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            //Hide the UI
            gsm.resultsUI.SetActive(false);
        }
        
        // public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        // {
            
        // }
    }
}