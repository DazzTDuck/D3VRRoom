using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class MainMenuButtons : MonoBehaviour
{
    [SerializeField] Animator whiteBoardAnimator;
    [SerializeField] Canvas mainMenuCanvas;
    [SerializeField] Canvas settingsCanvas;
    [SerializeField] GameObject normalWhiteBoard;
    [SerializeField] GameObject menuWhiteBoard;
    [Space]
    [SerializeField] GameObject player;
    [SerializeField] Transform startPointPlayer;
    [Space]

    AudioManager audioManager;
    BossSE1 bossSE1;

    float timeToWait = 0.20f;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
        bossSE1 = FindObjectOfType<BossSE1>();
    }

    public void StartGame()
    {
        //call StartGame fuction
        StartCoroutine(SetupBeginScene());
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

    IEnumerator SetupBeginScene()
    {
        whiteBoardAnimator.SetTrigger("Flip");
        BlackScreen.FadeIn();
        yield return new WaitForSeconds(timeToWait + 0.4f);

        normalWhiteBoard.SetActive(true);
        menuWhiteBoard.SetActive(false);

        yield return new WaitForSeconds(0.5f);

        player.transform.position = startPointPlayer.position;

        yield return new WaitForSeconds(0.2f);

        BlackScreen.FadeOut();

        yield return new WaitForSeconds(2f);

        //call starting dialoge
        audioManager.PlaySound("SE_1.1");
        bossSE1.StartBeginningSequence();

        StopCoroutine(SetupBeginScene());
    }
}
