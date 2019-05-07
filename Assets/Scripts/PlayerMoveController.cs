using System;
using UnityEngine;
using pokoro;

namespace SoulFood
{
    [RequireComponent(typeof(PlayerInput))]
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMoveController : MonoBehaviour
    {
        [Header("Move")]
        [SerializeField] float maxSpeed = 5f;


        [Header("Dash")]
        [SerializeField] float dashSpeed = 20f;
        [SerializeField] float dashReduction = 1f;
        private bool isDashing;
        float currentDashAmount;


        [Header("Jump")]
        [SerializeField] float jumpForce;
        private bool isJumping;
        private float gravity = 9.81f;
        float currentJumpAmount;

        [Header("IMGUI style")]
        [SerializeField] bool displayDebug = true;
        [SerializeField] GUIStyle debug;


        //CACHES
        new Camera camera;      //Required so that movement aligns with the view
        PlayerInput input;
        CharacterController controller;


        void Start()
        {
            camera = Camera.main;
            controller = GetComponent<CharacterController>();
            input = GetComponent<PlayerInput>();
        }

        void Update()
        {
            MoveCharacter();
        }

        void OnGUI()
        {
            if (!displayDebug)
                return;
                
            GUILayout.Label("Controls: WASD Move. J Pick Up. K Action. L Dash. Space Jump", debug);
            if (input.isPickup) GUILayout.Label("Picking up!", debug);
			if (input.isAction) GUILayout.Label("Action!", debug);
			if (input.isDash) GUILayout.Label("Dashing!", debug);
            if (input.isJump) GUILayout.Label("Jumping!", debug);

			if (input.wasPickup) GUILayout.Label("Picked up!", debug);
            if (input.wasAction) GUILayout.Label("Actioned!", debug);
            if (input.wasDash) GUILayout.Label("Dashed!", debug);
            if (input.wasJump) GUILayout.Label("Jumped!", debug);
        }

        void MoveCharacter()
        {
            //Set basic speed
            var speedTotal = maxSpeed;

			//// Dash
            if (isDashing)   //Start reducing the dash boost (linearly) if currently dashing
            {
                currentDashAmount -= dashReduction;
                if (currentDashAmount < 0)
                {
                    //Stop dash status and zero out
                    isDashing = false;
                    currentDashAmount = 0;
                }
            }
            if (input.wasDash && isDashing == false)  //Can't hold the dash button
            {
                // speedMultiplier = dashSpeed;
                isDashing = true;
                currentDashAmount = dashSpeed;
            }
            //Add dash
            speedTotal += currentDashAmount;

            //Calculate move motion vector
            Vector3 moveMotion = input.xAxis * camera.transform.RightSansYNormalized() * speedTotal + 
                                input.yAxis * camera.transform.ForwardSansYNormalized() * speedTotal;

            //Calculate jump motion vector
            Vector3 jumpMotion = Vector3.zero;  //TODO

            //Combine all motion vectors
            Vector3 finalMotion = moveMotion + jumpMotion;

            controller.Move(finalMotion * Time.deltaTime);
        }
    }
}