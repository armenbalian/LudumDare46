using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailObjects : MonoBehaviour
{
    [SerializeField]
    List<GameObject> GameObjectsToSpawn = new List<GameObject>();

    SpawnPoint spawnPoint;

    private void Awake()
    {
        spawnPoint = FindObjectOfType<SpawnPoint>();
        if (spawnPoint == null)
            throw new System.Exception("RailObjects : Need a SpawnPoint in the scene !");       
    }

    // Start is called before the first frame update
    void Start()
    {
        if (GameObjectsToSpawn.Count > 0)
        {
            Instantiate(GameObjectsToSpawn[0], spawnPoint.transform.position, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
