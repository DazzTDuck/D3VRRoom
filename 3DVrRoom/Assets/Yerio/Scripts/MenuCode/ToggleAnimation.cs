using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleAnimation : MonoBehaviour
{
    [SerializeField] Toggle toggle;
    [SerializeField] Animator checkmarkAnimator;

    public void Update()
    {
        if(toggle.isOn) { checkmarkAnimator.ResetTrigger("Off"); checkmarkAnimator.SetTrigger("On"); } 
        else { checkmarkAnimator.ResetTrigger("On"); checkmarkAnimator.SetTrigger("Off"); }
    }
}
