using UnityEngine;
using System.Collections;

public class Office : MonoBehaviour
{
    IEnumerator Start()
    {
        yield return Fade.instance.StartCoroutine(Fade.instance.FadeIn(1f));
        yield return new WaitForSeconds(0.3f);
    }
}
