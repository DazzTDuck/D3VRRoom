using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeypadScreen : MonoBehaviour
{
    public TMP_Text keypadSceenText;

    public void AddNumber(string number)
    {
        if (keypadSceenText.text == "00000")
            ClearText();

        if (keypadSceenText.text.Length < 5)
        {           
            keypadSceenText.text += number;
            Debug.Log(keypadSceenText.text);
        }     
    }

    public void ClearText()
    {
        keypadSceenText.text = "";
    }
}
