using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class DistancePickup : MonoBehaviour
{
    public SteamVR_Action_Boolean pickupInput;
    public float pickupDistance = 5f;
    [Space]
    public bool showLine = false;

    LineRenderer lineRenderer;
    Throwable throwableObjectSelected;
    Hand hand;
 
    Outline outline;
    bool hasOutline = false;

    private void Awake()
    {
        hand = GetComponent<Hand>();
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (hand.AttachedObjects.Count == 0 && hand.renderModelInstance)
        {
            RaycastHit hit;     
            if (Physics.Raycast(HandPos(), hand.renderModelInstance.transform.forward, out hit, pickupDistance))
            {
                //show if hovering over object with raycast
                if (hit.transform.GetComponent<Throwable>())
                {
                    throwableObjectSelected = hit.transform.GetComponent<Throwable>();

                    DisableOutline();
                    EnableOutline(throwableObjectSelected.gameObject);
                    SetLineRenderer(hit.point);

                    //Debug.Log(interactableOfGrabbedObject + " " + handType);  
                }
                else
                {
                    throwableObjectSelected = null;
                    DisableOutline();
                    DisableLineRenderer();
                }
            }
            else
            {
                throwableObjectSelected = null;
                DisableOutline();
                DisableLineRenderer();
            }

            if (pickupInput[hand.handType].stateDown && throwableObjectSelected)
            {
                GrabTypes bestGrabType = hand.GetBestGrabbingType();

                if (bestGrabType != GrabTypes.None)
                {
                    hand.AttachObject(throwableObjectSelected.gameObject, bestGrabType, throwableObjectSelected.attachmentFlags);
                }

                DisableOutline();
                DisableLineRenderer();

                //Debug.Log(interactableOfHoveringObject + " " + handType);
            }
        }
    }

    void EnableOutline(GameObject gameObject)
    {
        if (!hasOutline)
        {
            outline = gameObject.GetComponent<Outline>();
            outline.enabled = true;
            hasOutline = true;
            //Debug.Log(outline);
        }
    }
    void DisableOutline()
    {
        if (outline && hasOutline)
        {
            outline.enabled = false;
            hasOutline = false;
        }
    }

    void SetLineRenderer(Vector3 posisiton)
    {
        if (showLine)
        {
            lineRenderer.SetPosition(0, HandPos());
            lineRenderer.SetPosition(1, posisiton);
        }      
    }

    void DisableLineRenderer()
    {
        if (showLine)
        {
            lineRenderer.SetPosition(0, HandPos());
            lineRenderer.SetPosition(1, HandPos());
        }        
    }

    Vector3 HandPos()
    {
        return hand.renderModelInstance.transform.position;
    }
}
