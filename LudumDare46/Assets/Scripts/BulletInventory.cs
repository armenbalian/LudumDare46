using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BulletInventory : MonoBehaviour
{
    [SerializeField]
    List<SpriteRenderer> bullets;

    bool isReloading = false;

    [SerializeField]
    Animator anim;

    void Awake()
    {
        if (bullets.Count == 0)
        {
            throw new System.Exception("BulletInventory - dont have bullets.");
        }

        if (anim == null)
            throw new System.Exception("BulletInventory - Need Animator !");
    }

    public bool IsReloading()
    {
        return isReloading;
    }

    public void TriggerReload()
    {
        StartCoroutine(Reloading());
    }

    IEnumerator Reloading()
    {
        isReloading = true;
        StartReloadingAnimation(true);
        var bulletToReload = bullets.Where(b => !b.enabled).ToList();
        for (var i = bulletToReload.Count - 1; i >= 0; i --)
        {
            yield return new WaitForSecondsRealtime(0.5f);
            bulletToReload[i].enabled = true;
        }
        isReloading = false;
    }

    void StartReloadingAnimation(bool start)
    {
        if (start)
        {
            anim.SetLayerWeight(1, 1);
            anim.SetBool("isReloading", true);
        }
        else
        {
            anim.SetLayerWeight(1, 0);
            anim.SetBool("isReloading", false);
        }
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
