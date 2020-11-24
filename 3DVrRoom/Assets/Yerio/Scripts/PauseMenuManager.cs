﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] Transform vrCamera;
    [SerializeField] Transform rightHand;
    [SerializeField] Transform leftHand;
    [SerializeField] SteamVR_Action_Boolean rightInput;
    [SerializeField] SteamVR_Action_Boolean leftInput;
    [Space]
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject teleportObject;

    GameObject instantiatedPauseMenu;
    [HideInInspector]
    public bool paused;

    private void Update()
    {
        if (rightInput.stateDown && !paused)
        {
            //Instantiate menu on right hand       
            ActivateMenu(rightHand);
        }
        else if (rightInput.stateDown && paused)
        {
            CloseMenu();
        }

        if (leftInput.stateDown && !paused)
        {
            //Instantiate menu on left hand
            ActivateMenu(leftHand);
        }
        else if (leftInput.stateDown && paused)
        {
            CloseMenu();
        }
    }

    void ActivateMenu(Transform hand)
    {
        teleportObject.SetActive(false);
        paused = true;
        instantiatedPauseMenu = Instantiate(pauseMenu, hand.transform.position + vrCamera.forward, Quaternion.identity);
        var rotation = Quaternion.LookRotation(vrCamera.forward, Vector3.up);
        instantiatedPauseMenu.transform.rotation = rotation;
    }

    void CloseMenu()
    {
        teleportObject.SetActive(true);
        paused = false;

        if (instantiatedPauseMenu)
        {
            Destroy(instantiatedPauseMenu);
        }
    }
}
