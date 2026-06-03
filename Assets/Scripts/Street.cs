using UnityEngine;
using System.Collections;

public class Street : MonoBehaviour
{
    IEnumerator Start()
    {
        yield return Fade.instance.StartCoroutine(Fade.instance.FadeIn(1f));
        yield return new WaitForSeconds(0.1f);

        Tutorial.instance.ShowTutorial("Pergi ke kantor");
    }
}
