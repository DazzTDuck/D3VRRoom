using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuButtons : MonoBehaviour
{
    static PauseMenuManager pauseMenuManager;
    SettingsMenu settings;

    [SerializeField] Animator whiteBoardAnimator;

    private void Awake()
    {
        if(!pauseMenuManager)
        pauseMenuManager = FindObjectOfType<PauseMenuManager>();
        settings = FindObjectOfType<SettingsMenu>();
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
        whiteBoardAnimator.SetTrigger("Flip");
    }
    public void CloseSettings()
    {
        whiteBoardAnimator.SetTrigger("FlipBack");
    }
}
