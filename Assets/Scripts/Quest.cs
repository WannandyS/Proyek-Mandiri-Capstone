using UnityEngine;

public class Quest : MonoBehaviour
{
    public static Quest instance;

    public int questStage;
    public int documentCollected;

    public GameObject document1;
    public GameObject document2;

    void Start()
    {
        document1.SetActive(false);
        document2.SetActive(false);
    }

    private void Awake()
    {
        instance = this;
    }

    public void CollectDocument()
    {
        documentCollected++;

        Debug.Log("Document: " + documentCollected);
    }
}
