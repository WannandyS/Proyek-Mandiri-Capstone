using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class CutsceneWorkDesk : MonoBehaviour
{
    void Start()
    {
        Debug.Log("Cutscene workdesk start");
        StartCoroutine(StartCutscene());
    }

    IEnumerator StartCutscene()
    {
        yield return new WaitForSeconds(1f);

        if (WorkProgress.stage == 0)
        {
            string[] dialogue =
            {
                "Akhirnya sudah sampai di meja kerjaku",
                "Bos bilang hari ini ada banyak dokumen yang perlu dikerjakan",
                "Baiklah, ayo mulai kerja"
            };

            DialogueManager.instance.StartDialogue(dialogue);
            FindFirstObjectByType<AudioManager>().PlayCharacterSound();

            yield return StartCoroutine(WaitDialogue());
            yield return StartCoroutine(Fade.instance.FadeOut(1.5f));

            SceneManager.LoadScene("MinigameEasy");
        } else if (WorkProgress.stage == 1)
        {
            string[] dialogue =
            {
                "Dokumennya banyak juga",
                "Masih bisa kuatasi",
                "Tapi dokumennya masih tetap banyak",
                "Aku harus menyelesaikan lebih banyak lagi"
            };

            DialogueManager.instance.StartDialogue(dialogue);
            FindFirstObjectByType<AudioManager>().PlayCharacterSound();

            yield return StartCoroutine(WaitDialogue());
            yield return StartCoroutine(Fade.instance.FadeOut(1.5f));

            SceneManager.LoadScene("MinigameHard");
        } else if (WorkProgress.stage == 2)
        {
            string[] dialogue =
            {
                "Sudah jam pulang...",
                "Tapi dokumennya masih menumpuk",
                "Setiap kali satu selesai, muncul yang baru",
                "Aku benar-benar lelah hari ini",
                "Aku hanya ingin pulang"
            };
            DialogueManager.instance.StartDialogue(dialogue);
            FindFirstObjectByType<AudioManager>().PlayCharacterSound();

            yield return StartCoroutine(WaitDialogue());
            yield return StartCoroutine(Fade.instance.FadeOut(1.5f));

            SceneManager.LoadScene("AfterOffice");
        }

        IEnumerator WaitDialogue()
        {
            while (DialogueManager.instance.IsDialogueActive())
            {
                yield return null;
            }
        }
    }
}
