using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _speedLook;

    private float deltaDistance;
    private bool isMove = false;

    private void Start()
    {
        deltaDistance = _target.position.y - transform.position.y;
        isMove = true;
    }

    private void FixedUpdate()
    {
        /*if (isMove && _target.position.y >= 1.5f)
            transform.position = new Vector3(transform.position.x, Mathf.Clamp(_target.position.y - deltaDistance, 0, 10), transform.position.z);
        else
            //transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        //transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, Mathf.Clamp(_target.position.y - deltaDistance, 0, 10), transform.position.z), Time.deltaTime * _speedLook);
        */
    }
}
