using UnityEngine;

public class CutscenePlayer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();

        animator.SetInteger("State", WorkProgress.stage);
    }
}
