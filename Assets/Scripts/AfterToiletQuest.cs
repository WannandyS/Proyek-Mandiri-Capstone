using UnityEngine;
using System.Collections;

public class AfterToiletQuest : MonoBehaviour
{
    IEnumerator Start()
    {
        yield return StartCoroutine(Fade.instance.FadeIn(1f));

        yield return new WaitForSeconds(0.5f);

        string[] dialogue =
        {
            "Sudah lebih segar sekarang.",
            "Sudah pakai jas juga.",
            "Saatnya berangkat."
        };

        DialogueManager.instance.StartDialogue(dialogue);

        Objective.instance.SetObjective("Keluar rumah");
    }
}
