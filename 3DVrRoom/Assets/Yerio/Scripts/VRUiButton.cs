using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class VRUiButton : Selectable, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public UnityEvent OnClick;

    AudioManager audioManager;

    protected override void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnPointerClick(eventData);
        DoStateTransition(SelectionState.Pressed, false);
        OnClick.Invoke();
    }

    public void ButtonClick()
    {
        DoStateTransition(SelectionState.Pressed, false);
        OnClick.Invoke();
        audioManager.PlaySound("WhiteboardButtonClick");
    }

    public void ButtonSelect()
    {
        DoStateTransition(SelectionState.Highlighted, false);
        audioManager.PlaySound("WhiteboardButtonHover"); 
    }
    public void ButtonDeselect()
    {
        DoStateTransition(SelectionState.Normal, false);
    }

    public void Test()
    {
        Debug.Log("Button Pressed");
    }
}
