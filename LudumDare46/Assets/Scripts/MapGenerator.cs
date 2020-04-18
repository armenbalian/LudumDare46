using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{    
    [SerializeField]
    List<GameObject> FloorPossibility;
  
    int mapLenght = 200;

    // Start is called before the first frame update
    void Awake()
    {
        var newPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        for (var i = 0; i <= mapLenght; i++)
        {
            var isLava = Random.Range(0, 4) == 0;

            var gameObject = FloorPossibility[0];
            if (isLava)
            {
                gameObject = FloorPossibility[1];
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
