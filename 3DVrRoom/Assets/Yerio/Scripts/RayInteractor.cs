using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class RayInteractor : MonoBehaviour
{
    [Header("Pickup")]
    public SteamVR_Action_Boolean pickupInput;
    public float pickupDistance = 5f;
    [Header("UI")]
    public SteamVR_Action_Boolean uiInput;
    public LayerMask uiLayer;
    public Color pressColor;
    public bool interactWithUi = false;
    [Space]
    public bool showLine = false;

    //UI
    Button SelectedButton;

    //Pickup
    LineRenderer lineRenderer;
    Throwable throwableObjectSelected;
    Hand hand;
    Outline outline;
    bool hasOutline = false;

    Color originalColor;

    private void Awake()
    {
        hand = GetComponent<Hand>();
        lineRenderer = GetComponent<LineRenderer>();
        originalColor = lineRenderer.startColor;
    }

    private void Update()
    {
        if (hand.AttachedObjects.Count == 0 && hand.renderModelInstance)
        {
            #region Pickup
            RaycastHit hit;     
            if (Physics.Raycast(HandPos(), hand.renderModelInstance.transform.forward, out hit, pickupDistance))
            {
                
                //show if hovering over object with raycast
                if (hit.transform.GetComponent<Throwable>())
                {
                    throwableObjectSelected = hit.transform.GetComponent<Throwable>();

                    DisableOutline();
                    EnableOutline(throwableObjectSelected.gameObject);
                    SetLineRendererColor(originalColor);
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
            #endregion         
        }

        #region UIInteractable
        if (interactWithUi)
        {
            RaycastHit hitUi;
            if (Physics.Raycast(HandPos(), hand.renderModelInstance.transform.forward, out hitUi, 100f, uiLayer))
            {
                SetLineRenderer(hitUi.point);

                if (SelectedButton != hitUi.transform.GetComponent<Button>())
                {
                    SelectedButton = hitUi.transform.GetComponent<Button>();
                    SelectedButton.animator.SetTrigger(SelectedButton.animationTriggers.highlightedTrigger);
                }

                if (uiInput[hand.handType].stateDown)
                {
                    //press input
                    SetLineRendererColor(pressColor);

                    SelectedButton.animator.ResetTrigger(SelectedButton.animationTriggers.highlightedTrigger);
                    SelectedButton.animator.SetTrigger(SelectedButton.animationTriggers.pressedTrigger);
                    SelectedButton.onClick.Invoke();
                }

                if (uiInput[hand.handType].stateUp) //just to reset Color
                    SetLineRendererColor(originalColor);

            }
            else
            {
                if (SelectedButton)
                {
                    SelectedButton.animator.ResetTrigger(SelectedButton.animationTriggers.highlightedTrigger);
                    SelectedButton.animator.SetTrigger(SelectedButton.animationTriggers.normalTrigger);
                    SelectedButton = null;
                }
            }
        }    
        #endregion
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

    void SetLineRendererColor(Color newColor)
    {
        lineRenderer.startColor = newColor;
        lineRenderer.endColor = newColor;
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
