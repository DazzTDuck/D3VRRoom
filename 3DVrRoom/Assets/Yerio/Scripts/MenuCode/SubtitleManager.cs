using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class SubtitleManager : MonoBehaviour
{
    [Header("--Canvas--")]
    public GameObject subtilePanel;
    public TMP_Text subtitleText;
    public TMP_Text nameText;

    SettingsMenu settingsMenu;

    private void Awake()
    {
        settingsMenu = FindObjectOfType<SettingsMenu>();
    }

    public void SetupSubtitle(string text, string name, float time)
    {
        subtilePanel.SetActive(true);
        nameText.text = name;
        subtitleText.text = text;

        StartCoroutine(CloseSubtitles(time));
    }

    public IEnumerator CloseSubtitles(float time)
    {
        yield return new WaitForSeconds(time);
        subtilePanel.SetActive(false);
    }

    public IEnumerator SequenceWakeUp()
    {
        yield return new WaitForSeconds(2f);
    }
}
