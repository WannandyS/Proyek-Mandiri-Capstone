using UnityEngine;
using UnityEngine.SceneManagement;

public class Document : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        string sceneName = SceneManager.GetActiveScene().name;

        if (sceneName == "Office")
        {
            if (Quest.instance.questStage != 1)
                return;

            Quest.instance.documentCollected++;

            if (Quest.instance.documentCollected >= 1)
            {
                Objective.instance.SetObjective("Kembali ke bos");
                Quest.instance.questStage = 2;
            }
        } else
        {
            Minigame minigame = FindFirstObjectByType<Minigame>();

            if (minigame != null)
            {
                minigame.CollectDocument();
            }

            if (sceneName == "MinigameHard")
            {
                if (Spawner.instance != null)
                {
                    Spawner.instance.OnDocumentCollected();
                }
            }
        }
        Destroy(gameObject);
    }
}