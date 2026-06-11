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

    // Update is called once per frame
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

        //interact untuk NPC
        if (gameObject.tag == "NPC")
        {
            DialogueManager.instance.StartDialogue(dialogue);
        }

        if (gameObject.tag == "Boss")
        {
            BossInteraction();
        }

        //interact untuk masuk ke toilet
        if (gameObject.tag == "Toilet")
        {
            if (!alreadyUsed)
            {
                StartCoroutine(ToiletQuest());
            }
        }

        //interact keluar rumah
        if (gameObject.tag == "LeaveHouse")
        {
            LeaveHouse();
        }

        //interact keluar kantor
        if (gameObject.tag == "LeaveOffice")
        {
            StartCoroutine(ChangeScene("CutsceneHouse"));
        }

        //interact masuk ke kantor
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
                "Nah, akhirnya kamu datang tepat waktu", "Ada pekerjaan untukmu", "Tolong ambilkan 2 dokumen di meja"
            };

            DialogueManager.instance.StartDialogue(dialogue);
            Objective.instance.SetObjective("Ambil 2 Dokumen");
            Quest.instance.questStage = 1;
            Quest.instance.document1.SetActive(true);
            Quest.instance.document2.SetActive(true);

        } else if (Quest.instance.questStage == 1)
        {
            string[] dialogue =
            {
                "Kamu belum mengambil semua dokumennya"
            };

            DialogueManager.instance.StartDialogue(dialogue);

        } else if (Quest.instance.questStage == 2)
        {
            string[] dialogue =
            {
                "Bagus", "Dokumennya sudah lengkap", "Sekarang pergi ke meja kerjamu"
            };

            DialogueManager.instance.StartDialogue(dialogue);
            Objective.instance.SetObjective("Pergi ke meja");
            Quest.instance.questStage = 3;

        } else if (Quest.instance.questStage == 3)
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
        if (!Objective.instance.canLeaveHouse)
        {
            string[] dialogue =
            {
                "Aku belum siap berangkat"
            };

            DialogueManager.instance.StartDialogue(dialogue);
            return;
        }

        StartCoroutine(ChangeScene("Street"));
    }

    IEnumerator ToiletQuest()
    {
        alreadyUsed = true;
        FindFirstObjectByType<Player>().canMove = false;

        yield return StartCoroutine(Fade.instance.FadeOut(1f));
        yield return new WaitForSeconds(1);
        yield return StartCoroutine(Fade.instance.FadeIn(1f));

        FindFirstObjectByType<Player>().canMove = true;

        string[] toiletDialogue =
        {
            "Sudah lebih segar sekarang", "Sudah pakai jas juga", "Saatnya berangkat"
        };

        DialogueManager.instance.StartDialogue(toiletDialogue);
        Objective.instance.canLeaveHouse = true;
        Objective.instance.SetObjective("Keluar rumah");
    }

    IEnumerator ChangeScene(string sceneName)
    {
        Debug.Log("Pindah scene " + sceneName);
        FindFirstObjectByType<Player>().canMove = false;

        yield return StartCoroutine(Fade.instance.FadeOut(1.5f));
        yield return new WaitForSeconds(1);

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
        if (collision.gameObject.tag == "Player")
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
        if (collision.gameObject.tag == "Player")
        {
            playerInRange = false;
            interactionText.SetActive(false);
        }
    }
}
