using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    float speed = 1000;

    [SerializeField]
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            return;

        Health health = other.GetComponent<Health>();
        if (health != null)
        {
            health.TakeDamage(25, other.transform.position);
        }

        Destroy(gameObject);
    }
}
