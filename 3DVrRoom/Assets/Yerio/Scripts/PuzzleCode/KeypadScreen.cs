using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class KeypadScreen : MonoBehaviour
{
    public TMP_Text keypadSceenText;

    [Space]
    public UnityEvent onCorrectCode;
   
    string correctCode;
    Color textColor;

    private void Awake()
    {
        textColor = keypadSceenText.color;
        SetCorrectCode("12345");
    }

    public void AddNumber(string number)
    {
        if (keypadSceenText.text == "00000")
            SetText("");
        if (keypadSceenText.text == "Nice")
        {
            SetText("");
            SetTextColor(textColor);
        }
            

        if (keypadSceenText.text.Length < 5)
        {           
            keypadSceenText.text += number;
            //Debug.Log(keypadSceenText.text);
        }     
    }

    public void SetText(string text)
    {
        keypadSceenText.text = text;
        SetTextColor(textColor);
    }

    public void SetCorrectCode(string code)
    {
        correctCode = code;
    }

    public void ConfirmCode()
    {
        if(keypadSceenText.text == correctCode)
        {
            onCorrectCode.Invoke();
            SetTextColor(Color.green);
            //Debug.Log("Correct Code");
            return;
        }

        switch (keypadSceenText.text)
        {
            case "69420":
                SetTextColor(Color.yellow);
                keypadSceenText.text = "Nice";
                break;

            default:
                //wrong code
                //so play a sound or something
                SetText("Wrong");
                break;
        }

    }
    public void SetTextColor(Color color)
    {
        keypadSceenText.color = color;
    }
}
