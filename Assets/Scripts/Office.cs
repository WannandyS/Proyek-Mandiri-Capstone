using UnityEngine;
using System.Collections;

public class Office : MonoBehaviour
{
    IEnumerator Start()
    {
        yield return Fade.instance.StartCoroutine(Fade.instance.FadeIn(1f));
        yield return new WaitForSeconds(1f);

        Objective.instance.SetObjective("Mencari Bos");
        Objective.instance.ShowObjective();
    }
}
