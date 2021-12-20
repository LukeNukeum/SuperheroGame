using UnityEngine;
using LupiLab;
using LupiLab.Core;


namespace LupiLab.Camera
{
    public class ThirdPersonCamera : CameraRig
    {

        public static ThirdPersonCamera MainCameraRig;
        public bool IsControlled = false;
        public Transform Target;
        public bool IsAiming = false;

        public override UnityEngine.Camera Camera => _camera;

        [SerializeField] private SOFloat _mouseSensitivity;
        [SerializeField] private Transform _rotationCentreTransform;
        [SerializeField] private Transform _positionerTransform;
        [SerializeField] private Transform _camTransform;
        [SerializeField] private UnityEngine.Camera _camera;
        [SerializeField] private float _minVerticalAngle = -90f;
        [SerializeField] private float _maxVerticalAngle = 90f;
        [SerializeField] private float _verticalAngle = 0f;
        [SerializeField] private float _collisionRadius = 0.3f;
        [SerializeField] private LayerMask collisionMask;
        [SerializeField] private float _zoomInSpeed = 50f;
        [SerializeField] private float _zoomOutSpeed = 2f;

        private GameManager _gameManager;



        [SerializeField]
        private CameraSetup normalSetup;
        [SerializeField]
        private CameraSetup specialAttackAimingSetup;

        public override Quaternion ForwardFlatAngle
        {
            get
            {
                return transform.rotation;
            }
        }

        public override Quaternion TrueForwardAngle
        {
            get
            {
                return _camTransform.rotation;
            }
        }

        public override void SetTarget(Transform target)
        {
            Target = target;
        }


        // Use this for initialization
        void Start()
        {
            if (_rotationCentreTransform)
            {
                _verticalAngle = _rotationCentreTransform.localEulerAngles.x;
                if (_verticalAngle > 180)
                {
                    _verticalAngle -= 360;
                }
            }
        }

        // Update is called once per frame
        void LateUpdate()
        {
            if (PauseGameManagement.IsGamePaused)
            {
                return;
            }
            if (!IsControlled)
            {
                if (Target)
                {
                    transform.position = Target.position;
                    if (_mouseSensitivity != null)
                    {
                        transform.Rotate(Vector3.up, Input.GetAxis("Mouse X") * _mouseSensitivity.Value);
                        if (_rotationCentreTransform)
                        {
                            _verticalAngle = Mathf.Clamp(_verticalAngle + Input.GetAxis("Mouse Y") * _mouseSensitivity.Value, _minVerticalAngle, _maxVerticalAngle);
                            _rotationCentreTransform.localEulerAngles = new Vector3(_verticalAngle, 0, 0);
                        }
                    }
                }
            }
            float cameraDistance;
            if (IsAiming)
            {
                _rotationCentreTransform.localPosition = specialAttackAimingSetup.rotationCentrePosition;
                _positionerTransform.localPosition = specialAttackAimingSetup.positionerLocalPosition;
                cameraDistance = specialAttackAimingSetup.maxDistance;
            }
            else
            {
                _rotationCentreTransform.localPosition = normalSetup.rotationCentrePosition;
                _positionerTransform.localPosition = normalSetup.positionerLocalPosition;
                cameraDistance = normalSetup.maxDistance;
            }

            Ray _ray = new Ray(_positionerTransform.position, -_positionerTransform.forward);
            RaycastHit _hitInfo;
            float _targetZoom;
            if (Physics.SphereCast(_ray, _collisionRadius, out _hitInfo, cameraDistance, collisionMask))
            {
                _targetZoom = _hitInfo.distance;
            }
            else
            {
                _targetZoom = cameraDistance;
            }

            _camTransform.localPosition = new Vector3(0, 0, -Mathf.MoveTowards(-_camTransform.localPosition.z, _targetZoom, ((_targetZoom > -_camTransform.localPosition.z) ? _zoomOutSpeed : _zoomInSpeed) * Time.deltaTime / Time.timeScale));
        }

        void OnEnable()
        {
            MainCameraRig = this;
        }

        void OnDisable()
        {
            MainCameraRig = null;
        }

        public void Rotate(float x, float y)
        {
            if (!_mouseSensitivity) return;
            transform.Rotate(Vector3.up, x * _mouseSensitivity.Value);
            if (_rotationCentreTransform)
            {
                _verticalAngle = Mathf.Clamp(_verticalAngle + y * _mouseSensitivity.Value, _minVerticalAngle, _maxVerticalAngle);
                _rotationCentreTransform.localEulerAngles = new Vector3(_verticalAngle, 0, 0);
            }
        }

        public void MoveTo(Vector3 _position)
        {
            transform.position = _position;
        }

        [System.Serializable]
        public class CameraSetup
        {
            public Vector3 rotationCentrePosition = new Vector3(0, 1.7f, 0);
            public Vector3 positionerLocalPosition = new Vector3(0, 0, 0);
            public float maxDistance = 6f;
        }

    }
}