using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public Player player;

    public void PlayGame()
    {
        mainMenuPanel.SetActive(false);

        string[] intro =
        {
            "ha.... sudah pagi.", "saatnya siap-siap untuk bekerja."
        };

        DialogueManager.instance.StartDialogue(intro, true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
