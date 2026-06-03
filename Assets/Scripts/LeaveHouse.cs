using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeaveHouse : MonoBehaviour
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
        if (DialogueManager.instance.IsDialogueActive())
            return;

        if (DialogueManager.instance.dialogueJustClosed)
            return;

        if (playerInRange && Input.GetKeyDown(KeyCode.F))
        {
            Interact();
        }
    }

    public void Interact()
    {
        if (!Objective.instance.canLeaveHouse)
        {
            string[] dialogue =
            {
                "Aku belum siap berangkat"
            };

            DialogueManager.instance.StartDialogue(dialogue);
            return;
        }

        StartCoroutine(ChangeScene());
    }

    IEnumerator ChangeScene()
    {
        FindFirstObjectByType<Player>().canMove = false;

        yield return StartCoroutine(Fade.instance.FadeOut(1f));

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene("Street");
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
