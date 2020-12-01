using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;


public class GravitySwitchOnPickup : MonoBehaviour
{
    [SerializeField] Rigidbody rb;

    private void OnAttachedToHand(Hand hand)
    {
        Debug.Log("gravity Enabled");
        rb.useGravity = true;
        rb.isKinematic = false;
    }
}
