using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private int lineToMove = 0;
    public float lineDistance = 3;
    private CharacterController controller;
    private Vector3 direction;
    private Score score;
    private CapsuleCollider capsule;
    public GameObject hitObstacle;
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravity;
    [SerializeField] public int coinsCount;
    [SerializeField] private GameObject scoreText;
    [SerializeField] public GameObject losePanel;
    [SerializeField] public Text coinsText;
    [SerializeField] private Score scoreScript;
    private bool IsImmortal;
    public AudioSource coinSound;
    public AudioSource starSound;
    public AudioSource shieldSound;
    public AudioSource obstacleSound;

    private float currentTime, duration;
    public Image imageTimer;

    private void OnCollisionEnter(Collision obstacle)
    {
        if (obstacle.collider.GetComponent<ObstacleController>())
        {
            hitObstacle = obstacle.gameObject;
            if (IsImmortal)
                Destroy(obstacle.gameObject);
            else
            {
                Time.timeScale = 0;
                obstacleSound.Play();
                int lastRunScore = int.Parse(scoreScript.scoreText.text.ToString());
                PlayerPrefs.SetInt("lastRunScore", lastRunScore);
                
                losePanel.SetActive(true);
                obstacleSound.Play();                
            }
        }
    }

    private void OnTriggerEnter(Collider bonus)
    {
        if (bonus.gameObject.tag == "Coin")
        {
            coinsCount++;
            PlayerPrefs.SetInt("coins", coinsCount);
            coinsText.text = coinsCount.ToString();
            Destroy(bonus.gameObject);
            coinSound.Play();
        }

        if (bonus.gameObject.tag == "BonusStar")
        {
            currentTime = duration = 5;
            StartCoroutine(StarBonus());
            Destroy(bonus.gameObject);
            starSound.Play();
        }

        if (bonus.gameObject.tag == "BonusShield")
        {
            currentTime = duration = 10;
            StartCoroutine(ShieldBonus());
            Destroy(bonus.gameObject);
            shieldSound.Play();
        }
    }

    private IEnumerator StarBonus()
    {
        StartCoroutine(StartTimer());
        score.scoreMultiplier = 1;
        
        yield return new WaitForSeconds(5);
        
        score.scoreMultiplier = 0.5f;
    }

    private IEnumerator ShieldBonus()
    {
        StartCoroutine(StartTimer());
        IsImmortal = true;

        yield return new WaitForSeconds(10);
        
        IsImmortal = false;
    }

    private IEnumerator MovingCollider()
    {
        capsule.center = new Vector3(0, 1.15f, 0);
        controller.center = new Vector3(0, 1.15f, 0);

        yield return new WaitForSeconds(0.8f);

        capsule.center = new Vector3(0, 0.15f, 0);
        controller.center = new Vector3(0, 0.15f, 0);
    }

    private IEnumerator StartTimer()
    {
        imageTimer.enabled = true;
        while (currentTime >= 0)
        {
            imageTimer.fillAmount = Mathf.InverseLerp(0, duration, currentTime);
            yield return new WaitForSeconds(1f);
            currentTime -= 0.5f;
        }
        imageTimer.enabled = false;
    }

    private void Jump()
    {
        direction.y = jumpForce;
        animator.SetTrigger("Jump");
        StartCoroutine(MovingCollider());
    }

    private void Start()
    {
        Time.timeScale = 1;
        losePanel.SetActive(false);
        animator = GetComponentInChildren<Animator>();
        controller = GetComponent<CharacterController>();
        score = scoreText.GetComponent<Score>();
        capsule = GetComponent<CapsuleCollider>();
        score.scoreMultiplier = 0.5f;
        IsImmortal = false;
        ObstacleController.speed = 30f;
        PlayerPrefs.SetInt("recordScore", 0);
        PlayerPrefs.SetInt("coins", 250);
        coinsCount = PlayerPrefs.GetInt("coins");
        coinsText.text = coinsCount.ToString();
        capsule = GetComponent<CapsuleCollider>();
        imageTimer.enabled = false;
    }

    private void FixedUpdate()
    {
        direction.y += gravity * Time.fixedDeltaTime;
        controller.Move(direction * Time.fixedDeltaTime);
    }

    private void Update()
    {
        if (SwipeController.swipeRight || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (lineToMove < 3)
                lineToMove += 3;
        }

        if (SwipeController.swipeLeft || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (lineToMove > -3)
                lineToMove -= 3;
        }

        if (SwipeController.swipeUp || Input.GetKeyDown(KeyCode.Space))
        {
            if (controller.isGrounded)
                Jump();
        }

        if (controller.isGrounded)
            animator.SetBool("isRunning", true);
        else
            animator.SetBool("isRunning", false);

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