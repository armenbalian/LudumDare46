using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]
    bool debug = false;


    GameObject target = null;
    Vector3 lastHitPoint;

    AudioSource audioSource;
    ParticleSystem particleSystem;


    private void Awake()
    {
        particleSystem = GetComponentInChildren<ParticleSystem>();
        if (!particleSystem)
            throw new System.Exception("Gun - Need Particle System");

        audioSource = GetComponent<AudioSource>();
        if(!audioSource)
            throw new System.Exception("Gun - Need Audio Source");

    }

    void Update()
    {
        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = LayerMask.GetMask("Enemy");

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out hit, /*Mathf.Infinity*/ 35, layerMask))
        {
            target = hit.collider.gameObject;
            lastHitPoint = hit.point;
            if (debug)
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * hit.distance, Color.green);
            }
        }
        else
        {
            target = null;
            if (debug)
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * 35, Color.red);
            }
        }
    }

    public void Shoot()
    {
        particleSystem.Play();
        audioSource.Play();
        if (target)
        {
            var healthSystem = target.GetComponent<Health>();

            if (healthSystem)
            {
                healthSystem.TakeDamage(25, lastHitPoint);
            }
        }
    }
}
