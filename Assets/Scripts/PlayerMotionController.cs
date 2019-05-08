using System;
using pokoro;
using UnityEngine;

namespace SoulFood
{
    //Rigidbody based player motion controller
    [RequireComponent(typeof(PlayerInput))]
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMotionController : MonoBehaviour
    {
        [Header("Move")]
        [SerializeField] float maxSpeed = 10f;


        [Header("Dash")]
        [SerializeField] float dashDistance = 1f;
        private bool isDashing;
        private Vector3 dashVelocity;
        // [SerializeField] float dashDrag = 1.5f;
        // [SerializeField] float dashSpeed = 13f;
        // float dashMotion;


        [Header("Jump")]
        [SerializeField] float jumpHeight = 1f;
        [SerializeField] LayerMask groundMask;
        [SerializeField] float groundCheckRadius = 0.2f;
        [SerializeField] Transform groundCheck;
        private bool isGrounded;
        private bool isJumping;
        // [SerializeField] float jumpForce = 23.5f;
        // [SerializeField] float gravity = 3.5f;
        // private Vector3 jumpMotion;


        [Header("IMGUI style")]
        [SerializeField] bool displayDebug = true;
        [SerializeField] GUIStyle style;


        //CACHES
        new Camera camera;      //Required so that movement aligns with the view
        PlayerInput input;
        Rigidbody rb;

        void Awake()
        {
            camera = Camera.main;   //TODO Danger!
            input = GetComponent<PlayerInput>();
            rb = GetComponent<Rigidbody>();
            // groundChecker = transform?.GetChild(0);
        }

        void Update()
        {
            HandleDashing();
            HandleJumping();
            HandleRotate();
        }

        void FixedUpdate()
        {
            HandleMovement();
        }

        private void HandleDashing()
        {
            if (input.wasDash && !isDashing)
            {
                //Start dash
                isDashing = true;
                //Trigger unity event etc

                //Calculate dash velocity vector
                var dashValue = new Vector3((Mathf.Log(1f / (Time.deltaTime * rb.drag + 1)) / -Time.deltaTime), 0, (Mathf.Log(1f / (Time.deltaTime * rb.drag + 1)) / -Time.deltaTime));
                dashVelocity = Vector3.Scale(transform.forward, dashDistance * dashValue);

                //Apply instant dash to 
                rb.AddForce(dashVelocity, ForceMode.VelocityChange);
            }
            if (dashVelocity.magnitude <= 0f)
            {
                //End dash
                isDashing = false;
                //Trigger unity event etc
            }
        }
        private void HandleJumping()
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundMask, QueryTriggerInteraction.Ignore);

            if (input.wasJump && isGrounded)
            {
                //Start jump
                isJumping = true;
                isGrounded = false;
                //trigger unity event etc
                rb.AddForce(Vector3.up * Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
            }
            if (isJumping == true && isGrounded)
            {
                //Landed; End Jump
                isJumping = false;
                //Trigger unity event etc
            }
        }

        private void HandleRotate()
        {
            //Do nothing
        }
        
        private void HandleMovement()
        {
            // Vector3 inputVector;
            // inputVector.x = input.xAxis * camera.transform.RightSansYNormalized().x;
            // inputVector.y = 0;
            // inputVector.z = input.yAxis * camera.transform.ForwardSansYNormalized().z;
            var moveMotion = input.xAxis * camera.transform.RightSansYNormalized() + input.yAxis * camera.transform.ForwardSansYNormalized();
            rb.MovePosition(rb.position + moveMotion * maxSpeed * Time.fixedDeltaTime);
        }

        void OnDrawGizmos()
        {
            //Draw ground check sphere
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
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
    }
}