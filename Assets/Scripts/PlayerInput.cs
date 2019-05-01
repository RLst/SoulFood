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
        public bool isPickup { get; private set; }
        public bool wasPickup { get; private set; }

        //Action
        [SerializeField] KeyCode actionButton = KeyCode.J;
        public bool isAction { get; private set; }
        public bool wasAction { get; private set; }

        //Dash
        [SerializeField] KeyCode dashButton = KeyCode.L;
        public bool isDash { get; private set; }
        public bool wasDash { get; private set; }

        //Jump
        [SerializeField] KeyCode jumpButton = KeyCode.Space;
        public bool isJump { get; private set; }
        public bool wasJump { get; private set; }


        void Update()
        {
            SetAxes();
            SetButtons();
        }

        private void SetButtons()
        {
			//Current button state
            isPickup = Input.GetKey(pickupButton) ? true : false;
            isAction = Input.GetKey(actionButton) ? true : false;
            isDash = Input.GetKey(dashButton) ? true : false;
            isJump = Input.GetKey(jumpButton) ? true : false;

			//Was button pressed?
			wasPickup = Input.GetKeyDown(pickupButton) ? true : false;
			wasAction = Input.GetKeyDown(actionButton) ? true : false;
			wasDash = Input.GetKeyDown(dashButton) ? true : false;
			wasJump = Input.GetKeyDown(jumpButton) ? true : false;
        }

        private void SetAxes()
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