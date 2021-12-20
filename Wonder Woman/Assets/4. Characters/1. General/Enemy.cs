using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LupiLab.Character
{
    public class Enemy : Character
    {
        [SerializeField] private float WalkSpeed = 2f;
        [SerializeField] private Character TargetCharacter;
        [SerializeField] private Vector3 targetPosition;
        [SerializeField] private float crowdingRange = 2f;

        private Rigidbody _rigidbody;
        private Animator _animator;
        //private ITargetGetter _targetGetter;

        protected override void Awake()
        {
            base.Awake();
            _rigidbody = GetComponent<Rigidbody>();
            _animator = GetComponentInChildren<Animator>();
            //_targetGetter = GetComponent<ITargetGetter>();
        }

        private void FixedUpdate()
        {

            if (TargetCharacter != null)
            {

                Vector3 forwardDirection = TargetCharacter.transform.position - transform.position;
                forwardDirection.y = 0;

                if (forwardDirection != Vector3.zero)
                {
                    transform.forward = forwardDirection;
                }



                Vector3 fromSelfToTarget = TargetCharacter.transform.position - transform.position;
                Vector3 fromSelfToTargetFlat = fromSelfToTarget;
                fromSelfToTargetFlat.y = 0;

                targetPosition = TargetCharacter.transform.position - fromSelfToTarget.normalized * crowdingRange;

                _animator.SetFloat("WalkSpeed", Mathf.Clamp((Quaternion.Inverse(transform.rotation) * fromSelfToTargetFlat).z - crowdingRange, -0.5f, 1));


                Debug.DrawRay(targetPosition, Vector3.up, Color.red);


                Vector3 movementVector = (targetPosition - transform.position) / Time.fixedDeltaTime;
                movementVector.y = 0;
                movementVector = Vector3.ClampMagnitude(movementVector, WalkSpeed);

                movementVector.y = _rigidbody.velocity.y;

                //rigidbody.velocity = movementVector;

            }

        }

        private void Update()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                _animator.SetTrigger("TriggerAttack");
            }


            if (Input.GetButtonDown("Fire2"))
            {
                //SearchForTarget();
            }
        }


        //public void SearchForTarget()
        //{
        //    if (_targetGetter != null)
        //    {
        //        GameObject foundTarget = _targetGetter.SearchForTarget(CharacterManager.Singleton.characters);
        //        if (foundTarget != null)
        //        {
        //            TargetCharacter = foundTarget.GetComponent<Character>();
        //        }
        //    }
        //}

    }
}