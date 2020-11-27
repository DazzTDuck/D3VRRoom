using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Interactable))]
public class SimpleGrab : MonoBehaviour
{
    [SerializeField] SteamVR_Action_Boolean grabInput;

    private void HandHoverUpdate(Hand hand)
    {
        if (grabInput[hand.handType].stateDown)
        {
            hand.AttachObject(gameObject, GrabTypes.Grip);
            hand.HideGrabHint();
        }

        if (grabInput[hand.handType].stateUp)
        {
            hand.DetachObject(gameObject);
        }
    }
}
