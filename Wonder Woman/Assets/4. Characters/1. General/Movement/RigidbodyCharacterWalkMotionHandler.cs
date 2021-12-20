using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LupiLab.Character.Motion
{
    public class RigidbodyCharacterWalkMotionHandler: CharacterWalkMotionHandler
    {

        public override bool IsGrounded => _isGrounded;

        private Rigidbody _rigidbody;
        private GroundScanSettings _groundScanSettings;

        private float _yVelocity = 0;
        private float _yVelocityLastFrame = 0;

        private bool _isGrounded = false;

        public RigidbodyCharacterWalkMotionHandler(Rigidbody rigidbody, GroundScanSettings settings)
        {
            _rigidbody = rigidbody;
            _groundScanSettings = settings;
        }

        public override void FixedMove(Vector3 velocity)
        {
            Vector3 targetDisplacement = velocity * Time.fixedDeltaTime;

            _yVelocity = _rigidbody.velocity.y;
            








            Debug.DrawRay(_rigidbody.position, Vector3.down, Color.blue);
            Debug.DrawRay(_rigidbody.position + targetDisplacement, Vector3.down, Color.cyan);

            Ground currentGround = GetGround(_rigidbody.position + Vector3.up * _groundScanSettings.GroundScanVerticalOffset);
            Ground targetGround = GetGround(_rigidbody.position + targetDisplacement + Vector3.up * _groundScanSettings.GroundScanVerticalOffset);

            _isGrounded = !(targetGround is null);

            if((currentGround !=null) &&( targetGround != null))
            {
                Debug.Log($"Scanned Step Distance: {Mathf.Round(( targetGround.HitPoint.y - currentGround.HitPoint.y)*1000)}");
            }

            velocity.y = _rigidbody.velocity.y;
            _rigidbody.velocity = velocity;
        }


        private Ground GetGround(Vector3 origin)
        {
            Debug.DrawRay(origin, Vector3.down * (_groundScanSettings.GroundScanRadius + _groundScanSettings.MaxGroundScanDistance), Color.white);

            var ray = new Ray(origin, Vector3.down);
            RaycastHit hitInfo;
            if (Physics.SphereCast(ray, _groundScanSettings.GroundScanRadius, out hitInfo, _groundScanSettings.MaxGroundScanDistance, _groundScanSettings.WalkableLayerMask))
            {
                Debug.DrawRay(hitInfo.point, hitInfo.normal * _groundScanSettings.GroundScanRadius, Color.red);

                
                
                return new Ground(hitInfo.point, hitInfo.normal);
            }

            return null;
        }


        private class Ground
        {

            public Vector3 HitPoint;
            public Vector3 Normal;

            public Ground (Vector3 hitPoint,Vector3 normal)
            {
                HitPoint = hitPoint;
                Normal = normal;
            }
        }


    }
}