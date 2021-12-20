using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using LupiLab.Targeting;


namespace LupiLab.UI
{
    public class UIInteractionTooltip : MonoBehaviour
    {

        [SerializeField] InteractionAgent _interactionAgent;
        [SerializeField] Interactable _targetInteractable;

        [SerializeField] UnityEngine.Camera _camera;
        [SerializeField] RectTransform _parentRect;
        [SerializeField] TextMeshProUGUI _displayNameTextMesh;
        [SerializeField] TextMeshProUGUI _messageTextMesh;
        [SerializeField] GameObject[] _childObjects;

        RectTransform _rectTransform;
        


        public void Bind(InteractionAgent agent)
        {
            _interactionAgent = agent;
            _interactionAgent.TargetChangedEvent += OnTargetChanged;
            _interactionAgent.TargetLostEvent += OnTargetLost;

            SetTarget(_interactionAgent.CurrentTarget);
        }

        public void Unbind()
        {
            if(_interactionAgent!= null)
            {
                _interactionAgent.TargetChangedEvent -= OnTargetChanged;
                _interactionAgent.TargetLostEvent -= OnTargetLost;

                ClearTarget();
            }
        }

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _parentRect = (RectTransform)_rectTransform.parent;
        }

        private void OnEnable()
        {
            _camera = Camera.ThirdPersonCamera.MainCameraRig.Camera;
            if (_interactionAgent!=null)
            {
                Bind(_interactionAgent);
            }
            else
            {
                ClearTarget();
            }
        }

        private void OnDisable()
        {
            Unbind();
        }

        void OnTargetChanged(Interactable target)
        {
            //Debug.Log($"Target changed to: {target}");
            SetTarget(target);
        }

        void OnTargetLost()
        {
            ClearTarget();
        }


        public void SetTarget(Interactable target)
        {
            if(target == null)
            {
                ClearTarget();
                return;
            }
            _targetInteractable = target;
            _displayNameTextMesh.text = _targetInteractable.DisplayName;
            _messageTextMesh.text = _targetInteractable.PromptMessage;
            SetChildElementsActive(true);
        }

        public void ClearTarget()
        {
            SetChildElementsActive(false);
        }

        public void SetChildElementsActive(bool value)
        {
            foreach(GameObject child in _childObjects)
            {
                child.SetActive(value);
            }
            //foreach(Transform child in transform)
            //{
            //    n++;
            //    child.gameObject.SetActive(value);
            //}
        }



        private void LateUpdate()
        {
            if (_targetInteractable == null) return;

            Vector3 targetViewportPoint = _camera.WorldToViewportPoint(_targetInteractable.Position);

            targetViewportPoint.x = (targetViewportPoint.x) *_parentRect.sizeDelta.x;
            targetViewportPoint.y = (targetViewportPoint.y) * _parentRect.sizeDelta.y;
            targetViewportPoint.z = 0;
            _rectTransform.position = targetViewportPoint;
        }



    }
}