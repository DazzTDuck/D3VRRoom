using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuButtons : MonoBehaviour
{
    static PauseMenuManager pauseMenuManager;

    private void Awake()
    {
        if(!pauseMenuManager)
        pauseMenuManager = FindObjectOfType<PauseMenuManager>();
    }

    public void ResumeGame()
    {
        pauseMenuManager.CloseMenu();
    }

    public void MainMenu()
    {
        //call resetting game function
    }

    public void OpenSettings()
    {

    }
    public void CloseSettings()
    {

    }
}
