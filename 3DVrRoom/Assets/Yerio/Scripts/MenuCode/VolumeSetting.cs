using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

[System.Serializable]
/// <summary>
/// UnityEvent callback for when a toggle is toggled.
/// </summary>
public class VolumeEvent : UnityEvent<int>
{ }

public class VolumeSetting : MonoBehaviour
{
    [SerializeField] TMP_Text number;
    [SerializeField] int minVolume = 0;
    [SerializeField] int maxVolume = 10;
    [SerializeField] int startVolume = 7;

    [SerializeField] VolumeEvent OnValueChanged = new VolumeEvent();

    int volume;

    private void Awake()
    {
        volume = startVolume;
        UpdateText();
    }

    public void AddVolume()
    {
        if (volume == maxVolume) return;

        volume++;
        UpdateText();
        OnValueChanged.Invoke(volume);
    }

    public void DecreaseVolume()
    {
        if (volume == minVolume) return;

        volume--;
        UpdateText();
        OnValueChanged.Invoke(volume);
    }

    public void SetVolume(int newVolume) { volume = newVolume; UpdateText(); }

    public int GetDefaultVolume() { return startVolume; }
    public int GetCurrentVolume() { return volume; }

    public void UpdateText() { number.text = volume.ToString(); }
}
