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

    float blueValveRotProcent;
    float greenValveRotProcent;
    float redValveRotProcent;

    int maxPsiValue = 300;
    float blueHandleRotation;
    float greenHandleRotation;
    float redHandleRotation;
    private void Awake()
    {
        ValvesInteractable(true);
    }
    void ValveRotationsProcent()
    {
        //---blue---
        blueValveRotProcent = GetLinearMappingValue(blueValve) * 100;

        //---green---
        greenValveRotProcent = GetLinearMappingValue(greenValve) * 100;

        //---red---
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
        blueValveSet = bluePsiValue == correctPsiValueBlueValve;

        Debug.Log(bluePsiValue);

        //---green---
        float greenGaugePercentage = (greenHandleRotation - minHandleRot) / (maxHandleRot - minHandleRot) * 100;
        int greenPsiValue = Mathf.RoundToInt(maxPsiValue / 100 * greenGaugePercentage);
        greenValveSet = greenPsiValue == correctPsiValueGreenValve;

        //---red---
        float redGaugePercentage = (redHandleRotation - minHandleRot) / (maxHandleRot - minHandleRot) * 100;
        int redPsiValue = Mathf.RoundToInt(maxPsiValue / 100 * redGaugePercentage);
        redValveSet = redPsiValue == correctPsiValueRedValve;

        if (blueValveSet && greenValveSet && redValveSet) 
        { 
            OnValvesSet.Invoke();

            ValvesInteractable(false);
        }
            
    }

    public void ValvesInteractable(bool state)
    {
        blueValve.GetComponent<Interactable>().enabled = state;
        greenValve.GetComponent<Interactable>().enabled = state;
        redValve.GetComponent<Interactable>().enabled = state;
    }

    private void Update()
    {
        ValveRotationsProcent();
        MoveGaugeHandleOnProcent();
        CheckGaugePercentage();
    }
}
