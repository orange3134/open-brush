using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace SkyCamera
{
    public class SkyCameraControllerDesktop : MonoBehaviour
    {
        private SkyCamera _skyCamera;
        [Header("Movement")]
        [SerializeField] private Key _keyForward = Key.W;
        [SerializeField] private Key _keyBack = Key.S;
        [SerializeField] private Key _keyLeft = Key.A;
        [SerializeField] private Key _keyRight = Key.D;
        [SerializeField] private Key _keyUp = Key.Space;
        [SerializeField] private Key _keyDown = Key.C;
        [Header("Rotation")]
        [SerializeField] private Key _keyYawRight = Key.RightArrow;
        [SerializeField] private Key _keyYawLeft = Key.LeftArrow;
        [SerializeField] private Key _keyPitchUp = Key.UpArrow;
        [SerializeField] private Key _keyPitchDown = Key.DownArrow;
        [SerializeField] private Key _keyRollLeft = Key.Q;
        [SerializeField] private Key _keyRollRight = Key.R;
        [Header("Fov")]
        [SerializeField] private Key _keyFovTele = Key.R;
        [SerializeField] private Key _keyFovWide = Key.F;
        [Header("Function")]
        [SerializeField] private Key _keyResetRoll = Key.V;
        [Header("Config")]
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

                if (Keyboard.current[_keyResetRoll].wasPressedThisFrame) _skyCamera.ResetRollRotation();
            }

        }

        private Vector3 MoveInput()
        {
            float forward = (Keyboard.current[_keyForward].isPressed ? 1f : 0f) + (Keyboard.current[_keyBack].isPressed ? -1f : 0f);
            float right = (Keyboard.current[_keyRight].isPressed ? 1f : 0f) + (Keyboard.current[_keyLeft].isPressed ? -1f : 0f);
            float up = (Keyboard.current[_keyUp].isPressed ? 1f : 0f) + (Keyboard.current[_keyDown].isPressed ? -1f : 0f);
            Vector3 vector = new Vector3(right,up,forward);
            return vector;
        }

        private Vector3 RotationInput()
        {
            float yaw = (Keyboard.current[_keyYawRight].isPressed ? 1f : 0f) + (Keyboard.current[_keyYawLeft].isPressed ? -1f : 0f);
            float pitch = (Keyboard.current[_keyPitchDown].isPressed ? 1f : 0f) + (Keyboard.current[_keyPitchUp].isPressed ? -1f : 0f);
            float roll = (Keyboard.current[_keyRollLeft].isPressed ? 1f : 0f) + (Keyboard.current[_keyRollRight].isPressed ? -1f : 0f);
            Vector3 vector = new Vector3(yaw, _invertYAxis ? -pitch : pitch, roll);
            return vector;
        }

        private float FovInput()
        {
            float fov = (Keyboard.current[_keyFovWide].isPressed ? 1f : 0f) + (Keyboard.current[_keyFovTele].isPressed ? -1f : 0f);
            return fov;
        }

        public void test()
        {

        }
    }
}

