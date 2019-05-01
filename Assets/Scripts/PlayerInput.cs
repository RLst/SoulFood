using UnityEngine;

namespace SoulFood
{
	/*-------------------------------
	Basic keyboard player input
	-------------------------------*/
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] bool isRaw = false;

        //Horizontal axis
		[Header("Horizontal Axis")]
		[SerializeField] bool invertHorizontal = false;
        [SerializeField] string horizontalAxisName = "Horizontal";
        public float xAxis { get; private set; }

        //Vertical axis
		[Header("Vertical Axis")]
		[SerializeField] bool invertVertical = false;
        [SerializeField] string verticalAxisName = "Vertical";
        public float yAxis { get; private set; }

		[Header("Buttons")]
        //Pickup/Drop
        [SerializeField] KeyCode pickupButton = KeyCode.K;
        public bool isPickingUp { get; private set; }

        //Action
        [SerializeField] KeyCode actionButton = KeyCode.J;
        public bool isAction { get; private set; }

        //Dash
        [SerializeField] KeyCode dashButton = KeyCode.L;
        public bool isDashing { get; private set; }

        //Jump
        [SerializeField] KeyCode jumpButton = KeyCode.Space;
        public bool isJumping { get; private set; }


        void Update()
        {
            HandleAxes();
            HandleButtons();
        }

        private void HandleButtons()
        {
            isPickingUp = Input.GetKey(pickupButton) ? true : false;
            isAction = Input.GetKey(actionButton) ? true : false;
            isDashing = Input.GetKey(dashButton) ? true : false;
            isJumping = Input.GetKey(jumpButton) ? true : false;
        }

        private void HandleAxes()
        {
            if (!isRaw)
            {
              	xAxis = invertHorizontal ? -Input.GetAxis(horizontalAxisName) : Input.GetAxis(horizontalAxisName);
                yAxis = invertVertical ? -Input.GetAxis(verticalAxisName) : Input.GetAxis(verticalAxisName);
            }
            else
            {
              	xAxis = invertHorizontal ? -Input.GetAxisRaw(horizontalAxisName) : Input.GetAxisRaw(horizontalAxisName);
                yAxis = invertVertical ? -Input.GetAxisRaw(verticalAxisName) : Input.GetAxisRaw(verticalAxisName);
            }

        }
    }
}