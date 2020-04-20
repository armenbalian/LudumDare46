using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{    
    [SerializeField]
    List<GameObject> floorPossibilityPrefab;

    [SerializeField]
    GameObject enemyPrefab;
  
    int mapLenght = 1000;
    int maxHoleLength = 5;

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

            var isHole = Random.Range(0, 8) == 0;

            if (safeZone || floorLenght < 6)
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
                    holeLenght = Random.Range(3, maxHoleLength + 1);
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
          
            GameObject enemyInstance = null;
            //var haveEnemy = Random.Range(0, 15) == 0;
            var haveEnemy = false;
            if(haveEnemy && !isHole && !safeZone)
            {                
                if (enemyPrefab)
                {
                    var enemyPos = new Vector3(newPos.x, newPos.y + 10, newPos.z);
                    enemyInstance = Instantiate(enemyPrefab, enemyPos, transform.rotation);
                }                
            }

            if (i == mapLenght)
            {
                var posPlant = newPos;
                posPlant.y++;
                var newPlantInstance = Instantiate(floorPossibilityPrefab[2], posPlant, transform.rotation);
                //GameObjectUtils.SetActive(newPlantInstance, false);
            }

            gameObject.name = "Floor#" + i;
            var newInstance = Instantiate(gameObject, newPos, transform.rotation);

            if (i >= 80)
            {
                //GameObjectUtils.SetActive(newInstance, false);                
                if (enemyInstance != null)
                {
                    //GameObjectUtils.SetActive(enemyInstance, false);
                }                
            }

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
