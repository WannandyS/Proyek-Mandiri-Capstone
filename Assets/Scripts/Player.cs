using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    private float movement;
    public float speed = 3f;
    public float jumpHeight = 5f;

    private bool isGround;

    private Rigidbody2D rb;
    private Animator animator;

    public GameObject gameOverPanel;
    private bool isDead = false;

    private bool canJumpInThisScene;
    public bool canMove = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        string sceneName = SceneManager.GetActiveScene().name;

        canJumpInThisScene =
            sceneName == "Street" ||
            sceneName == "MinigameEasy" ||
            sceneName == "MinigameHard";

        if (gameOverPanel != null && sceneName != "Street")
        {
            gameOverPanel.SetActive(false);
        }
    }

    void Update()
    {
        movement = Input.GetAxisRaw("Horizontal");

        if (animator != null)
        {
            animator.SetFloat("Walk", Mathf.Abs(movement));
        }

        if (!canMove)
            return;

        if (!isDead && transform.position.y <= -5.9f)
        {
            GameOver();
        }

        transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * speed;

        if (movement > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (movement < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (canJumpInThisScene && Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    public void Jump()
    {
        if (isGround)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpHeight);
        }

        animator.SetBool("Jump", true);
    }

    public void GameOver()
    {
        Debug.Log("Player fall into the hole");

        isDead = true;
        canMove = false;

        rb.linearVelocity = Vector2.zero;

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
            gameOverPanel.LeanMoveLocalY(0f, 0.8f).setEaseOutExpo();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = true;
        }

        if (animator != null)
        {
            animator.SetBool("Jump", false);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = false;
        }

        if (animator != null)
        {
            animator.SetBool("Jump", true);
        }
    }
}