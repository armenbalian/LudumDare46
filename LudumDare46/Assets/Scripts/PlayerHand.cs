using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    [SerializeField]
    Gun gun;

    private void Awake()
    {
        if (gun == null)
            throw new System.Exception("Player Hand - Need Gun !");
    }

    // Update is called once per frame
    void Update()
    {
        var newPos = transform.position;
        newPos.x += 0.2f;
        newPos.y += 0.15f;
        gun.transform.position = newPos;
    }
}
