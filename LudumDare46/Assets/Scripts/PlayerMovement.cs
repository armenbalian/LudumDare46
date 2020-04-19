using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Animator animator;

    bool isRunning = false;

    [SerializeField]
    Health health;

    [SerializeField]
    bool debug = false;

    [SerializeField]
    float defaultSpeed = 3.0f;

    float speed;

    [SerializeField]
    float gravity = -9.81f; //-19.62f;

    [SerializeField]
    float jumpHeight = 1f;

    bool isGrounded = false;

    GroundSensor groundSensor;
    CharacterController controller;

    Vector2 velocity;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        if (animator == null)
            throw new System.Exception("Player Movement - Need Animator");

        speed = defaultSpeed;

        controller = GetComponent<CharacterController>();
        if (controller == null)
            throw new System.Exception("Player Movement - Need CharacterController");

        groundSensor = GetComponentInChildren<GroundSensor>();
        if (groundSensor == null)
            throw new System.Exception("Player Movement - Need GroundSensor");

        if (health == null)
            throw new System.Exception("Player Movement - Need health");

        if (!debug)
        {
            StartCoroutine(StartRunning());
        }
    }

    IEnumerator StartRunning()
    {
        animator.SetBool("isRunning", false);
        yield return new WaitForSecondsRealtime(3.0f);
        animator.SetBool("isRunning", true);
        isRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = groundSensor.GetIsTouchingGround();

        var isJumping = animator.GetBool("isJumping");
        var isDocking = animator.GetBool("isDocking");

        if (isGrounded && isJumping)
        {
            animator.SetBool("isJumping", false);
            speed = defaultSpeed;
        }


        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (isRunning)
        {
            Move(1.0f);
        }


        if (isGrounded && !isJumping)
        {
            if (Input.GetButtonDown("Jump"))
            {
                health.isInvincible = false;
                animator.SetBool("isDocking", false);
                animator.SetBool("isJumping", true);
                groundSensor.resetAllStatus();

                Jump();
            }

            if (Input.GetButtonDown("Fire3") && !isDocking)
            {                
                StartCoroutine(Dock());
            }
        }
    }

    IEnumerator Dock()
    {
        health.isInvincible = true;
        animator.SetBool("isDocking", true);
        speed *= 1.5f;
        yield return new WaitForSecondsRealtime(3.0f);

        if (animator.GetBool("isDocking"))
        {
            health.isInvincible = false;
            speed = defaultSpeed;
            animator.SetBool("isDocking", false);
        }
    }

    void Move(float xMoveInput)
    {
        if (xMoveInput != 0)
        {
            Vector2 move = new Vector2(xMoveInput, 0.0f);
            controller.Move(move * speed * Time.deltaTime);
        }
    }   

    void Jump()
    {
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        controller.Move(velocity * Time.deltaTime);
    }    
}
