using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectKiller : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator WaitBeforeRestart()
    {
        yield return new WaitForSecondsRealtime(1.0f);
        SceneManager.LoadScene(0);
    }

    void OnTriggerEnter(Collider other)
    {
        var isPlayer = other.gameObject.tag == "Player";
        if (isPlayer)
        {
            StartCoroutine(WaitBeforeRestart());           
            return;
        }

        var grenade = other.gameObject.GetComponent<Grenade>();
        if (grenade != null)
        {
            return;
        }

        Destroy(other.gameObject);
    }        
}
