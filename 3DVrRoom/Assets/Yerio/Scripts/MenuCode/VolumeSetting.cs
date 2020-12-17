using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VolumeSetting : MonoBehaviour
{
    [SerializeField] TMP_Text number;
    [SerializeField] int minVolume = 0;
    [SerializeField] int maxVolume = 10;
    [SerializeField] int startVolume = 7;

    [HideInInspector]
    public int volume;

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
    }

    public void DecreaseVolume()
    {
        if (volume == minVolume) return;

        volume--;
        UpdateText();
    }

    public int GetDefaultVolume() { return startVolume; }

    public void UpdateText() { number.text = volume.ToString(); }
}
