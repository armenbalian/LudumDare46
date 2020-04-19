using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionDetection : MonoBehaviour
{
    //Dictionary<string, Health> objectsInRange = new Dictionary<string, Health>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
    public IEnumerable<Health> GetEnemiesToDamage()
    {
        print(objectsInRange.Count);
        return objectsInRange.Values;
    }
    */

    void OnTriggerEnter(Collider other)
    {
        var health = other.gameObject.GetComponent<Health>();
        if (health == null)
            return;

        health.TakeDamage(100, health.transform.position);
        /*
        var instanceId = health.GetInstanceID().ToString();
        if (!objectsInRange.ContainsKey(instanceId))
        {
            objectsInRange.Add(instanceId, health);
        }
        */
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
