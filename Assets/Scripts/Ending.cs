using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ending : MonoBehaviour
{
    public GameObject panel;
    public TMP_Text endingText;

    IEnumerator Start()
    {
        panel.SetActive(false);
        yield return StartCoroutine(Fade.instance.FadeIn(1));
        yield return new WaitForSeconds(1);

        yield return AutoDialogue.instance.ShowDialogue("teks 1", 3);
        yield return AutoDialogue.instance.ShowDialogue("teks 2", 3);
        yield return AutoDialogue.instance.ShowDialogue("teks 3", 3);

        yield return StartCoroutine(Fade.instance.FadeOut(1.5f));
        panel.SetActive(true);

        StartCoroutine(ShowEndingText());
    }

    IEnumerator ShowEndingText()
    {
        yield return StartCoroutine(ShowText("Pekerjaan akan selalu ada", 2f));
        yield return StartCoroutine(ShowText("Namun waktu untuk diri sendiri juga penting", 2f));
        yield return StartCoroutine(ShowText("Menjaga keseimbangan antara pekerjaan", 1.5f));
        yield return StartCoroutine(ShowText("Dan kehidupan pribadi membantu kita", 1.5f));
        yield return StartCoroutine(ShowText("tetap sehat dan produktif", 1.5f));
        yield return StartCoroutine(ShowText("Work-Life Balance", 2f));
        yield return StartCoroutine(ShowText("THE END", 2f));

        yield return new WaitForSeconds(1f);

        WorkProgress.stage = 0;

        SceneManager.LoadScene("Home");
    }

    IEnumerator ShowText(string message, float displayTime)
    {
        endingText.text = message;

        Color color = endingText.color;
        color.a = 0f;
        endingText.color = color;

        float timer = 0f;

        while (timer < 1f)
        {
            timer += Time.deltaTime;

            color.a = Mathf.Lerp(0f, 1f, timer);
            endingText.color = color;

            yield return null;
        }

        yield return new WaitForSeconds(displayTime);

        timer = 0f;

        while (timer < 1f)
        {
            timer += Time.deltaTime;

            color.a = Mathf.Lerp(1f, 0f, timer);
            endingText.color = color;

            yield return null;
        }
    }
}
