using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    public GameObject dialoguePanel;
    public TMP_Text dialogueText;

    private string[] currentLines;
    private int currentIndex;

    private Player player;

    private bool dialogueActive = false;
    public bool dialogueJustClosed;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        dialoguePanel.SetActive(false);

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

        if (Input.GetKeyDown(KeyCode.F))
        {
            NextLine();
        }
    }

    public bool IsDialogueActive()
    {
        return dialogueActive;
    }

    public void StartDialogue(string[] lines)
    {
        if (dialogueActive)
            return;

        currentLines = lines;
        currentIndex = 0;

        dialogueActive = true;

        dialoguePanel.SetActive(true);

        player.canMove = false;

        dialogueText.text = currentLines[currentIndex];
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

        dialogueText.text = currentLines[currentIndex];
    }

    private void EndDialogue()
    {
        dialoguePanel.SetActive(false);

        player.canMove = true;

        dialogueActive = false;

        dialogueJustClosed = true;
    }
}