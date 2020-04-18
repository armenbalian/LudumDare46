﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    GameObject target = null;

    ParticleSystem particleSystem = null;

    private void Awake()
    {
        particleSystem = GetComponentInChildren<ParticleSystem>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << 8;

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        layerMask = ~layerMask;

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out hit, Mathf.Infinity, layerMask))
        {
            target = hit.collider.gameObject;
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * hit.distance, Color.green);
            Debug.Log("Did Hit");
        }
        else
        {
            target = null;
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * 1000, Color.red);
        }
    }

    public void Shoot()
    {
        particleSystem.Play();
        if (target != null)
        {
            Destroy(target);
        }
    }
}