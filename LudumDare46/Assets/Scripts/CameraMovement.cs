using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    Transform target;

    Vector3 offset;

    void Awake()
    {
        offset = transform.position - target.position;
    }

    void LateUpdate()
    {
        transform.position = new Vector3(target.position.x + offset.x, transform.position.y, transform.position.z);
    }
}
