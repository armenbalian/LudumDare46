using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BulletInventory : MonoBehaviour
{
    [SerializeField]
    List<SpriteRenderer> bullets;

    private bool isReloading = false;

    void Awake()
    {
        if (bullets.Count == 0)
        {
            throw new System.Exception("BulletInventory - dont have bullets.");
        }
    }

    public void TriggerReload()
    {
        StartCoroutine(Reloading());
    }

    IEnumerator Reloading()
    {
        isReloading = true;
        var bulletToReload = bullets.Where(b => !b.enabled).ToList();
        for (var i = bulletToReload.Count - 1; i >= 0; i --)
        {
            yield return new WaitForSecondsRealtime(0.5f);
            bulletToReload[i].enabled = true;
        }
        isReloading = false;
    }

    public bool ConsumeBullet()
    {
        if (isReloading)
        {
            return false;
        }

        var sprite = bullets.FirstOrDefault(b => b.enabled);

        if (sprite == null)
        {
            StartCoroutine(Reloading());
            return false;
        }

        sprite.enabled = false;

        return true;
    }
}
