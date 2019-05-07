using System;
using UnityEngine;
using pokoro;
using UnityEngine.Events;

namespace SoulFood
{
    //Character Controller based Player Motion Controller
    [RequireComponent(typeof(PlayerInput))]
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMoveControllerCC : MonoBehaviour
    {
        [Header("Move")]
        [SerializeField] float maxSpeed = 10f;


        [Header("Dash")]
        [SerializeField] float dashSpeed = 13f;
        [SerializeField] float dashDrag = 1.5f;
        private bool isDashing;
        float currentDashSpeed;


        [Header("Jump")]
        [SerializeField] float jumpSpeed = 23.5f;
        [SerializeField] float gravity = 3.5f;
        private bool isJumping;
        private Vector3 jumpMotion;


        // [Header("Events")]
        // UnityEvent OnJump;
        // OnLanded, OnDash, OnPickup, OnAction;


        [Header("IMGUI style")]
        [SerializeField] bool displayDebug = true;
        [SerializeField] GUIStyle style;


        //CACHES
        new Camera camera;      //Required so that movement aligns with the view
        PlayerInput input;
        CharacterController controller;



        void Awake()
        {
            camera = Camera.main;
            controller = GetComponent<CharacterController>();
            input = GetComponent<PlayerInput>();
        }

        void Start()
        {
            // OnJump.AddListener()
        }

        void Update()
        {
            MoveCharacter();
        }

        void OnGUI()
        {
            if (!displayDebug) return;
                
            GUILayout.Label("Controls: WASD Move. J Pick Up. K Action. L Dash. Space Jump", style);
            if (input.isPickup) GUILayout.Label("Picking up!", style);
			if (input.isAction) GUILayout.Label("Action!", style);
			if (input.isDash) GUILayout.Label("Dashing!", style);
            if (input.isJump) GUILayout.Label("Jumping!", style);

			if (input.wasPickup) GUILayout.Label("Picked up!", style);
            if (input.wasAction) GUILayout.Label("Actioned!", style);
            if (input.wasDash) GUILayout.Label("Dashed!", style);
            if (input.wasJump) GUILayout.Label("Jumped!", style);
        }

        void MoveCharacter()
        {
#region Move & Dash
            //Set basic speed
            var speedTotal = maxSpeed;

            //Calculate dash boost
            if (isDashing)   //Start reducing the dash boost (linearly) if currently dashing
            {
                currentDashSpeed -= dashDrag;
                if (currentDashSpeed < 0)
                {
                    //Stop dash status and zero out
                    isDashing = false;
                    currentDashSpeed = 0;
                }
            }
            if (input.wasDash && isDashing == false)  //Can't hold the dash button
            {
                // speedMultiplier = dashSpeed;
                isDashing = true;
                currentDashSpeed = dashSpeed;
            }
            //Add dash
            speedTotal += currentDashSpeed;

            //Calculate move motion vector
            Vector3 moveMotion = input.xAxis * camera.transform.RightSansYNormalized() * speedTotal + 
                                input.yAxis * camera.transform.ForwardSansYNormalized() * speedTotal;
#endregion

#region Jump
            //Calculate jump motion
            if (controller.isGrounded)
            {
                jumpMotion.y = -gravity;
                if (input.wasJump)
                {
                    // OnJump.Invoke();
                    jumpMotion.y = jumpSpeed;
                }
            }
            else 
            {
                jumpMotion.y -= gravity;
            }
#endregion

            //Combine all motion vectors and apply to player
            Vector3 resultantMotion = moveMotion + jumpMotion;
            controller.Move(resultantMotion * Time.deltaTime);
        }
    }
}