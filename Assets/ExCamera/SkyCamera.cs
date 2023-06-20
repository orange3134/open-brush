using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkyCamera
{
    public class SkyCamera : MonoBehaviour
    {
        private Transform _cameraParent;
        private Transform _pitchRoll;
        private Camera _mainCamera;

        private Vector3 _prevPositionVector;
        private Vector3 _currentMoveVelocity;
        private Vector3 _prevRotationVector;
        private Vector3 _currentRotationVelocity;
        private float _prevFovControl;
        private float _currentFovVelocity;

        [Header("Config")]
        [SerializeField] private bool _horizontalMove = true;
        [SerializeField] private float _moveSpeed = 0.2f;
        [SerializeField] private float _moveSmoothTime = 0.1f;
        [SerializeField] private float _rotationSpeed = 0.2f;
        [SerializeField] private float _rotationSmoothTime = 0.1f;
        [SerializeField] private float _fovSpeed = 0.2f;
        [SerializeField] private float _fovSmoothTime = 0.1f;

        // Start is called before the first frame update
        void Start()
        {
            _cameraParent = this.transform.Find("CameraParent");
            _pitchRoll = _cameraParent.Find("PitchRoll");
            _mainCamera = _pitchRoll.Find("Camera").GetComponent<Camera>();
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void Move(Vector3 positionVector)
        {
            Transform moveSpace = _horizontalMove ? _cameraParent : _pitchRoll;

            Vector3 positionVectorInSpace = _cameraParent.InverseTransformVector(moveSpace.TransformVector(positionVector));

            Vector3 smoothPositionVector = Vector3.SmoothDamp(_prevPositionVector, positionVectorInSpace * _moveSpeed, ref _currentMoveVelocity, _moveSmoothTime);

            Debug.Log("PositionVector: " + positionVector + " smoothPositionVector: " + smoothPositionVector);
            _cameraParent.Translate(smoothPositionVector);
            _prevPositionVector = smoothPositionVector;
        }

        //x=yaw y=pitch z=roll
        public void Rotation(Vector3 rotationVector)
        {
            Vector3 smoothRotationVector = Vector3.SmoothDamp(_prevRotationVector, rotationVector * _rotationSpeed, ref _currentRotationVelocity, _rotationSmoothTime);
            _prevRotationVector = smoothRotationVector;

            Quaternion yawRotation = Quaternion.Euler(0, smoothRotationVector.x, 0);
            Quaternion pitchRollRotation = Quaternion.Euler(smoothRotationVector.y, 0, smoothRotationVector.z);

            ParentYawRotation(yawRotation);
            PitchRollRotation(pitchRollRotation);

        }

        private void ParentYawRotation(Quaternion rotation)
        {
            _cameraParent.SetLocalPositionAndRotation(_cameraParent.localPosition, rotation * _cameraParent.localRotation);
        }

        private void PitchRollRotation(Quaternion rotation)
        {
            _pitchRoll.SetLocalPositionAndRotation(_pitchRoll.localPosition, _pitchRoll.localRotation * rotation);
        }

        public void ResetRollRotation()
        {
            var currentEuler = _pitchRoll.localRotation.eulerAngles;
            _pitchRoll.SetLocalPositionAndRotation(_pitchRoll.localPosition, Quaternion.Euler(currentEuler.x, 0f, 0f));
        }

        public void Fov(float fovControl)
        {
            float smoothFovControl = Mathf.SmoothDamp(_prevFovControl, fovControl * _fovSpeed, ref _currentFovVelocity, _fovSmoothTime);
            _prevFovControl = smoothFovControl;
            _mainCamera.fieldOfView = Mathf.Clamp(_mainCamera.fieldOfView + smoothFovControl, 2f, 150f);
        }
    }
}

