using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class VRUiButton : Selectable, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public UnityEvent OnClick;

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
    }

    public void ButtonSelect()
    {
        DoStateTransition(SelectionState.Highlighted, false);
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
