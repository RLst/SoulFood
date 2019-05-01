using System;
using UnityEngine;

namespace SoulFood
{
	[RequireComponent(typeof(PlayerInput))]
	[RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] float speed = 3f;

        CharacterController cc;
        PlayerInput input;

        void Start()
        {
            cc = GetComponent<CharacterController>();
            input = GetComponent<PlayerInput>();
        }

        void Update()
        {
            MoveCharacter();

			DebugActions();
        }

        void DebugActions()
        {
			if (input.isPickingUp)
				Debug.Log("Picking Up!");

			if (input.isJumping)
				Debug.Log("Jumping!");

			if (input.isDashing)
				Debug.Log("Dashing!");

			if (input.isAction)
				Debug.Log("Actioning!");
        }

        void MoveCharacter()
        {
            var movement = new Vector3(input.xAxis * speed * Time.deltaTime, 0, input.yAxis * speed * Time.deltaTime);
            cc.Move(movement);
        }


    }

}