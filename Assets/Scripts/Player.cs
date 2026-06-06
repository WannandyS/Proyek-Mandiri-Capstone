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

    public GameObject gameOverPanel;
    private bool isDead = false;

    private bool canJumpInThisScene;
    public bool canMove = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        string sceneName = SceneManager.GetActiveScene().name;

        canJumpInThisScene =
            sceneName == "Street";

        if (sceneName != "Street")
        {
            gameOverPanel.SetActive(false);
        }
    }

    void Update()
    {
        if (!canMove)
            return;

        if (!isDead && transform.position.y <= -5.9f)
        {
            GameOver();
        }

        movement = Input.GetAxis("Horizontal");
        transform.position += new Vector3 (movement, 0, 0) * Time.deltaTime * speed;

        if (canJumpInThisScene && Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    public void Jump()
    {
        if (isGround)
        {
            rb.linearVelocity = new Vector2 (rb.linearVelocity.x, jumpHeight);
        }
    }

    public void GameOver()
    {
        Debug.Log("Player fall into the hole");
        isDead = true;
        canMove = false;
        rb.linearVelocity = Vector2.zero;
        gameOverPanel.LeanMoveLocalY(0f, 0.8f).setEaseOutExpo();
        gameOverPanel.SetActive(true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = false;
        }
    }
}