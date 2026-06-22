using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ending : MonoBehaviour
{
    public GameObject panel;
    public TMP_Text endingText;
    public Camera mainCamera;
    public float cameraMoveDistance = 5f;
    public float cameraMoveDuration = 3f;

    IEnumerator Start()
    {
        panel.SetActive(false);
        yield return StartCoroutine(Fade.instance.FadeIn(3.5f));
        yield return new WaitForSeconds(1);

        FindFirstObjectByType<AudioManager>().PlayCharacterSound();
        yield return AutoDialogue.instance.ShowDialogue("Akhirnya bisa beristirahat", 3);
        FindFirstObjectByType<AudioManager>().PlayCharacterSound();
        yield return AutoDialogue.instance.ShowDialogue("Tapi besok kerjaan pasti akan datang lagi", 3);
        FindFirstObjectByType<AudioManager>().PlayCharacterSound();
        yield return AutoDialogue.instance.ShowDialogue("Tapi setidaknya aku bisa beristirahat untuk sesaat hari ini", 3);
        

        yield return StartCoroutine(CameraMoveFade());
        panel.SetActive(true);

        StartCoroutine(ShowEndingText());
    }

    IEnumerator CameraMoveFade()
    {
        Vector3 startPos = mainCamera.transform.position;
        Vector3 targetPos = startPos + new Vector3(0f, cameraMoveDistance, 0f);

        float timer = 0f;

        StartCoroutine(Fade.instance.FadeOut(cameraMoveDuration));

        while (timer < cameraMoveDuration)
        {
            timer += Time.deltaTime;

            mainCamera.transform.position = Vector3.Lerp(startPos, targetPos, timer / cameraMoveDuration);

            yield return null;
        }
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
