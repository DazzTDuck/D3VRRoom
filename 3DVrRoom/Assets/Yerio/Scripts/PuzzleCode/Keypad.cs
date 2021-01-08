using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class Keypad : MonoBehaviour
{
    public TMP_Text keypadSceenText;

    [Space]
    public UnityEvent onCorrectCode;

    AudioManager audioManager;
    string correctCode;
    Color textColor;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
        textColor = keypadSceenText.color;
        SetCorrectCode("12345");
    }

    public void AddNumber(string number)
    {
        audioManager.PlaySound("KeyBeep");

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
            audioManager.PlaySound("KeyAccept");
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
                audioManager.PlaySound("KeyDeny");
                SetText("Wrong");
                break;
        }

    }
    public void SetTextColor(Color color)
    {
        keypadSceenText.color = color;
    }
}
