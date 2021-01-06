using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBarks : MonoBehaviour
{
    [SerializeField] AudioClip[] Barks;
    [SerializeField] Line[] BarkLines;

    AudioManager audioManager;
    SubtitleManager subtitleManager;
    AudioSource audioSource;

    private void Awake()
    {
        subtitleManager = FindObjectOfType<SubtitleManager>();
        audioSource = GetComponent<AudioSource>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    public void CallBark(int index)
    {
        if (!IsVoiceLinePlaying.GetIfVoiceLinePlaying())
        {
            audioSource.clip = Barks[index];
            audioSource.Play();
            subtitleManager.SetupSubtitle(BarkLines[index].line, BarkLines[index].name, BarkLines[index].lineLength);
            IsVoiceLinePlaying.VoicelinePlaying(BarkLines[index].lineLength);
        }
    }

    public void StartOfficeSequence()
    {
        StartCoroutine(OfficeSequence());
    }

    public IEnumerator OfficeSequence()
    {
        yield return new WaitForSeconds(3);
        CallBark(6);
        yield return new WaitForSeconds(BarkLines[6].lineLength + 1);
        audioManager.PlaySound("SE_3.2");
        StopCoroutine(OfficeSequence());
    }
}
