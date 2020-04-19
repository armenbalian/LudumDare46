using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    GameObject floatingTextPrefab;

    [SerializeField]
    ParticleSystem hitParticle;

    [SerializeField]
    ParticleSystem DeadParticle;
    

    [SerializeField]
    AudioSource audioSound;

    [SerializeField]
    int maxHealth;

    int currentHealth;

    Animator animator;

    public bool isInvincible = false;

    void Awake()
    {
        currentHealth = maxHealth;

        animator = GetComponentInChildren<Animator>();
        if (animator == null)
            throw new System.Exception("Health - Need a animator !");
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
            bool randomSound = Random.Range(0, 6) == 0;
            if(randomSound)
                audioSound.Play();
        }

        currentHealth -= amount;

        if (hitParticle)
        {
            hitParticle.transform.position = hitPoint;
            hitParticle.transform.position += new Vector3(0.8f, 1f, 0f);
            hitParticle.Play();
        }

        ShowFloatingText(amount);

        if (IsDead())
        {
            Dead();
        }
    }

    void ShowFloatingText(float damage)
    {
        if (!floatingTextPrefab && !IsDead())
        {
            return;
        }

        var gameObject = Instantiate(floatingTextPrefab, transform.position, Quaternion.identity, transform);
        gameObject.GetComponent<TextMesh>().text = damage.ToString();
    }

    void Dead()
    {                
        DeadParticle.Play();

        animator.SetBool("isDying", true);

        StartCoroutine(KillerAndDestroy());
    }

    IEnumerator KillerAndDestroy()
    {
        RemoveColliderAndController();

        yield return new WaitForSeconds(2.0f);

        //MakeInvisible();
        Destroy(gameObject, 1.0f);
    }

    void RemoveColliderAndController()
    {
        var boxCollider = GetComponent<BoxCollider>();
        if (boxCollider)
        {
            boxCollider.enabled = false;
        }

        var characterController = GetComponent<CharacterController>();
        if (characterController)
        {
            characterController.enabled = false;
        }
    }

    void MakeInvisible()
    {
        GameObjectUtils.SetActive(gameObject, false);
    }

    public bool IsDead()
    {
        return currentHealth <= 0;
    }
}
