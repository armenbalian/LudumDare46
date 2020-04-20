using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    Rigidbody rb;
    SphereCollider sphereCollider;
    MeshRenderer[] meshRenderers;

    ExplosionDetection explosionDetection;

    AudioSource audioSource;

    [SerializeField]
    ParticleSystem particleSystemExplosion;

    bool isActive = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
            throw new System.Exception("Grenade - Need a RigidBody !");

        sphereCollider = GetComponent<SphereCollider>();
        if (sphereCollider == null)
            throw new System.Exception("Grenade - Need a sphere collider !");

        meshRenderers = GetComponentsInChildren<MeshRenderer>();
        if (meshRenderers.Length == 0)
            throw new System.Exception("Grenade - Need a Mesh Renderer !");

        if (particleSystemExplosion == null)
            throw new System.Exception("Grenade - Need a Particle System !");

        explosionDetection = particleSystemExplosion.GetComponentInChildren<ExplosionDetection>();
        if (explosionDetection == null)
            throw new System.Exception("Grenade - Need a Explosion Detection !");

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            throw new System.Exception("Grenade - Need Audio Source");

        foreach (var mesh in meshRenderers)
        {
            mesh.enabled = false;
        }

        rb.isKinematic = false;      
    }

    public void SetActive(bool active)
    {
        isActive = active;
        rb.useGravity = active;
        foreach (var mesh in meshRenderers)
        {
            mesh.enabled = active;
        }
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

                audioSource.Play();

                SetActive(false);
            }
        }        
    }
}
