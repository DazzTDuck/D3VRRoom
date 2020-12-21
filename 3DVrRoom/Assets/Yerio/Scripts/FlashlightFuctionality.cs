using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class FlashlightFuctionality : MonoBehaviour
{
    [SerializeField] SteamVR_Action_Boolean switchInput;
    [SerializeField] Light[] lights;
    [SerializeField] Animator buttonAnimator;
    [SerializeField] SteamVR_Skeleton_Poser skeletonPoser;

    [SerializeField] bool hasUVAttachment = false;

    int lightsIndex = 2;
    Hand holdingHand;
    Hand[] hands;

    private void Update()
    {
        if (holdingHand)
        {
            if (switchInput[holdingHand.handType].stateDown)
            { 
                buttonAnimator.SetTrigger("ButtonPress");
                lightsIndex++;

                if (lightsIndex > lights.Length)
                {
                    lightsIndex = 0;
                }

                switch (lightsIndex)
                {
                    case 0:
                        //Normal Light
                        lights[lightsIndex].enabled = true;
                        break;
                    case 1:
                        //UV light
                        if (hasUVAttachment)
                        {
                            lights[lightsIndex - 1].enabled = false;
                            lights[lightsIndex].enabled = true;
                        }
                        else
                            lightsIndex = 2;

                        break;
                    case 2:
                        //nothing
                        lights[lightsIndex - 1].enabled = false;
                        break;
                }

            }
        }      
    }

    public void UVAttachment() { hasUVAttachment = true; }

    private void OnAttachedToHand(Hand hand)
    {
        GetHand();
        //Debug.Log("Picked Up Flashlight");
    }

    private void OnDetachedFromHand(Hand hand)
    {
        holdingHand = null;
        //Debug.Log("Dropped Flashlight");
    }

    void GetHand()
    {
        if (hands == null)
            hands = FindObjectsOfType<Hand>();

        foreach (var hand in hands)
        {
            if (hand.ObjectIsAttached(gameObject))
            {
                holdingHand = hand;
            }
        }
    }
}
