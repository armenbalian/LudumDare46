using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimReload : MonoBehaviour
{
    /*
        OUUTCHH !!
        Need rethink hard.
        Really Bad solution

        Dont know how to change 
    */

    Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
        if (anim == null)
            throw new System.Exception("AnimReload - Need anim !");
    }

    // Call at the end of the anim
    public void StopAnimReload()
    {
        anim.SetLayerWeight(1, 0);
        anim.SetBool("isReloading", false);
    }
}
