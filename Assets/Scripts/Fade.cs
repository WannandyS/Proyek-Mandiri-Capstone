using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Fade : MonoBehaviour
{
    public static Fade instance;
    public Image fadeImage;

    private void Awake()
    {
        instance = this;
    }

    public IEnumerator FadeOut(float duration)
    {
        float time = 0;

        while (time < duration)
        {
            time += Time.deltaTime;

            Color color = fadeImage.color;
            color.a = Mathf.Lerp(0, 1, time / duration);

            fadeImage.color = color;

            yield return null;
        }
    }

    public IEnumerator FadeIn(float duration)
    {
        float time = 0;

        while (time < duration)
        {
            time += Time.deltaTime;

            Color color = fadeImage.color;
            color.a = Mathf.Lerp(1, 0, time / duration);

            fadeImage.color = color;

            yield return null;
        }
    }
}
