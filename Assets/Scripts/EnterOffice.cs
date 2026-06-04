using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterOffice : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject interactionText;
    private bool playerInRange;

    void Start()
    {
        interactionText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.F))
        {
            Interact();
        }
    }

    public void Interact()
    {
        StartCoroutine(ChangeScene());
    }

    IEnumerator ChangeScene()
    {
        FindFirstObjectByType<Player>().canMove = false;

        yield return StartCoroutine(Fade.instance.FadeOut(1f));

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene("Office");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
            interactionText.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
            interactionText.SetActive(false);
        }
    }
}
