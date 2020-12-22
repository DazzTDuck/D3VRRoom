using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] Transform vrCamera;
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
        if (rightInput.stateDown && !paused || leftInput.stateDown && !paused)
        {
            //Instantiate menu on right hand       
            ActivateMenu();
        }
        else if (rightInput.stateDown && paused || leftInput.stateDown && paused)
        {
            CloseMenu();
        }
    }

    void ActivateMenu()
    {
        teleportObject.SetActive(false);
        paused = true;
        var rotation = Quaternion.LookRotation(vrCamera.forward, Vector3.up);
        instantiatedPauseMenu = Instantiate(pauseMenu, vrCamera.position + vrCamera.forward, rotation);
    }

    public void CloseMenu()
    {
        teleportObject.SetActive(true);
        paused = false;

        if (instantiatedPauseMenu)
        {
            Destroy(instantiatedPauseMenu);
        }
    }
}
