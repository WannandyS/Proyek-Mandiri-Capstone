using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class CutsceneHome : MonoBehaviour
{
    public GameObject player;
    private Animator animator;

    IEnumerator Start()
    {
        animator = player.GetComponent<Animator>();
        player.SetActive(false);

        yield return StartCoroutine(Fade.instance.FadeIn(1f));
        yield return new WaitForSeconds(1f);

        player.SetActive(true);
        FindAnyObjectByType<AudioManager>().PlayDoorSound();
        player.transform.localScale = new Vector3(-1f, 1f, 1f);

        yield return new WaitForSeconds(1.5f);
        FindFirstObjectByType<AudioManager>().PlayCharacterSound();
        yield return AutoDialogue.instance.ShowDialogue("Akhirnya sampai rumah...", 3f);
        
        FindFirstObjectByType<AudioManager>().PlayCharacterSound();
        yield return AutoDialogue.instance.ShowDialogue("Hari ini benar-benar melelahkan", 3f);

        FindFirstObjectByType<AudioManager>().PlayCharacterSound();
        yield return AutoDialogue.instance.ShowDialogue("Aku hanya ingin beristirahat sekarang", 3f);

        yield return StartCoroutine(WalkAndFade());
    }

    IEnumerator WalkAndFade()
    {
        float targetX = 1f;
        float fadeTrigger = 6.5f;
        bool fadeStart = false;
        Player playerScript = player.GetComponent<Player>();

        if(playerScript != null )
        {
            playerScript.enabled = false;
        }

        animator.SetFloat("Walk", 1);

        while (player.transform.position.x >= targetX)
        {
            player.transform.Translate(Vector2.left * 2f * Time.deltaTime);

            if (!fadeStart && player.transform.position.x <= fadeTrigger)
            {
                fadeStart = true;
                StartCoroutine(Fade.instance.FadeOut(2f));
            }

            yield return null;
        }

        animator.SetFloat("Walk", 0);

        SceneManager.LoadScene("Ending");
    }
}