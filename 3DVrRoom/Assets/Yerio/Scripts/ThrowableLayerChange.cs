using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class ThrowableLayerChange : MonoBehaviour
{
    private void OnAttachedToHand(Hand hand)
    {
        gameObject.layer = 9;
        foreach (var transform in GetComponentsInChildren<Transform>())
        {
            transform.gameObject.layer = 9;
        }
    }

    private void OnDetachedFromHand(Hand hand)
    {
        gameObject.layer = 0;
        foreach (var transform in GetComponentsInChildren<Transform>())
        {
            transform.gameObject.layer = 0;
        }
    }
}
