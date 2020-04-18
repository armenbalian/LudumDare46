using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    bool isRunning = true;

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
        speed = defaultSpeed;

        controller = GetComponent<CharacterController>();
        if (controller == null)
            throw new System.Exception("Player Movement - Need CharacterController");

        groundSensor = GetComponentInChildren<GroundSensor>();
        if (groundSensor == null)
            throw new System.Exception("Player Movement - Need GroundSensor");
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = groundSensor.GetIsTouchingGround();

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
        
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
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
