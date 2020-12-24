using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackScreen : MonoBehaviour
{
    static Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public static void FadeIn()
    {
        animator.SetTrigger("FadeIn");
    }
    public static void FadeOut()
    {
        animator.SetTrigger("FadeOut");
    }
}
