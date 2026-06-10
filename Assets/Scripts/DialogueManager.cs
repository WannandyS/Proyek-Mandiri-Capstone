using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    public GameObject dialoguePanel;
    public TMP_Text dialogueText;

    private string[] currentLines;
    private int currentIndex;

    private Player player;

    private bool dialogueActive = false;
    public bool dialogueJustClosed;
    private bool showTutorialAfterDialogue;
    private bool waitForKeyRelease;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(false);
        }

        player = FindFirstObjectByType<Player>();
    }

    private void Update()
    {
        if (dialogueJustClosed)
        {
            if (!Input.GetKey(KeyCode.F))
            {
                dialogueJustClosed = false;
            }
        }

        if (!dialogueActive)
            return;

        if (waitForKeyRelease)
        {
            if (!Input.GetKey(KeyCode.F))
            {
                waitForKeyRelease = false;
            }
            return;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            NextLine();
        }
    }

    public bool IsDialogueActive()
    {
        return dialogueActive;
    }

    public void StartDialogue(string[] lines, bool showTutorial = false)
    {
        if (dialogueActive)
            return;

        if (Objective.instance != null)
        {
            Objective.instance.HideObjective();
        }

        currentLines = lines;
        currentIndex = 0;

        showTutorialAfterDialogue = showTutorial;

        dialogueActive = true;

        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(true);
        }

        waitForKeyRelease = true;

        if (player != null)
        {
            player.canMove = false;
        }

        if (dialogueText != null && currentLines.Length > 0)
        {
            dialogueText.text = currentLines[currentIndex];
        }
    }

    private void NextLine()
    {
        currentIndex++;

        Debug.Log("Current Index = " + currentIndex);
        Debug.Log("Total Lines = " + currentLines.Length);

        if (currentIndex >= currentLines.Length)
        {
            Debug.Log("END DIALOGUE");
            EndDialogue();
            return;
        }

        if (dialogueText != null)
        {
            dialogueText.text = currentLines[currentIndex];
        }
    }

    private void EndDialogue()
    {
        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(false);
        }

        if (player != null)
        {
            player.canMove = true;
        }

        dialogueActive = false;
        dialogueJustClosed = true;

        if (showTutorialAfterDialogue)
        {
            Invoke(nameof(ShowTutorial), 0.1f);

            showTutorialAfterDialogue = false;
        }
        else
        {
            if (Objective.instance != null)
            {
                Objective.instance.ShowObjective();
            }
        }
    }

    private void ShowTutorial()
    {
        if (Tutorial.instance != null)
        {
            Tutorial.instance.ShowTutorial("Pergi mandi");
        }
    }
}