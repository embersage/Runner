using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 direction;
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravity;

    private int lineToMove = 1;
    public float lineDistance = 4;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Jump()
    {
        direction.y = jumpForce;
    }

    private void Update()
    {
        if (SwipeController.swipeRight)
        {
            if (lineToMove < 2) 
                lineToMove++;
        }

        if (SwipeController.swipeLeft)
        {
            if (lineToMove > 0) 
                lineToMove--;
        }

        if (SwipeController.swipeUp)
        {
            if (controller.isGrounded) 
                Jump();
        }

        Vector3 targetPosition = transform.position.x * transform.right + transform.position.y * transform.up;

        if (lineToMove == 0) 
            targetPosition += Vector3.forward * lineDistance;
        else if (lineToMove == 2) 
            targetPosition += Vector3.back * lineDistance;

        transform.position = targetPosition;
    }

    private void FixedUpdate()
    {
        direction.y += gravity * Time.fixedDeltaTime;
        controller.Move(direction * Time.fixedDeltaTime);
    }
}