using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    Rigidbody rigidbody;
    SphereCollider sphereCollider;
    MeshRenderer meshRenderer;

    ExplosionDetection explosionDetection;

    [SerializeField]
    ParticleSystem particleSystemExplosion;

    bool isActive = false;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        if (rigidbody == null)
            throw new System.Exception("Grenade - Need a RigidBody !");

        sphereCollider = GetComponent<SphereCollider>();
        if (sphereCollider == null)
            throw new System.Exception("Grenade - Need a sphere collider !");

        meshRenderer = GetComponentInChildren<MeshRenderer>();
        if (meshRenderer == null)
            throw new System.Exception("Grenade - Need a Mesh Renderer !");

        if (particleSystemExplosion == null)
            throw new System.Exception("Grenade - Need a Particle System !");

        explosionDetection = particleSystemExplosion.GetComponentInChildren<ExplosionDetection>();
        if (explosionDetection == null)
            throw new System.Exception("Grenade - Need a Explosion Detection !");

        meshRenderer.enabled = false;
        rigidbody.isKinematic = false;      
    }

    public void SetActive(bool active)
    {
        isActive = active;
        rigidbody.useGravity = active;
        meshRenderer.enabled = active;
    }

    public bool GetIsActive()
    {
        return isActive;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player")
        {
            if (GetIsActive())
            {
                particleSystemExplosion.transform.position = gameObject.transform.position;
                particleSystemExplosion.Play();

                SetActive(false);
            }
        }        
    }
}
