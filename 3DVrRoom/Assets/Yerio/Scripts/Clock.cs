using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Clock : MonoBehaviour
{
    [SerializeField] TMP_Text clockText;

    // Update is called once per frame
    void Update()
    {
        clockText.text = System.DateTime.Now.ToString("HH:mm:ss tt");;
    }
}
