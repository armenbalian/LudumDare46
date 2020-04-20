using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAction : MonoBehaviour
{
    Animator animator;

    Gun gun;

    [SerializeField]
    Health health;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        if (animator != null)
        {
            animator.SetBool("isJumping", false);
            animator.SetBool("isRunning", false);
        }

        gun = GetComponentInChildren<Gun>();
        if (gun == null)
        {
            throw new System.Exception("Enemy - Gun is null");
        }

        if(health == null)
        {
            throw new System.Exception("Enemy - health is null");
        }
    }

    void Update()
    {        
        if (!health.IsDead())
        {
            EnemyGunLogic();
        }        
    }

    public void EnemyGunLogic()
    {
        if (IfPlayerInRange())
        {
            var shouldIShoot = Random.Range(0, 30) == 0;
            if (shouldIShoot)
            {
                gun.Shoot(health.GetInstanceID(), 0.05f);
            }
        }
    }

    public bool IfPlayerInRange()
    {
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(gun.transform.position, gun.transform.TransformDirection(Vector3.right), out hit, 35))
        {
            var isPlayer = hit.collider.gameObject.tag == "Player";
            return isPlayer;
        }

        return false;
    }

}
