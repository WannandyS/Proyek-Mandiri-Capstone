using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    public static MainMenu instance;

    public GameObject mainMenuPanel;
    public Animator playerAnimator;

    private void Awake()
    {
        instance = this;
    }

    public void PlayGame()
    {
        mainMenuPanel.SetActive(false);

        StartCoroutine(StartWakeup());
    }

    IEnumerator StartWakeup()
    {
        playerAnimator.SetTrigger("WakeUp");

        yield return new WaitForSeconds(2f);

        string[] intro =
        {
        "Ha.... sudah pagi.",
        "Saatnya siap-siap untuk bekerja."
    };

        DialogueManager.instance.StartDialogue(intro, true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}