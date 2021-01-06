using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsVoiceLinePlaying : MonoBehaviour
{
    static bool isVoiceLinePlaying;

    static float timeToReset = 0;

    bool resetStarted;

    public static void VoicelinePlaying(float time) { isVoiceLinePlaying = true; timeToReset = time; }
    public static void VoicelineNotPlaying() { isVoiceLinePlaying = false;} 

    public static bool GetIfVoiceLinePlaying() { return isVoiceLinePlaying; }

    public void StartResetBool() { if (isVoiceLinePlaying && !resetStarted) { StartCoroutine(ResetBool(timeToReset)); resetStarted = true; } }

    private void Update()
    {
        StartResetBool();

        if (!GetIfVoiceLinePlaying())
        {
            StopCoroutine(nameof(ResetBool));
        }
    }

    IEnumerator ResetBool(float time)
    {
        yield return new WaitForSeconds(time);

        VoicelineNotPlaying();
        resetStarted = false;
    }
}
