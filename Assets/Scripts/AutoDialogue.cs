using TMPro;
using UnityEngine;
using System.Collections;

public class AutoDialogue : MonoBehaviour
{
    public static AutoDialogue instance;

    public GameObject dialoguePanel;
    public TMP_Text dialogueText;

    private void Awake()
    {
        instance = this;
    }

    public IEnumerator ShowDialogue(string text, float duration)
    {
        dialoguePanel.SetActive(true);
        dialogueText.text = text;

        yield return new WaitForSeconds(duration);

        dialoguePanel.SetActive(false);
    }
}
