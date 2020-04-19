using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerAction : MonoBehaviour
{
    Gun gun;

    ObjectThrow objectThrow;

    [SerializeField]
    Health health;

    private void Awake()
    {
        gun = GetComponentInChildren<Gun>();
        if (gun == null)
            throw new System.Exception("Player Action - Need Gun !");

        objectThrow = FindObjectOfType<ObjectThrow>();
        if (objectThrow == null)
            throw new System.Exception("Player Action - Need Object Throw !");   

        if(health == null)
            throw new System.Exception("Player Action - Need Health !");

    }

    // Start is called before the first frame update
    void Start()
    {
  
    }

    // Update is called once per frame
    void Update()
    {
        if (!health.IsDead())
        {
            if (Input.GetButtonDown("Fire1"))
            {
                gun.Shoot(health.GetInstanceID());
            }

            if (Input.GetButtonDown("Fire2"))
            {
                objectThrow.Launch();
            }
        }
        else
        {
            StartCoroutine(WaitBeforeRestart());
        }
    }
    IEnumerator WaitBeforeRestart()
    {
        yield return new WaitForSecondsRealtime(1.0f);
        SceneManager.LoadScene(0);
    }
}
