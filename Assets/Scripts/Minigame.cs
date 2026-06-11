using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Minigame : MonoBehaviour
{
    public int targetDocument = 4;
    public float timer = 25f;

    private int collected;

    IEnumerator Start()
    {
        yield return Fade.instance.StartCoroutine(Fade.instance.FadeIn(1f));
        yield return new WaitForSeconds(1f);
    }
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
