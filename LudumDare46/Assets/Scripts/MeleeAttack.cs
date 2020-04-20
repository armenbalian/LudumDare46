using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            return;

        var health = other.gameObject.GetComponent<Health>();
        if (health != null)
        {
            health.TakeDamage(100, other.gameObject.transform.position);
        }
    }
}
