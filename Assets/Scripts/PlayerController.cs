using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private int lineToMove = 0;
    public float lineDistance = 3;
    private CharacterController controller;
    private Vector3 direction;
    private Score score;
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravity;
    [SerializeField] private int coinsCount;
    [SerializeField] private GameObject scoreText;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private Text coinsText;
    [SerializeField] private Score scoreScript;
    private bool IsImmortal;

    private void OnCollisionEnter(Collision obstacle)
    {
        if (obstacle.collider.GetComponent<ObstacleController>())
        {
            if (IsImmortal)
                Destroy(obstacle.gameObject);
            else
            {
                Time.timeScale = 0;
                losePanel.SetActive(true);
                int lastRunScore = int.Parse(scoreScript.scoreText.text.ToString());
                PlayerPrefs.SetInt("lastRunScore", lastRunScore);
            }            
        }    
    }

    private void OnTriggerEnter(Collider bonus)
    {
        if (bonus.gameObject.tag == "Coin")
        {
            coinsCount++;
            coinsText.text = coinsCount.ToString();
            Destroy(bonus.gameObject);
        }

        if (bonus.gameObject.tag == "BonusStar")
        {
            StartCoroutine(StarBonus());
            Destroy(bonus.gameObject);
        }

        if (bonus.gameObject.tag == "BonusShield")
        {
            StartCoroutine(ShieldBonus());
            Destroy(bonus.gameObject);
        }
    }

    private IEnumerator StarBonus()
    {
        score.scoreMultiplier = 1;

        yield return new WaitForSeconds(5);

        score.scoreMultiplier = 0.5f;
    }

    private IEnumerator ShieldBonus()
    {
        IsImmortal = true;

        yield return new WaitForSeconds(10);

        IsImmortal = false;
    }

    private void Jump()
    {
        direction.y = jumpForce;
    }

    private void Start()
    {
        Time.timeScale = 1;
        losePanel.SetActive(false);
        controller = GetComponent<CharacterController>();
        score = scoreText.GetComponent<Score>();
        score.scoreMultiplier = 0.5f;
        IsImmortal = false;
        ObstacleController.speed = 30f;
    }

    private void FixedUpdate()
    {
        direction.y += gravity * Time.fixedDeltaTime;
        controller.Move(direction * Time.fixedDeltaTime);
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
}