using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class CutsceneHome : MonoBehaviour
{
    public GameObject player;

    IEnumerator Start()
    {
        player.SetActive(false);

        yield return StartCoroutine(Fade.instance.FadeIn(1f));
        yield return new WaitForSeconds(1f);

        player.SetActive(true);

        yield return new WaitForSeconds(2f);
        yield return AutoDialogue.instance.ShowDialogue(
            "Akhirnya sampai rumah...",
            3f
        );

        yield return AutoDialogue.instance.ShowDialogue(
            "Hari ini benar-benar melelahkan.",
            3f
        );

        yield return AutoDialogue.instance.ShowDialogue(
            "Aku hanya ingin beristirahat sekarang.",
            3f
        );

        yield return StartCoroutine(WalkAndFade());
    }

    IEnumerator WalkAndFade()
    {
        float targetX = -3f;

        StartCoroutine(Fade.instance.FadeOut(2f));

        while (player.transform.position.x > targetX)
        {
            player.transform.Translate(Vector2.left * 2f * Time.deltaTime);

            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        SceneManager.LoadScene("Balcony");
    }
}