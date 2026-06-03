using UnityEngine;
using System.Collections;

public class Toilet : MonoBehaviour
{
    public GameObject interactionText;
    private bool playerInRange;
    private bool alreadyUsed = false;

    void Start()
    {
        interactionText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.F) && !alreadyUsed)
        {
            StartCoroutine(ToiletQuest());
        }
    }

    IEnumerator ToiletQuest()
    {
        alreadyUsed = true;

        interactionText.SetActive(false);

        yield return StartCoroutine(Fade.instance.FadeOut(1f));

        yield return new WaitForSeconds(2f);

        yield return StartCoroutine(Fade.instance.FadeIn(1f));

        string[] dialogue =
        {
            "Sedikit lebih segar sekarang.",
            "Sudah pakai jas juga.",
            "Sekarang saatnya pergi ke kantor."
        };

        DialogueManager.instance.StartDialogue(dialogue);

        Objective.instance.SetObjective("Keluar rumah");
        Objective.instance.canLeaveHouse = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;

            if (!alreadyUsed)
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
