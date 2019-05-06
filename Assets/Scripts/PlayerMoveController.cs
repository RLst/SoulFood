using System;
using UnityEngine;

namespace SoulFood
{
    [RequireComponent(typeof(PlayerInput))]
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMoveController : MonoBehaviour
    {
        [SerializeField] float maxSpeed = 5f;
        [SerializeField] float dashSpeed = 20f;
        [SerializeField] float dashReduction = 1f;
        // [SerializeField] float 

        float dashSpeedBoost;

        CharacterController cc;
        PlayerInput input;
        private bool isDashing;

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
            //Set basic speed
            var speedTotal = maxSpeed;

			//Dash
            if (isDashing)   //Start reducing the dash boost (linearly) if currently dashing
            {
                dashSpeedBoost -= dashReduction;
                if (dashSpeedBoost < 0)
                {
                    //Stop dash status and zero out
                    isDashing = false;
                    dashSpeedBoost = 0;
                }
            }
            if (input.wasDash)  //Can't hold the dash button
            {
                // speedMultiplier = dashSpeed;
                isDashing = true;
                dashSpeedBoost = dashSpeed;
            }
            //Add dash
            speedTotal += dashSpeedBoost;


            //Jump
            // if (input.wasJump)  //Can't hold the jump button
            // {
            //     jumpForce = 
            // }


            //Calculate speed multiplier
            var movement = new Vector3(input.xAxis * speedTotal * Time.deltaTime, 0, input.yAxis * speedTotal * Time.deltaTime);


            cc.Move(movement);
        }
    }
}