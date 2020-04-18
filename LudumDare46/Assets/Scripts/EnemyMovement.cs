using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
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

        groundSensor = GetComponentInChildren<GroundSensor>();
        if (groundSensor == null)
            throw new System.Exception("Enemy Movement - Need GroundSensor");
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = groundSensor.GetIsTouchingGround();

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        Move(-1.0f);

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
