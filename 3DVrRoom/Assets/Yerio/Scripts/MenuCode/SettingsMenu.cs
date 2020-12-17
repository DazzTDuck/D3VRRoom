using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using UnityEngine.Assertions.Must;
using System.Linq;

public class SettingsMenu : MonoBehaviour
{
    [Header("Mixer")]
    public AudioMixer masterMixer;

    //[Header("Dropdowns")]
    //public TMP_Dropdown resolutionDropdown;
    //public TMP_Dropdown qualityDropdown;

    [Header("Volumes")]
    public VolumeSetting master;
    public VolumeSetting sfx;
    public VolumeSetting dialogue;

    readonly float maxVolume = 20;
    readonly float minVolume = -40;

    [Header("Toggles")]
    //public Toggle fullscreenToggle;
    //public Toggle motionBlurToggle;
    public Toggle bloomToggle;

    [Header("PostFX Profile")]
    public VolumeProfile postFx;

    //MotionBlur motionBlur;
    Bloom bloom;

    //Resolution[] resolutions;

    //List<Resolution> resolutionsList = new List<Resolution>();

    private void Awake()
    {
        LoadSettings();
    }

    void LoadSettings()
    {
       // if (resolutionDropdown)
           // GetResolutions();

        postFx.TryGet(out bloom);
        //postFx.TryGet(out motionBlur);

        UpdateVolume();

        //if (qualityDropdown)
        //{
        //    SetQuality(PlayerPrefs.GetInt("qualityIndex", 2));
        //    qualityDropdown.value = PlayerPrefs.GetInt("qualityIndex", 2); ;
        //    qualityDropdown.RefreshShownValue();
        //}

        //if (resolutionDropdown)
        //{
        //    SetResolution(PlayerPrefs.GetInt("resIndex", resolutions.Length));
        //    resolutionDropdown.value = PlayerPrefs.GetInt("resIndex", resolutions.Length);
        //    resolutionDropdown.RefreshShownValue();
        //}
        //if (fullscreenToggle)
        //{
        //    SetFullscreen(PlayerPrefs.GetInt("fullscreenBool", 1) != 0);
        //    fullscreenToggle.isOn = PlayerPrefs.GetInt("fullscreenBool", 1) != 0;
        //}
        if (bloomToggle)
        {
            SetBloom(PlayerPrefs.GetInt("bloomBool", 1) != 0);
            bloomToggle.isOn = PlayerPrefs.GetInt("bloomBool", 1) != 0;
        }
        //if (motionBlurToggle)
        //{
        //    SetMotionBlur(PlayerPrefs.GetInt("motionBlurBool", 1) != 0);
        //    motionBlurToggle.isOn = PlayerPrefs.GetInt("motionBlurBool", 1) != 0;
        //}
    }

    public void ResetAllSettings()
    {
        PlayerPrefs.DeleteKey("masterVolume");
        PlayerPrefs.DeleteKey("sfxVolume");
        PlayerPrefs.DeleteKey("dialogueVolume");
       // PlayerPrefs.DeleteKey("resIndex");
        //PlayerPrefs.DeleteKey("qualityIndex");
        //PlayerPrefs.DeleteKey("motionBlurBool");
        PlayerPrefs.DeleteKey("bloomBool");
       // PlayerPrefs.DeleteKey("fullscreenBool");

        LoadSettings();
    }

    //public void GetResolutions()
    //{
    //    //get and set the resolutions for the dropdown
    //    resolutions = Screen.resolutions;

    //    resolutionDropdown.ClearOptions();
    //    resolutionsList.Clear();

    //    List<string> options = new List<string>();

    //    for (int i = 0; i < resolutions.Length; i++)
    //    {
    //        string option = resolutions[i].width + "x" + resolutions[i].height;
    //        if (!options.Contains(option))
    //        {
    //            options.Add(option);
    //            resolutionsList.Add(resolutions[i]);
    //           // Debug.Log(option + " " + i);
    //        }
    //    }

    //    PlayerPrefs.SetInt("resIndex", resolutions.Length); //the last in the list
    //    PlayerPrefs.Save();
    //    resolutionDropdown.AddOptions(options);
    //    resolutionDropdown.value = PlayerPrefs.GetInt("resIndex");
    //    resolutionDropdown.RefreshShownValue();
    //}

    //public void SetResolution(int resIndex)
    //{
    //    Resolution res = resolutionsList[resIndex];
    //    Screen.SetResolution(res.width, res.height, Screen.fullScreen);
    //    //Screen.SetResolution(1920, 1080, Screen.fullScreen);

    //    Debug.Log(resIndex + " " + res.width + " " + res.height);

    //    PlayerPrefs.SetInt("resIndex", resIndex);
    //    PlayerPrefs.Save();
    //}

    ////set the quality of the game
    //public void SetQuality(int qualityIndex)
    //{
    //    QualitySettings.SetQualityLevel(qualityIndex);

    //    PlayerPrefs.SetInt("qualityIndex", qualityIndex);
    //    PlayerPrefs.Save();
    //}

    
    public void UpdateVolume()
    {
        PlayerPrefs.SetInt("masterVolume", master.volume);
        PlayerPrefs.Save();
        SetMasterVolume(PlayerPrefs.GetInt("masterVolume", master.GetDefaultVolume()));

        PlayerPrefs.SetInt("sfxVolume", sfx.volume);
        PlayerPrefs.Save();
        SetSFXVolume(PlayerPrefs.GetInt("sfxVolume", sfx.GetDefaultVolume()));

        PlayerPrefs.SetInt("dialogueVolume", dialogue.volume);
        PlayerPrefs.Save();
        SetDialogueVolume(PlayerPrefs.GetInt("dialogueVolume", dialogue.GetDefaultVolume()));
    }

    public void SetMasterVolume(int volumeAmt)
    {
        float volume = Mathf.Lerp(minVolume, maxVolume, (float)volumeAmt / 10f);

        masterMixer.SetFloat("MasterVolume", volume);
    }
    public void SetDialogueVolume(int volumeAmt)
    {
        float volume = Mathf.Lerp(minVolume, maxVolume, (float)volumeAmt / 10f);

        masterMixer.SetFloat("DialogueVolume", volume);
    }
    public void SetSFXVolume(int volumeAmt)
    {
        float volume = Mathf.Lerp(minVolume, maxVolume, (float)volumeAmt / 10f);

        masterMixer.SetFloat("SFXVolume", volume);
    }

    //set the fullscreen of the game
    //public void SetFullscreen(bool isFullscreen)
    //{
    //    Screen.fullScreen = isFullscreen;

    //    PlayerPrefs.SetInt("fullscreenBool", isFullscreen ? 1 : 0);
    //    PlayerPrefs.Save();
    //}
    public void SetBloom(bool isBloom)
    {
        bloom.active = isBloom;

        PlayerPrefs.SetInt("bloomBool", isBloom ? 1 : 0);
        PlayerPrefs.Save();
    }
    //public void SetMotionBlur(bool isMB)
    //{
    //    motionBlur.active = isMB;

    //    PlayerPrefs.SetInt("motionBlurBool", isMB ? 1 : 0);
    //    PlayerPrefs.Save();

    //}
}
