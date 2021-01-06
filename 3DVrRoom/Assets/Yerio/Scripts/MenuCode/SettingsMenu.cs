using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

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
    public Toggle subtitlesToggle;

    [Header("PostFX Profile")]
    public VolumeProfile postFx;

    //MotionBlur motionBlur;
    Bloom bloom;

    //Resolution[] resolutions;

    //List<Resolution> resolutionsList = new List<Resolution>();

    private void Awake()
    {
        //ResetAllSettings();
        postFx.TryGet(out bloom);
    }
    private void Start()
    {
        LoadSettings();
    }

    public void LoadSettings()
    {
        SetMasterVolume(PlayerPrefs.GetInt("masterVolume", master.GetDefaultVolume()));
        master.SetVolume(PlayerPrefs.GetInt("masterVolume", master.GetDefaultVolume()));

        SetSFXVolume(PlayerPrefs.GetInt("sfxVolume", sfx.GetDefaultVolume()));
        sfx.SetVolume(PlayerPrefs.GetInt("sfxVolume", sfx.GetDefaultVolume()));

        SetDialogueVolume(PlayerPrefs.GetInt("dialogueVolume", dialogue.GetDefaultVolume()));
        dialogue.SetVolume(PlayerPrefs.GetInt("dialogueVolume", dialogue.GetDefaultVolume()));

        SetBloom(PlayerPrefs.GetInt("bloomBool", 1) != 0);
        bloomToggle.isOn = PlayerPrefs.GetInt("bloomBool", 1) != 0;

        SetSubtitle(PlayerPrefs.GetInt("subtitleBool", 1) != 0);
        subtitlesToggle.isOn = PlayerPrefs.GetInt("subtitleBool", 1) != 0;
    }

    public void ResetAllSettings()
    {
        PlayerPrefs.DeleteKey("masterVolume");
        PlayerPrefs.DeleteKey("sfxVolume");
        PlayerPrefs.DeleteKey("dialogueVolume");
        PlayerPrefs.DeleteKey("bloomBool");
        PlayerPrefs.DeleteKey("subtitleBool");

        LoadSettings();
    }

    public void SetMasterVolume(int volumeAmt)
    {
        float volume = Mathf.Lerp(minVolume, maxVolume, (float)volumeAmt / 10f);

        masterMixer.SetFloat("MasterVolume", volume);

        PlayerPrefs.SetInt("masterVolume", volumeAmt);
        PlayerPrefs.Save();
    }
    public void SetDialogueVolume(int volumeAmt)
    {
        float volume = Mathf.Lerp(minVolume, maxVolume, (float)volumeAmt / 10f);

        masterMixer.SetFloat("DialogueVolume", volume);

        PlayerPrefs.SetInt("dialogueVolume", volumeAmt);
        PlayerPrefs.Save();
    }
    public void SetSFXVolume(int volumeAmt)
    {
        float volume = Mathf.Lerp(minVolume, maxVolume, (float)volumeAmt / 10f);

        masterMixer.SetFloat("SFXVolume", volume);

        PlayerPrefs.SetInt("sfxVolume", volumeAmt);
        PlayerPrefs.Save();
    }

    public void SetBloom(bool isBloom)
    {
        bloom.active = isBloom;

        PlayerPrefs.SetInt("bloomBool", isBloom ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void SetSubtitle(bool subtitleOn)
    {
        subtitlesToggle.isOn = subtitleOn;

        PlayerPrefs.SetInt("subtitleBool", subtitleOn ? 1 : 0);
        PlayerPrefs.Save();
    }
}
