﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            return;
        }

        var player = other.gameObject.GetComponent<PlayerMovement>();
        if (player != null)
        {
            SceneManager.LoadScene(0);
        }

        Destroy(other.gameObject);
    }        
}
