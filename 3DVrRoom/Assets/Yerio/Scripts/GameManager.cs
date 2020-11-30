using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Code Randomizer")]
    [SerializeField] int minCodeRange;
    [SerializeField] int maxCodeRange;

    [Header("Dumbell Code")]
    [SerializeField] TMP_Text number1;
    [SerializeField] TMP_Text number2;
    [SerializeField] TMP_Text number3;
    [SerializeField] TMP_Text number4;
    [SerializeField] TMP_Text number5;
    [SerializeField] Keypad officeVaultKeypad;
    bool codeSet = false;

    private void Update()
    {
        SetDumbellNumbers();
    }

    public void SetDumbellNumbers()
    {
        if (!codeSet)
        {
            codeSet = true;

            var code = GetCode().ToString();
            number1.text = code.Substring(0, 1);
            number2.text = code.Substring(1, 1);
            number3.text = code.Substring(2, 1);
            number4.text = code.Substring(3, 1);
            number5.text = code.Substring(4, 1);

            officeVaultKeypad.SetCorrectCode(code);
        }      
    }

    public int GetCode()
    {
        return Random.Range(minCodeRange, maxCodeRange);
    }
}
