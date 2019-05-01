using System;
using UnityEngine;

namespace SoulFood
{
    [RequireComponent(typeof(PlayerInput))]
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] float speed = 3f;
        [SerializeField] float dashSpeed = 20f;

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
            if (input.isPickup) Debug.Log("Picking up!");
			if (input.isAction) Debug.Log("Action!");
			if (input.isDash) Debug.Log("Dashing!");
            if (input.isJump) Debug.Log("Jumping!");

			if (input.wasPickup) Debug.Log("Picked up!");
            if (input.wasAction) Debug.Log("Actioned!");
            if (input.wasDash) Debug.Log("Dashed!");
            if (input.wasJump) Debug.Log("Jumped!");
        }

        void MoveCharacter()
        {
            var speedMultiplier = speed;

			//Dash
            if (input.wasDash)  //Can't hold the dash button to keep dashing
            {
                speedMultiplier = dashSpeed;
            }

            var movement = new Vector3(input.xAxis * speedMultiplier * Time.deltaTime, 0, input.yAxis * speedMultiplier * Time.deltaTime);
            cc.Move(movement);
        }
    }
}