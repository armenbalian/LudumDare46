using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField]
    Transform target;

    Vector3 offset;

    [SerializeField]
    private float smoothSpeed = 0.125f;

    void Awake()
    {        
        offset = transform.position - target.position;        
    }
    
    void LateUpdate()
    {
        transform.position = new Vector3(target.position.x + offset.x, transform.position.y, transform.position.z);        
    }   
}
