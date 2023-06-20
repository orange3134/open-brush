using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SkyCamera
{
    public class SkyCameraControllerDesktop : MonoBehaviour
    {
        private SkyCamera _skyCamera;
        
        [SerializeField] private bool _invertYAxis = true;
        
        // Start is called before the first frame update
        void Start()
        {
            _skyCamera = GetComponent<SkyCamera>();
        }

        // Update is called once per frame
        void Update()
        {
            if(_skyCamera != null)
            {
                Vector3 inputVector = MoveInput();
                Vector3 rotationInput = RotationInput();
                float fovInput = FovInput();
                _skyCamera.Move(inputVector);
                _skyCamera.Rotation(rotationInput);
                _skyCamera.Fov(fovInput);

                if (Keyboard.current.zKey.wasPressedThisFrame) _skyCamera.ResetRollRotation();
            }

        }

        private Vector3 MoveInput()
        {
            float forward = (Keyboard.current.wKey.isPressed ? 1f : 0f) + (Keyboard.current.sKey.isPressed ? -1f : 0f);
            float right = (Keyboard.current.dKey.isPressed ? 1f : 0f) + (Keyboard.current.aKey.isPressed ? -1f : 0f);
            float up = (Keyboard.current.spaceKey.isPressed ? 1f : 0f) + (Keyboard.current.cKey.isPressed ? -1f : 0f);
            Vector3 vector = new Vector3(right,up,forward);
            return vector;
        }

        private Vector3 RotationInput()
        {
            float yaw = (Keyboard.current.rightArrowKey.isPressed ? 1f : 0f) + (Keyboard.current.leftArrowKey.isPressed ? -1f : 0f);
            float pitch = (Keyboard.current.downArrowKey.isPressed ? 1f : 0f) + (Keyboard.current.upArrowKey.isPressed ? -1f : 0f);
            float roll = (Keyboard.current.qKey.isPressed ? 1f : 0f) + (Keyboard.current.eKey.isPressed ? -1f : 0f);
            Vector3 vector = new Vector3(yaw, _invertYAxis ? -pitch : pitch, roll);
            return vector;
        }

        private float FovInput()
        {
            float fov = (Keyboard.current.fKey.isPressed ? 1f : 0f) + (Keyboard.current.rKey.isPressed ? -1f : 0f);
            return fov;
        }
    }
}

