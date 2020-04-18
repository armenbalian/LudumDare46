using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralMovement : MonoBehaviour
{
    [SerializeField]
    private bool usePhysics = false;

    [SerializeField]
    private bool isWalking = false;

    private float speed = 6.0f;

    private float gravity = -9.81f; //-19.62f;
    private bool isGrounded = false;

    GroundSensor groundSensor;
    CharacterController controller;

    Vector2 velocity;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        if (controller == null)
            throw new System.Exception("Enemy Movement - Need CharacterController");

        if (usePhysics)
        {
            groundSensor = GetComponentInChildren<GroundSensor>();
            if (groundSensor == null)
                throw new System.Exception("Enemy Movement - Need GroundSensor");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (usePhysics)
        {
            isGrounded = groundSensor.GetIsTouchingGround();

            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }
        }

        if (isWalking)
        {
            Move(-1.0f);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void Move(float xMoveInput)
    {
        if (xMoveInput != 0)
        {
            Vector2 move = new Vector2(xMoveInput, 0.0f);
            controller.Move(move * speed * Time.deltaTime);
        }
    }
}
