using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private GameObject floatingTextPrefab;

    [SerializeField]
    private ParticleSystem hitParticle;

    [SerializeField]
    private ParticleSystem DeadParticle;

    private AudioSource audioSound;

    [SerializeField]
    private int maxHealth;

    private int currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
        audioSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int amount, Vector3 hitPoint)
    {
        if (IsDead())
        {
            return;
        }
    
        if (audioSound)
        {
            audioSound.Play();
        }

        currentHealth -= amount;

        if (hitParticle)
        {
            hitParticle.transform.position = hitPoint;
            hitParticle.transform.position += new Vector3(0.8f, 0f, 0f);
            hitParticle.Play();
        }

        if (IsDead())
        {
            Dead();
        }
        else
        {
            ShowFloatingText();
        }
    }

    void ShowFloatingText()
    {
        if (!floatingTextPrefab && !IsDead())
        {
            return;
        }

        var gameObject = Instantiate(floatingTextPrefab, transform.position, Quaternion.identity, transform);
        gameObject.GetComponent<TextMesh>().text = currentHealth.ToString();
    }

    void Dead()
    {
        if (DeadParticle)
        {
            DeadParticle.Play();
        }

        MakeInvisible();

        Destroy(gameObject, 1.0f);
    }

    void MakeInvisible()
    {
        gameObject.layer = 0;

        var meshRenderer = GetComponent<MeshRenderer>();
        if (meshRenderer)
        {
            meshRenderer.enabled = false;
        }

        var gun = GetComponentInChildren<Gun>();
        if (gun)
        {
            var gunMeshRenderers = gun.GetComponentsInChildren<MeshRenderer>();
            foreach(var renderer in gunMeshRenderers)
            {
                renderer.enabled = false;
            }
        }         
    }

    bool IsDead()
    {
        return currentHealth <= 0;
    }
}
