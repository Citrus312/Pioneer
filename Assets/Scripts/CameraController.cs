using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform _cameraTrans;
    private float _preMousePosX;
    private float _dragSpeed;

    protected void Awake()
    {
        _cameraTrans = transform;
        _preMousePosX = 0.0f;
        _dragSpeed = 0.02f;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0)){
            float _moveDistance = (Input.mousePosition.x - _preMousePosX) * _dragSpeed;
            _cameraTrans.position = new Vector3(_cameraTrans.position.x + _moveDistance, _cameraTrans.position.y, _cameraTrans.position.z);
        }
        _preMousePosX = Input.mousePosition.x;
    }
}
