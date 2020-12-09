using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButtons : MonoBehaviour
{
    [SerializeField] Animator whiteBoardAnimator;
    [SerializeField] Canvas mainMenuCanvas;
    [SerializeField] Canvas settingsCanvas;
    [SerializeField] GameObject normalWhiteBoard;
    [SerializeField] GameObject menuWhiteBoard;

    float timeToWait = 0.20f;

    public void StartGame()
    {
        //call StartGame fuction
        StartCoroutine(WhiteBoardReplace());
    }
    public void OpenSettings()
    {
        whiteBoardAnimator.SetTrigger("Flip");
        StartCoroutine(EnableSettingsCanvas());
    }
    public void CloseSettings()
    {
        whiteBoardAnimator.SetTrigger("FlipBack");
        StartCoroutine(DisableSettingsCanvas());
    }
    public void Quit()
    {
        Application.Quit();
    }

    IEnumerator EnableSettingsCanvas()
    {
        yield return new WaitForSeconds(timeToWait);
        settingsCanvas.enabled = true;
        mainMenuCanvas.enabled = false;
        StopCoroutine(EnableSettingsCanvas());
    }
    IEnumerator DisableSettingsCanvas()
    {
        yield return new WaitForSeconds(timeToWait);
        settingsCanvas.enabled = false;
        mainMenuCanvas.enabled = true;
        StopCoroutine(DisableSettingsCanvas());
    }

    IEnumerator WhiteBoardReplace()
    {
        whiteBoardAnimator.SetTrigger("Flip");
        yield return new WaitForSeconds(timeToWait + 0.2f);
        normalWhiteBoard.SetActive(true);
        menuWhiteBoard.SetActive(false);
        StopCoroutine(WhiteBoardReplace());
    }
}
