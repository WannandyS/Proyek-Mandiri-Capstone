using UnityEngine; //masih belum kelar

public class Boss : MonoBehaviour
{
    public GameObject interactionText;
    public bool playerInRange;
    private bool waitForKeyRelease;

    void Start()
    {
        interactionText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (DialogueManager.instance.IsDialogueActive())
            return;

        if (waitForKeyRelease)
        {
            if (!Input.GetKeyDown(KeyCode.F))
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
    }
}
