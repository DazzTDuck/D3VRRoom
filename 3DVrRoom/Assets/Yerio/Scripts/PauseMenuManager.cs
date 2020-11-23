using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Transform rightHand;
    [SerializeField] Transform leftHand;
    [SerializeField] SteamVR_Action_Boolean rightInput;
    [SerializeField] SteamVR_Action_Boolean leftInput;
    [Space]
    [SerializeField] GameObject pauseMenu;
    [Space]
    [SerializeField] Vector3 menuOffset;

    GameObject instantiatedPauseMenu;
    bool menuActive;

    private void Update()
    {
        if (rightInput.stateDown && !menuActive)
        {
            //Instantiate menu on right hand       
            ActivateMenu(rightHand);
        }
        else if (rightInput.stateDown && menuActive)
        {
            CloseMenu();
        }

        if (leftInput.stateDown && !menuActive)
        {
            //Instantiate menu on left hand
            ActivateMenu(leftHand);
        }
        else if (leftInput.stateDown && menuActive)
        {
            CloseMenu();
        }
    }

    void ActivateMenu(Transform hand)
    {
        menuActive = true;
        instantiatedPauseMenu = Instantiate(pauseMenu, hand.transform.position + menuOffset, Quaternion.identity);
        instantiatedPauseMenu.transform.LookAt(player);
    }

    void CloseMenu()
    {
        menuActive = false;

        if (instantiatedPauseMenu)
        {
            Destroy(instantiatedPauseMenu);
        }
    }
}
