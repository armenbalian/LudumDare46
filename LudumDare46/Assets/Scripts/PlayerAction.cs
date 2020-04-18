using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    Gun gun;

    private void Awake()
    {
        gun = GetComponentInChildren<Gun>();
        if (gun == null)
            throw new System.Exception("Player Action - Need Gun !");
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
    }
}
