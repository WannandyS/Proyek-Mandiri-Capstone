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

    private bool canJumpInThisScene;
    public bool canMove = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        string sceneName = SceneManager.GetActiveScene().name;

        canJumpInThisScene =
            sceneName == "Street";
    }

    void Update()
    {
        if (!canMove)
            return;

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