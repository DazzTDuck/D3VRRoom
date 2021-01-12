using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class ValvePuzzle : MonoBehaviour
{
    [Header("Valves")]
    [SerializeField] Transform blueValve;
    [SerializeField] Transform greenValve;
    [SerializeField] Transform redValve;
    [Header("Gauges")]
    [SerializeField] Transform blueGaugeHandle;
    [SerializeField] Transform greenGaugeHandle;
    [SerializeField] Transform redGaugeHandle;
    [Header("Values")]
    [SerializeField] float minHandleRot;
    [SerializeField] float maxHandleRot;
    [Space]
    [SerializeField] int correctPsiValueBlueValve;
    [SerializeField] int correctPsiValueGreenValve;
    [SerializeField] int correctPsiValueRedValve;
    [Space]
    public UnityEvent OnValvesSet;

    //privates
    bool blueValveSet = false;
    bool greenValveSet = false;
    bool redValveSet = false;

    bool puzzleCompleted = false;

    float blueValveRotProcent;
    float greenValveRotProcent;
    float redValveRotProcent;

    int maxPsiValue = 300;
    float blueHandleRotation;
    float greenHandleRotation;
    float redHandleRotation;

    CircularDrive red;
    CircularDrive green;
    CircularDrive blue;
    Interactable iRed;
    Interactable iGreen;
    Interactable iBlue;

    private void Awake()
    {
        blue = blueValve.GetComponent<CircularDrive>();
        green = greenValve.GetComponent<CircularDrive>();
        red = redValve.GetComponent<CircularDrive>();

        iBlue = blueValve.GetComponent<Interactable>();
        iGreen = greenValve.GetComponent<Interactable>();
        iRed = redValve.GetComponent<Interactable>();

        BlueValveInteractable(true);
        GreenValveInteractable(false);
        RedValveInteractable(false);
    }
    void ValveRotationsProcent()
    {
        //---blue---
        if(blue.isActiveAndEnabled)
            blueValveRotProcent = GetLinearMappingValue(blueValve) * 100;

        //---green---
        if (green.isActiveAndEnabled)
            greenValveRotProcent = GetLinearMappingValue(greenValve) * 100;

        //---red---
        if (red.isActiveAndEnabled)
            redValveRotProcent = GetLinearMappingValue(redValve) * 100;
    }

    float GetLinearMappingValue(Transform transform)
    {
        return transform.GetComponent<LinearMapping>().value;
    }

    void MoveGaugeHandleOnProcent()
    {
        //---blue---
        blueHandleRotation = (maxHandleRot / 100) * blueValveRotProcent;
        blueHandleRotation = Mathf.Clamp(blueHandleRotation, minHandleRot, maxHandleRot);

        Quaternion zRotBlueHandle = Quaternion.Euler(0, 0, blueHandleRotation);
        blueGaugeHandle.rotation = zRotBlueHandle;

        //Debug.Log(zRotBlueHandle);

        //---green---
        greenHandleRotation = (maxHandleRot / 100) * greenValveRotProcent;
        greenHandleRotation = Mathf.Clamp(greenHandleRotation, minHandleRot, maxHandleRot);

        Quaternion zRotGreenHandle = Quaternion.Euler(0, 0, greenHandleRotation);
        greenGaugeHandle.rotation = zRotGreenHandle;

        //---red---
        redHandleRotation = (maxHandleRot / 100) * redValveRotProcent;
        redHandleRotation = Mathf.Clamp(redHandleRotation, minHandleRot, maxHandleRot);

        Quaternion zRotRedHandle = Quaternion.Euler(0, 0, redHandleRotation);
        redGaugeHandle.rotation = zRotRedHandle;

        //Debug.Log(blueHandleRotation);
    }

    void CheckGaugePercentage()
    {
        //---blue---
        float blueGaugePercentage = (blueHandleRotation - minHandleRot) / (maxHandleRot - minHandleRot) * 100;
        //because the begin value is not 0 the procentage of the raw difference is 1%, so if i make it so the minRot gets
        //subtracted from the rotation and maxValue it acts like if the begin value is 0 so it begins on 0% too.

        int bluePsiValue = Mathf.RoundToInt(maxPsiValue / 100 * blueGaugePercentage);
        int bluePsiValueRoundedToTen = Mathf.RoundToInt((float)bluePsiValue / 10f) * 10;
        blueValveSet = bluePsiValueRoundedToTen == correctPsiValueBlueValve;

        //Debug.Log(bluePsiValueRoundedToTen);

        //---green---
        float greenGaugePercentage = (greenHandleRotation - minHandleRot) / (maxHandleRot - minHandleRot) * 100;
        int greenPsiValue = Mathf.RoundToInt(maxPsiValue / 100 * greenGaugePercentage);
        int greenPsiValueRoundedToTen = Mathf.RoundToInt((float)greenPsiValue / 10f) * 10;
        greenValveSet = greenPsiValueRoundedToTen == correctPsiValueGreenValve;

        //---red---
        float redGaugePercentage = (redHandleRotation - minHandleRot) / (maxHandleRot - minHandleRot) * 100;
        int redPsiValue = Mathf.RoundToInt(maxPsiValue / 100 * redGaugePercentage);
        int redPsiValueRoundedToTen = Mathf.RoundToInt((float)redPsiValue / 10f) * 10;
        redValveSet = redPsiValueRoundedToTen == correctPsiValueRedValve;

        if (blueValveSet && greenValveSet && redValveSet && !puzzleCompleted) 
        { 
            OnValvesSet.Invoke();
            AllValvesInteractable(false);
            puzzleCompleted = true;
        }      
    }

    public void SetNewCodes(int firstCode, int secondCode, int thirdCode)
    {
        correctPsiValueBlueValve = firstCode;
        correctPsiValueGreenValve = secondCode;
        correctPsiValueRedValve = thirdCode;
    }

    public void AllValvesInteractable(bool state)
    {
        blue.enabled = state;
        green.enabled = state;
        red.enabled = state;

        iRed.enabled = state;
        iGreen.enabled = state;
        iBlue.enabled = state;
    }
    public void BlueValveInteractable(bool state)
    {
        blue.enabled = state;
        iBlue.enabled = state;
    }
    public void GreenValveInteractable(bool state)
    {
        green.enabled = state;
        iGreen.enabled = state;
    }
    public void RedValveInteractable(bool state)
    {
        red.enabled = state;
        iRed.enabled = state;
    }  

    private void Update()
    {
        ValveRotationsProcent();
        MoveGaugeHandleOnProcent();
        CheckGaugePercentage();
    }
}
