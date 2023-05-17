using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    [SerializeField] private Vector3 _addRotation;
    private Transform _camera;
    private Transform _transform;
    private void Start()
    {
        _transform = transform;
    }
    private void FixedUpdate()
    {
        if(_camera == null)
        {
            _camera = Camera.main.transform;
        }
        _transform.LookAt(_camera);
        _transform.Rotate(_addRotation);
    }
}
