using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        GameObjectUtils.SetActive(other.gameObject, true);
    }
}
