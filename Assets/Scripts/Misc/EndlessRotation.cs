using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessRotation : MonoBehaviour
{
    [SerializeField] private Vector3 _rotationOverFrame;
    private Transform _transform;
    private void Start()
    {
        _transform = transform;
    }
    private void FixedUpdate()
    {
        _transform.Rotate(_rotationOverFrame);
    }
}
