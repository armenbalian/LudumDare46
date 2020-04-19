using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]
    GameObject bulletPrefab;

    [SerializeField]
    Transform bulletSpawnPoint;

    AudioSource audioSource;
    ParticleSystem ps;

    private void Awake()
    {
        ps = GetComponentInChildren<ParticleSystem>();
        if (ps == null)
            throw new System.Exception("Gun - Need Particle System");

        audioSource = GetComponent<AudioSource>();
        if(audioSource == null)
            throw new System.Exception("Gun - Need Audio Source");
    }

    public void Update()
    {

    }

    public void Shoot(int OwnerId, float speedMultiplier = 1.0f)
    {
        ps.Play();
        audioSource.Play();

        var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, transform.rotation);
        var bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {          
            bulletScript.SetOwner(OwnerId);
            bulletScript.SetSpeedMultiplier(speedMultiplier);
        }        
    }
}
