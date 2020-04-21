using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]
    GameObject bulletPrefab;

    [SerializeField]
    Transform bulletSpawnPoint;

    AudioSource asGunShot;
    AudioSource asGunReload;

    ParticleSystem ps;

    BulletInventory bulletInventory;

    private void Awake()
    {
        ps = GetComponentInChildren<ParticleSystem>();
        if (ps == null)
            throw new System.Exception("Gun - Need Particle System");

        //asGunShot = GetComponent<AudioSource>();

        var audioSources = GetComponents<AudioSource>();
        if (audioSources.Length == 0)
        {
            throw new System.Exception("Gun - Need Audio Source");
        }

        foreach (var audio in audioSources)
        {
            if (audio.clip.name.Contains("Shot"))
            {
                asGunShot = audio;
            }
            else if (audio.clip.name.Contains("Reload"))
            {
                asGunReload = audio;
            }
        }

        // can be null
        bulletInventory = GetComponent<BulletInventory>();
    }

    public void Update()
    {

    }

    public void Reload()
    {
        if(bulletInventory != null)
        {
            if (!bulletInventory.IsReloading())
            {
                asGunReload.Play();
                bulletInventory.TriggerReload();
            }
        }
    }

    public void Shoot(int OwnerId, float speedMultiplier = 1.0f)
    {

        if (bulletInventory != null)
        {
            if (!bulletInventory.ConsumeBullet())
            {
                Reload();
                return;
            }
        }

        ps.Play();
        asGunShot.Play();

        var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, transform.rotation);
        var bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {          
            bulletScript.SetOwner(OwnerId);
            bulletScript.SetSpeedMultiplier(speedMultiplier);
        }        
    }
}
