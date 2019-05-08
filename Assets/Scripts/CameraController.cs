using System;
using UnityEngine;

namespace SoulFood
{
    public class CameraController : MonoBehaviour
    {
        [Header("Attach this script to a camera pivot object")]
        [SerializeField] bool edgePanningEnabled = true;
        [SerializeField] float panBorderSize = 20f;
        [SerializeField] float panSpeed = 10f;
        [SerializeField] float turnSpeed = 100f;
        [SerializeField] float zoomSpeed = 0.5f;

        [Header("Input")]
        [SerializeField] KeyCode leftKey = KeyCode.F;
        [SerializeField] KeyCode rightKey = KeyCode.H;
        [SerializeField] KeyCode upKey = KeyCode.T;
        [SerializeField] KeyCode downKey= KeyCode.G;


        new Camera camera;
        Vector3 cameraInitLocalOffset;
        float zoomFactor = 1f;


        void Start()
        {
            camera = GetComponentInChildren<Camera>();
            cameraInitLocalOffset = camera.transform.localPosition;
            camera.transform.LookAt(transform.position, Vector3.up);
        }

        void Update()
        {
            HandleEdgePanning();
            HandleMoveAndRotate();
            HandleZoom();
        }

        // void OnGUI()
        // {
        //     if (edgePanningEnabled)
        //     {
        //         GUILayout.Label("Move mouse to edge of screen to pan");
        //     }
        //     GUILayout.Label("Use arrows keys to pan");
        // }

        private void HandleEdgePanning()
        {
            if (!edgePanningEnabled)
                return;

            //Grab mouse inputs
            var mx = Input.mousePosition.x;
            var my = Input.mousePosition.y;

            //Pan Forward
            if (my >= Screen.height - panBorderSize)
            {
                transform.position += transform.forward * panSpeed * Time.deltaTime;
            }
            //Pan Down
            if (my <= panBorderSize)
            {
                transform.position -= transform.forward * panSpeed * Time.deltaTime;
            }
            //Pan Left
            if (mx <= panBorderSize)
            {
                transform.position -= transform.right * panSpeed * Time.deltaTime;
            }
            //Pan Right
            if (mx >= Screen.width - panBorderSize)
            {
                transform.position += transform.right * panSpeed * Time.deltaTime;
            }
        }

        private void HandleMoveAndRotate()
        {
            //Pan Up
            if (Input.GetKey(upKey))
            {
                transform.position += transform.forward * panSpeed * Time.deltaTime;
            }
            //Pan Down
            if (Input.GetKey(downKey))
            {
                transform.position -= transform.forward * panSpeed * Time.deltaTime;
            }
            //Pan Left
            if (Input.GetKey(leftKey))
            {
                transform.position -= transform.right * panSpeed * Time.deltaTime;
            }
            //Pan Right
            if (Input.GetKey(rightKey))
            {
                transform.position += transform.right * panSpeed * Time.deltaTime;
            }
            //Rotate about
            if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Mouse1))
            {
                var mxAxis = Input.GetAxis("Mouse X");

                //Rotate about Y
                transform.Rotate(Vector3.up, Time.deltaTime * mxAxis * turnSpeed);
            }
        }

        private void HandleZoom()
        {
            var camLocalPos = camera.transform.localPosition;
            zoomFactor -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
            camLocalPos.y = cameraInitLocalOffset.y * zoomFactor;
            camLocalPos.z = cameraInitLocalOffset.z * zoomFactor;
            camera.transform.localPosition = camLocalPos;
        }
    }
}