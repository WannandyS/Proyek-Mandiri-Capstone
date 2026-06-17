using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static Spawner instance;

    public GameObject document;
    public Transform[] spawnPoints;

    private int collectedCount;

    private void Awake()
    {
        instance = this;
    }

    public void OnDocumentCollected()
    {
        collectedCount++;

        Debug.Log("Collected Count = " + collectedCount);

        // Dokumen ke-2
        if (collectedCount == 2)
        {
            SpawnDocument();
        }

        // Setelah itu setiap dokumen
        else if (collectedCount > 2)
        {
            SpawnDocument();
        }
    }

    public void SpawnDocument()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.Log("Spawn Point Kosong!");
            return;
        }

        int randomIndex = Random.Range(0, spawnPoints.Length);

        Debug.Log("Spawn di : " + spawnPoints[randomIndex].name);

        Instantiate(document, spawnPoints[randomIndex].position, Quaternion.identity);
    }
}