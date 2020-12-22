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

    private void Start()
    {
        LoadSettings();
    }

    public void LoadSettings()
    {
        postFx.TryGet(out bloom);
        //postFx.TryGet(out motionBlur);

        UpdateVolume();

        if (bloomToggle)
        {
            SetBloom(PlayerPrefs.GetInt("bloomBool", 1) != 0);
            bloomToggle.isOn = PlayerPrefs.GetInt("bloomBool", 1) != 0;
        }
    }

    public void ResetAllSettings()
    {
        PlayerPrefs.DeleteKey("masterVolume");
        PlayerPrefs.DeleteKey("sfxVolume");
        PlayerPrefs.DeleteKey("dialogueVolume");
        PlayerPrefs.DeleteKey("bloomBool");

        LoadSettings();
    }
    public void UpdateVolume()
    {
        SetMasterVolume(PlayerPrefs.GetInt("masterVolume", master.GetDefaultVolume()));

        SetSFXVolume(PlayerPrefs.GetInt("sfxVolume", sfx.GetDefaultVolume()));

        SetDialogueVolume(PlayerPrefs.GetInt("dialogueVolume", dialogue.GetDefaultVolume()));
    }

    public void SetMasterVolume(int volumeAmt)
    {
        float volume = Mathf.Lerp(minVolume, maxVolume, (float)volumeAmt / 10f);

        masterMixer.SetFloat("MasterVolume", volume);

        PlayerPrefs.SetInt("masterVolume", master.volume);
        PlayerPrefs.Save();
    }
    public void SetDialogueVolume(int volumeAmt)
    {
        float volume = Mathf.Lerp(minVolume, maxVolume, (float)volumeAmt / 10f);

        masterMixer.SetFloat("DialogueVolume", volume);;

        PlayerPrefs.SetInt("dialogueVolume", dialogue.volume);
        PlayerPrefs.Save();
    }
    public void SetSFXVolume(int volumeAmt)
    {
        float volume = Mathf.Lerp(minVolume, maxVolume, (float)volumeAmt / 10f);

        masterMixer.SetFloat("SFXVolume", volume);

        PlayerPrefs.SetInt("sfxVolume", sfx.volume);
        PlayerPrefs.Save();
    }

    public void SetBloom(bool isBloom)
    {
        bloom.active = isBloom;

        PlayerPrefs.SetInt("bloomBool", isBloom ? 1 : 0);
        PlayerPrefs.Save();
    }
}
