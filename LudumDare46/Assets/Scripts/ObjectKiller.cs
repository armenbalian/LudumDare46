using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectKiller : MonoBehaviour
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
        var grenade = other.gameObject.GetComponent<Grenade>();
        if (grenade != null)
        {
            print("grenade skip");
            return;
        }

        Destroy(other.gameObject);
    }        
}
