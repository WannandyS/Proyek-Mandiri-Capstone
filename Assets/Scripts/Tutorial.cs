using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public static Tutorial instance;
    public GameObject tutorialPanel;
    private bool tutorialActive;

    public Player player;
    private string nextObjective;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        tutorialPanel.SetActive(false);

        player = FindAnyObjectByType<Player>();
    }

    void Update()
    {
        if (tutorialActive && Input.GetKeyDown(KeyCode.F))
        {
            CloseTutorial();
        }
    }

    public void ShowTutorial(string objectiveText)
    {
        Debug.Log("Tutorial Open");

        tutorialPanel.SetActive(true);
        tutorialActive = true;

        nextObjective = objectiveText;

        if (player != null)
        {
            player.canMove = false;
        }
        FindAnyObjectByType<AudioManager>().PlayTutorialSound();
    }

    private void CloseTutorial()
    {
        tutorialPanel.SetActive(false);
        tutorialActive = false;

        if (player != null)
        {
            player.canMove = true;
        }

        Objective.instance.SetObjective(nextObjective);
        Objective.instance.ShowObjective();
    }
}
