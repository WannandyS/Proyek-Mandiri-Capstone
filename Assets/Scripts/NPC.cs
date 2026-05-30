using UnityEngine;

public class NPC : MonoBehaviour
{
    [Header("Interaction")]
    public GameObject interactionText;

    [Header("Dialogue")]
    [TextArea(3, 5)]
    public string[] dialogue;

    private bool playerInRange;
    private bool waitForKeyRelease = false;

    void Start()
    {
        interactionText.SetActive(false);
    }

    void Update()
    {
        // Jika dialog sedang aktif, NPC tidak menerima input
        if (DialogueManager.Instance.IsDialogueActive())
            return;

        // Tunggu tombol F dilepas dulu
        if (waitForKeyRelease)
        {
            if (!Input.GetKey(KeyCode.F))
            {
                waitForKeyRelease = false;
            }

            return;
        }

        if (playerInRange && Input.GetKeyDown(KeyCode.F))
        {
            Interact();
        }
    }

    private void Interact()
    {
        waitForKeyRelease = true;

        interactionText.SetActive(false);

        Debug.Log("INTERACT");

        DialogueManager.Instance.StartDialogue(dialogue);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;

            if (!DialogueManager.Instance.IsDialogueActive())
            {
                interactionText.SetActive(true);
            }
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