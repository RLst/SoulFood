using ASM;
using UnityEngine;

namespace ASMTest
{
    [RequireComponent(typeof(Animator))]
	public class GameStateMachine : MonoBehaviour
	{
		Animator anim;
		
		[Header("Maybe these should be in some kind of array or dictionary")]
		public GameObject mainMenuUI;
		public GameObject optionsMenuUI;
		public GameObject gameUI;
		public GameObject pauseUI;
		public GameObject resultsUI;
		
		
		void Start()
		{
			mainMenuUI.SetActive(true);
		}
		

		void Awake()
		{
			anim = GetComponent<Animator>();
			this.ConfigureAllStateBehaviours(anim);
		}
		
		
		public void Options()
		{
			anim.SetTrigger("GoOptions");
		}
		public void ReturnToMainMenu()
		{
			anim.SetTrigger("GoReturnToMenu");
		}
		public void Pause()
		{
			anim.SetTrigger("GoPause");
		}
		public void StartGame()
		{
			anim.SetTrigger("GoGame");
		}
		public void FinishGame()
		{
			anim.SetTrigger("GoFinishGame");
		}
		public void Quit()
		{
			anim.SetTrigger("GoQuit");
		}
		
		
	}
}