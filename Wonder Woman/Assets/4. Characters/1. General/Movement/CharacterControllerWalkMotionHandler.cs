using UnityEngine;

namespace LupiLab.Character.Motion
{
    public class CharacterControllerWalkMotionHandler : CharacterWalkMotionHandler
    {
        public float Gravity { get; set; } = 9.81f;
        [Range(0,1)] public float GroundStickVelocity = 0.1f;

        public override bool IsGrounded => _characterController.isGrounded;

        private CharacterController _characterController;
        private Transform _transform;
        private GroundScanSettings _groundScanSettings;
        private CharacterControllerPhysics _characterControllerPhysics;


        public CharacterControllerWalkMotionHandler(CharacterController characterController, GroundScanSettings groundScanSettings, CharacterControllerPhysics characterControllerPhysics)
        {
            _characterController = characterController;
            _transform = _characterController.transform;
            _groundScanSettings = groundScanSettings;
            _characterControllerPhysics = characterControllerPhysics;
        }


        public override void FixedMove(Vector3 inputVelocity)
        {
            //Vector3 targetDisplacement = inputVelocity * Time.fixedDeltaTime;
            //

            if (_characterController.isGrounded)
            {
                inputVelocity.y -= GroundStickVelocity;
            }
            else
            {
                inputVelocity.y = _characterControllerPhysics.Velocity.y - Gravity * Time.fixedDeltaTime;
            }


            Vector3 groundNormal;
            GetGroundNormal(out groundNormal);


            Vector3 previousPosition = _transform.position;

            _characterController.Move(inputVelocity * Time.fixedDeltaTime);

            _characterControllerPhysics.Velocity = (_transform.position - previousPosition) / Time.fixedDeltaTime;


        }

        bool GetGroundNormal(out Vector3 normal)
        {

            var ray = new Ray(GroundScanOrigin, Vector3.down);
            RaycastHit hitInfo;

            Debug.DrawLine(ray.origin + Vector3.back * _groundScanSettings.GroundScanRadius, ray.origin + Vector3.forward * _groundScanSettings.GroundScanRadius, Color.cyan);
            Debug.DrawLine(ray.origin + Vector3.left * _groundScanSettings.GroundScanRadius, ray.origin + Vector3.right * _groundScanSettings.GroundScanRadius, Color.cyan);
            Debug.DrawRay(ray.origin, Vector3.down * MaxGroundRaycastDistance);

            //if (Physics.Raycast(ray: ray, hitInfo: out hitInfo, maxDistance: MaxGroundRaycastDistance, layerMask: _groundScanSettings.WalkableLayerMask))
            //{
            //    Debug.DrawRay(hitInfo.point, -hitInfo.normal, Color.green);
            //    normal = hitInfo.normal;
            //    return true;
            //}

            if (Physics.SphereCast(ray: ray, radius: _groundScanSettings.GroundScanRadius, hitInfo: out hitInfo, maxDistance: _groundScanSettings.MaxGroundScanDistance, layerMask: _groundScanSettings.WalkableLayerMask))
            {
                Debug.DrawRay(hitInfo.point, -hitInfo.normal, Color.yellow);
                normal = hitInfo.normal;
                return true;
            }

            Debug.DrawRay(_transform.position, -Vector3.up, Color.red);
            normal = Vector3.up;
            return false;
        }


        private Vector3 GroundScanOrigin => _transform.position + new Vector3(0, _groundScanSettings.GroundScanRadius + _groundScanSettings.GroundScanVerticalOffset, 0);
        private float MaxGroundRaycastDistance => _groundScanSettings.GroundScanRadius + _groundScanSettings.GroundScanVerticalOffset + _groundScanSettings.MaxGroundScanDistance;


    }
}