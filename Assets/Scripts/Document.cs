using UnityEngine;

public class Document : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (Quest.instance.questStage != 1)
                return;

            Quest.instance.documentCollected++;
            Destroy(gameObject);

            if (Quest.instance.documentCollected >= 2)
            {
                Objective.instance.SetObjective("Kembali ke bos");
                Quest.instance.questStage = 2;
            }
        }
    }
}
