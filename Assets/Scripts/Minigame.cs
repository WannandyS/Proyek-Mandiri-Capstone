using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Minigame : MonoBehaviour
{
    public int targetDocument = 4;
    public float timer = 25f;

    private int collected;

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            FinishStage();
        }
    }

    public void CollectDocument()
    {
        collected++;

        if (collected >= targetDocument)
        {
            FinishStage();
        }
    }

    void FinishStage()
    {
        enabled = false;

        StartCoroutine(NextStage());
    }

    IEnumerator NextStage()
    {
        yield return StartCoroutine(Fade.instance.FadeOut(1f));

        WorkProgress.stage++;

        SceneManager.LoadScene("WorkDesk");
    }
}
