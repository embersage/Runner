using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private int lineToMove = 0;
    public float lineDistance = 4;
    private CharacterController controller;
    private Vector3 direction;
    private Score score;
    private CapsuleCollider capsule;
    private float currentTimeStar, durationStar;
    private float currentTimeShield, durationShield;
    public Image imageTimerStar;
    public Image imageTimerShield;
    public GameObject hitObstacle;
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravity;
    [SerializeField] public int coinsCount;
    [SerializeField] private GameObject scoreText;
    [SerializeField] public GameObject losePanel;
    [SerializeField] public GameObject pausePanel;
    [SerializeField] public Text coinsText;
    [SerializeField] private Score scoreScript;
    private bool IsImmortal;
    public AudioSource coinSound;
    public AudioSource starSound;
    public AudioSource shieldSound;
    public AudioSource obstacleSound;

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
            StartCoroutine(StarBonus());
            Destroy(bonus.gameObject);
            starSound.Play();
        }

        if (bonus.gameObject.tag == "BonusShield")
        {
            StartCoroutine(ShieldBonus());
            Destroy(bonus.gameObject);
            shieldSound.Play(); 
        }
    }

    private IEnumerator StarBonus()
    {
        currentTimeStar = durationStar = 10;
        StartCoroutine(StartTimerStar());
        score.scoreMultiplier = 1;

        yield return new WaitForSeconds(10);

        score.scoreMultiplier = 0.5f;
    }

    private IEnumerator ShieldBonus()
    {
        currentTimeShield = durationShield = 10;
        StartCoroutine(StartTimerShield());
        IsImmortal = true;

        yield return new WaitForSeconds(10);

        IsImmortal = false;
    }

    private IEnumerator StartTimerStar()
    {
        imageTimerStar.enabled = true;
        while (currentTimeStar >= 0)
        {
            imageTimerStar.fillAmount = Mathf.InverseLerp(0, durationStar, currentTimeStar);
            yield return new WaitForSeconds(1f);
            currentTimeStar -= 0.5f;
        }
        imageTimerStar.enabled = false;
    }

    private IEnumerator StartTimerShield()
    {
        imageTimerShield.enabled = true;
        while (currentTimeShield >= 0)
        {
            imageTimerShield.fillAmount = Mathf.InverseLerp(0, durationShield, currentTimeShield);
            yield return new WaitForSeconds(1f);
            currentTimeShield -= 0.5f;
        }
        imageTimerShield.enabled = false;
    }

    private void Jump()
    {
        direction.y = jumpForce;
        animator.SetTrigger("Jump");
        StartCoroutine(MovingCollider());
    }

    private IEnumerator MovingCollider()
    {
        capsule.center = new Vector3(0, 1.15f, 0);
        controller.center = new Vector3(0, 1.15f, 0);

        yield return new WaitForSeconds(0.8f);

        capsule.center = new Vector3(0, 0.15f, 0);
        controller.center = new Vector3(0, 0.15f, 0);
    }

    private void Start()
    {
        Time.timeScale = 1;
        losePanel.SetActive(false);
        pausePanel.SetActive(false);
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
        imageTimerStar.enabled = false;
        imageTimerShield.enabled = false;
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
            if (lineToMove < 4)
                lineToMove += 4;
        }

        if (SwipeController.swipeLeft || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (lineToMove > -4)
                lineToMove -= 4;
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

        if (lineToMove == -4)
            targetPosition += Vector3.left * lineDistance;
        else if (lineToMove == 4)
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