using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class GroundSensor : MonoBehaviour
{
    MeshRenderer meshRenderer; 

    Dictionary<string, bool> allGroundsStatus;

    private void Awake()
    {
        RefreshGroundStatusCache();
    }

    public void RefreshGroundStatusCache()
    {
        allGroundsStatus = new Dictionary<string, bool>();
        var grounds = FindObjectsOfType<GameObject>().Where(g => g.layer == LayerMask.NameToLayer("Ground"));
        foreach (var ground in grounds)
        {
            allGroundsStatus.Add(ground.name, false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (meshRenderer.enabled)
        {
            meshRenderer.enabled = false;
        }
    }

    public bool GetIsTouchingGround()
    {
        return allGroundsStatus.Any(c => c.Value);
    }

    public void resetAllStatus()
    {
        foreach (var key in allGroundsStatus.Keys.ToList())
        {
            allGroundsStatus[key] = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        var gameObject = other.gameObject;
        if (gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            allGroundsStatus[gameObject.name] = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        var gameObject = other.gameObject;
        if (gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            allGroundsStatus[gameObject.name] = false;
        }
    }
}
