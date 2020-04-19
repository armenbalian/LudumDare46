using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]
    bool debug = false;

    [SerializeField]
    GameObject bulletPrefab;

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

    }

    public void Shoot()
    {
        particleSystem.Play();
        audioSource.Play();

        Instantiate(bulletPrefab, transform.position, transform.rotation);
    }
}
