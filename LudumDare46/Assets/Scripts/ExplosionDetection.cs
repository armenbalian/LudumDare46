using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionDetection : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            return;

        var health = other.gameObject.GetComponent<Health>();
        if (health == null)
            return;

        health.TakeDamage(100, health.transform.position);
    }


    void OnTriggerExit(Collider other)
    {
        var health = other.gameObject.GetComponent<Health>();
        if (health == null)
            return;

        /*
        var instanceId = health.GetInstanceID().ToString();
        if (objectsInRange.ContainsKey(instanceId))
        {
            objectsInRange.Remove(instanceId);
        }
        */
    }
}
