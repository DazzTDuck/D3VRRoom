using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeypadScreen : MonoBehaviour
{
    public TMP_Text keypadSceenText;

    public void AddNumber(string number)
    {
        if(keypadSceenText.text.Length < 6)
        {
            if (keypadSceenText.text == "00000")
                ClearText();

            keypadSceenText.text += number;
            Debug.Log(keypadSceenText.text);
        }     
    }

    public void ClearText()
    {
        keypadSceenText.text = "";
    }
}
