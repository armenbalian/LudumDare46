using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int ownerId;

    [SerializeField]
    float speed = 200;

    [SerializeField]
    float speedMultiplier = 1;

    [SerializeField]
    Rigidbody rb;

    bool haveCollide = false;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * (speed * speedMultiplier);        
    }

    public void SetOwner(int instanceId)
    {
        ownerId = instanceId;
    }

    public void SetSpeedMultiplier(float speedMulti)
    {
        speedMultiplier = speedMulti;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (haveCollide)
            return;


        Health health = other.GetComponent<Health>();
        if (health != null)
        {
            if (ownerId == health.GetInstanceID())
                return;

            if (!health.isInvincible)
            {
                haveCollide = true;
                health.TakeDamage(Random.Range(30, 60), other.transform.position);
                Destroy(gameObject);
            }
        }

        var bullet = other.gameObject.GetComponent<Bullet>();
        if (bullet != null)
        {
            Destroy(gameObject);
            Destroy(other.gameObject);  
        }

        var objectSpawner = other.gameObject.GetComponent<ObjectSpawner>();
        if (objectSpawner != null)
        {
            Destroy(gameObject);
        }

        var objectKiller = other.gameObject.GetComponent<ObjectKiller>();
        if (objectKiller != null)
        {
            Destroy(gameObject);
        }
    }
}
