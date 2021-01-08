using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class RayInteractor : MonoBehaviour
{
    [Header("Pickup")]
    [SerializeField] SteamVR_Action_Boolean pickupInput;
    [SerializeField] float pickupDistance = 5f;
    [Header("UI")]
    [SerializeField] SteamVR_Action_Boolean uiInput;
    [SerializeField] LayerMask uiLayer;
    [SerializeField] Color pressColor;
    [SerializeField] bool interactWithUi = false;
    [Space]
    [SerializeField] bool showLine = false;

    //UI
    Button SelectedButton;
    VRUiButton SelectedVRButton;
    Toggle selectedToggle;

    //Pickup
    LineRenderer lineRenderer;
    Throwable throwableObjectSelected;
    Hand hand;
    Outline outline;
    bool hasOutline = false;

    Color originalColor;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
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

                if (hand.hoveringInteractable)
                {
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

            if (pickupInput[hand.handType].stateDown && throwableObjectSelected && !hand.hoveringInteractable)
            {
                DisableOutline();
                DisableLineRenderer();
                audioManager.PlaySound("Pickup");

                GrabTypes bestGrabType = hand.GetBestGrabbingType();

                if (bestGrabType != GrabTypes.None)
                {
                    hand.AttachObject(throwableObjectSelected.gameObject, bestGrabType, throwableObjectSelected.attachmentFlags);
                }

                //Debug.Log(interactableOfHoveringObject + " " + handType);
            }
            #endregion

            #region UIInteractable
            if (interactWithUi)
            {
                RaycastHit hitUi;
                if (Physics.Raycast(HandPos(), hand.renderModelInstance.transform.forward, out hitUi, 100f, uiLayer))
                {
                    SetLineRenderer(hitUi.point);
                    SetLineRendererColor(originalColor);

                    if (SelectedVRButton != hitUi.transform.GetComponent<VRUiButton>())
                    {
                        if (SelectedVRButton)
                        {
                            SelectedVRButton.ButtonDeselect();
                        }
                        SelectedVRButton = hitUi.transform.GetComponent<VRUiButton>();

                        if (SelectedVRButton)
                            SelectedVRButton.ButtonSelect();
                    }

                    if (selectedToggle != hitUi.transform.GetComponent<Toggle>())
                    {
                        selectedToggle = hitUi.transform.GetComponent<Toggle>();
                    }

                    if (uiInput[hand.handType].stateDown)
                    {
                        //press input
                        SetLineRendererColor(pressColor);

                        if (SelectedVRButton)
                            SelectedVRButton.ButtonClick();

                        if (selectedToggle)
                            selectedToggle.isOn = !selectedToggle.isOn;
                    }

                    if (uiInput[hand.handType].stateUp) //just to reset Color
                        SetLineRendererColor(originalColor);

                }
                else
                {
                    //if (SelectedButton)
                    //    ResetSelectedButton();
                    if (SelectedVRButton)
                    {
                        SetLineRendererColor(originalColor);
                        SelectedVRButton.ButtonDeselect();
                    }                        
                }           
            }
            #endregion
        }
    }

    void ResetSelectedButton()
    {
        SelectedButton.animator.ResetTrigger(SelectedButton.animationTriggers.highlightedTrigger);
        SelectedButton.animator.SetTrigger(SelectedButton.animationTriggers.normalTrigger);
        SelectedButton = null;
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
        return hand.renderModelInstance.transform.position - hand.renderModelInstance.transform.forward * 0.08f;
    }
}
