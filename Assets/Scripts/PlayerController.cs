using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 direction;
    private Score score;
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravity;
    [SerializeField] private int coinsCount;
    [SerializeField] private GameObject ScoreText;

    [SerializeField] private Text coinsText;

    private int lineToMove = 0;
    public float lineDistance = 3;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        score = ScoreText.GetComponent<Score>();
    }

    private void Update()
    {
        if (SwipeController.swipeRight)
        {
            if (lineToMove < 3) 
                lineToMove += 3;
        }

        if (SwipeController.swipeLeft)
        {
            if (lineToMove > -3) 
                lineToMove -= 3;
        }

        if (SwipeController.swipeUp)
        {
            if (controller.isGrounded) 
                Jump();
        }

        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;

        if (lineToMove == -3) 
            targetPosition += Vector3.left * lineDistance;

        else if (lineToMove == 3) 
            targetPosition += Vector3.right * lineDistance;

        if (transform.position == targetPosition) 
            return;

        Vector3 diff = targetPosition - transform.position;
        Vector3 moveDir = diff.normalized * 25 * Time.deltaTime;

        if (moveDir.sqrMagnitude < diff.sqrMagnitude) 
            controller.Move(moveDir);
        else 
            controller.Move(diff);
    }

    private void Jump()
    {
        direction.y = jumpForce;
    }

    private void FixedUpdate()
    {
        direction.y += gravity * Time.fixedDeltaTime;
        controller.Move(direction * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter(Collision enemy)
    {
        if (enemy.collider.GetComponent<ObstacleController>()) 
            Time.timeScale = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Coin")
        {
            coinsCount++;
            coinsText.text = (coinsCount).ToString();
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "BonusStar")
        {
            score.speed *= 2;
            //Debug.Log(score.scoreMultiplier);
        }
    }
}