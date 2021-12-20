using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyRotation : MonoBehaviour
{
    [SerializeField] private Transform _keyTransform;

    private void Update()
    {
        transform.rotation = _keyTransform.rotation;
    }
}
