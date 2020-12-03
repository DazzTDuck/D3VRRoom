using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Timer")]
    [SerializeField] TMP_Text[] timerText;
    [SerializeField] float minutesToFinishGame;
    bool startTimer = false;
    float timerSeconds;

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

    [Header("Dumbell Puzzle")]
    [SerializeField] GameObject[] dumbells;
    [SerializeField] GameObject[] hidingPlacesDumbell;
    [SerializeField] GameObject[] dumbellsInDrawer;

    int amountDumbellsToHide = 3;
    int amountChosen;
    List<GameObject> dumbellsChosen = new List<GameObject>();

    [Header("Hidden Key Office")]
    [SerializeField] GameObject[] keyHidingPlaces;
    

    public void StartGame()
    {
        SetDumbellNumbers();
        HideDumbells();
    }

    private void Start()
    {
        StartTimer();
    }

    private void Update()
    {
        UpdateTimer();
    }

    public void StartTimer()
    {
        timerSeconds = minutesToFinishGame * 60;
        startTimer = true;
    }

    public void UpdateTimer()
    {
        if (startTimer)
        {
            timerSeconds -= Time.deltaTime;

            int hours = (int)(timerSeconds / 3600) % 24;
            int minutes = (int)(timerSeconds / 60) % 60;
            int seconds = (int)(timerSeconds % 60);

            bool lower10Seconds = seconds < 10;

            foreach (var text in timerText)
            {
                if (lower10Seconds)
                    text.text = $"0{hours}:{minutes}:0{seconds}";
                else
                    text.text = $"0{hours}:{minutes}:{seconds}";
            }

            if(timerSeconds <= 0)
            {
                //end game
            }
        }
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

    public void HideDumbells()
    {
        foreach (var dumbell in dumbells)
        {
            if(amountChosen != amountDumbellsToHide)
            {
                var index = Random.Range(0, dumbells.Length - 1);

                if (!dumbellsChosen.Contains(dumbells[index]))
                {
                    dumbellsChosen.Add(dumbells[index]);
                }
                else return;

                amountChosen++;
            }
        }



    }

    public int GetCode()
    {
        return Random.Range(minCodeRange, maxCodeRange);
    }
}
