﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{    
    [SerializeField]
    List<GameObject> floorPossibilityPrefab;

    [SerializeField]
    GameObject enemyPrefab;
  
    int mapLenght = 1000;
    int maxHoleLength = 4;

    // Start is called before the first frame update
    void Awake()
    {
        var newPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        var holeLenght = 0;
        var floorLenght = 0;
        for (var i = 0; i <= mapLenght; i++)
        {
            bool safeZone = i < 50;

            var gameObject = floorPossibilityPrefab[0];

            var isHole = Random.Range(0, 10) == 0;

            if (safeZone || floorLenght < 2)
            {
                isHole = false;
            }
            
            if (holeLenght > 0)
            {
                isHole = true;
            }

            // we need to floor at least
            if (isHole)
            {
                floorLenght = 0;
                gameObject = floorPossibilityPrefab[1];
                if (holeLenght == 0)
                {
                    holeLenght = Random.Range(2, maxHoleLength + 1);
                }
                else
                {
                    holeLenght--;
                }
            }            
            else
            {
                floorLenght++;
            }

            var haveEnemy = Random.Range(0, 15) == 0;
            if(haveEnemy && !isHole && !safeZone)
            {                
                if (enemyPrefab)
                {
                    var enemyPos = new Vector3(newPos.x, newPos.y + 10, newPos.z);
                    Instantiate(enemyPrefab, enemyPos, transform.rotation);
                }                
            }

            gameObject.name = "Floor#" + i;
            Instantiate(gameObject, newPos, transform.rotation);
            newPos.x++;
        }

        RefreshAllGroundSensor();
    }

    void RefreshAllGroundSensor()
    {
        var groundSensors = FindObjectsOfType<GroundSensor>();
        foreach (var sensor in groundSensors)
        {
            sensor.RefreshGroundStatusCache();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}