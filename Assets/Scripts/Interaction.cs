using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Interaction : MonoBehaviour
{
    public GameObject interactionText;

    [TextArea(3, 5)]
    public string[] dialogue;

    private bool playerInRange;
    private bool waitForKeyRelease;
    private bool alreadyUsed;

    void Start()
    {
        interactionText.SetActive(false);
    }

    void Update()
    {
        if (DialogueManager.instance.IsDialogueActive())
            return;

        if (DialogueManager.instance.dialogueJustClosed)
            return;

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

    public void Interact()
    {
        waitForKeyRelease = true;
        interactionText.SetActive(false);

        Debug.Log("Interact: " + gameObject.tag);

        if (gameObject.tag == "NPC")
        {
            DialogueManager.instance.StartDialogue(dialogue);
        }

        if (gameObject.tag == "Boss")
        {
            BossInteraction();
        }

        if (gameObject.tag == "Toilet")
        {
            if (!alreadyUsed)
            {
                StartCoroutine(ToiletQuest());
            }
        }

        if (gameObject.tag == "LeaveHouse")
        {
            LeaveHouse();
        }

        if (gameObject.tag == "LeaveOffice")
        {
            StartCoroutine(ChangeScene("CutsceneHouse"));
        }

        if (gameObject.tag == "EnterOffice")
        {
            StartCoroutine(ChangeScene("Office"));
        }

        if (gameObject.tag == "WorkDesk")
        {
            GoToWorkDesk();
        }
    }

    public void BossInteraction()
    {
        if (Quest.instance.questStage == 0)
        {
            string[] dialogue =
            {
                "Nah, akhirnya kamu datang tepat waktu",
                "Ada pekerjaan untukmu",
                "Tolong ambilkan 2 dokumen di meja"
            };

            DialogueManager.instance.StartDialogue(dialogue);

            Objective.instance.SetObjective("Ambil 2 Dokumen");

            Quest.instance.questStage = 1;
            Quest.instance.document1.SetActive(true);
            Quest.instance.document2.SetActive(true);
        }
        else if (Quest.instance.questStage == 1)
        {
            string[] dialogue =
            {
                "Kamu belum mengambil semua dokumennya"
            };

            DialogueManager.instance.StartDialogue(dialogue);
        }
        else if (Quest.instance.questStage == 2)
        {
            string[] dialogue =
            {
                "Bagus",
                "Dokumennya sudah lengkap",
                "Sekarang pergi ke meja kerjamu"
            };

            DialogueManager.instance.StartDialogue(dialogue);

            Objective.instance.SetObjective("Pergi ke meja");

            Quest.instance.questStage = 3;
        }
        else if (Quest.instance.questStage == 3)
        {
            string[] dialogue =
            {
                "Mejamu ada di sebelah kiri yang kosong itu"
            };

            DialogueManager.instance.StartDialogue(dialogue);
        }
    }

    public void LeaveHouse()
    {
        StartCoroutine(ChangeScene("Street"));
    }

    IEnumerator ToiletQuest()
    {
        alreadyUsed = true;

        Player player = FindFirstObjectByType<Player>();

        if (player != null)
        {
            player.canMove = false;
        }

        yield return StartCoroutine(Fade.instance.FadeOut(1f));

        yield return new WaitForSeconds(0.5f);

        SceneManager.LoadScene("AfterQuestToilet");
    }

    IEnumerator ChangeScene(string sceneName)
    {
        Debug.Log("Pindah scene " + sceneName);

        Player player = FindFirstObjectByType<Player>();

        if (player != null)
        {
            player.canMove = false;
        }

        yield return StartCoroutine(Fade.instance.FadeOut(1.5f));

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(sceneName);
    }

    public void GoToWorkDesk()
    {
        if (Quest.instance.questStage != 3)
            return;

        WorkProgress.stage = 0;

        StartCoroutine(ChangeScene("WorkDesk"));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;

            if (!DialogueManager.instance.IsDialogueActive())
            {
                interactionText.SetActive(true);
            }

            if (gameObject.tag == "WorkDesk")
            {
                interactionText.SetActive(Quest.instance.questStage == 3);
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