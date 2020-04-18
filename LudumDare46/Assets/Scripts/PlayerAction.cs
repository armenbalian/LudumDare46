using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    Gun gun;

    ObjectThrow objectThrow; 

    private void Awake()
    {
        gun = GetComponentInChildren<Gun>();
        if (gun == null)
            throw new System.Exception("Player Action - Need Gun !");

        objectThrow = FindObjectOfType<ObjectThrow>();
        if (objectThrow == null)
            throw new System.Exception("Player Action - Need Object Throw !");
    }

    // Start is called before the first frame update
    void Start()
    {
  
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            gun.Shoot();
        }

        if (Input.GetButtonDown("Fire2"))
        {
            objectThrow.Launch();
        }
    }
}
