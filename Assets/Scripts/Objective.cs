using UnityEngine;
using TMPro;

public class Objective : MonoBehaviour
{
    public static Objective instance;
    public TMP_Text objectiveText;
    public bool canLeaveHouse = false;

    private void Awake()
    {
        instance = this;
    }

    public void SetObjective(string objective)
    {
        objectiveText.text = "Objective:\n" + objective;
    }

    private void Start()
    {
        objectiveText.gameObject.SetActive(false);
    }

    public void ShowObjective()
    {
        objectiveText.gameObject.SetActive(true);
    }

    public void HideObjective()
    {
        objectiveText.gameObject.SetActive(false);
    }
}
