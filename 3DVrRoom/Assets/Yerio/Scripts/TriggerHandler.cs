using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerHandler : MonoBehaviour
{
    [SerializeField] bool callsVoiceLineNotAudioManager = false;
    [SerializeField] float lengthVoiceLine = 0f;
    public UnityEvent triggerEnter;
    bool hasActivated = false;

    public void OnTriggerEnter(Collider other)
    {
        if (hasActivated) return;

        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (callsVoiceLineNotAudioManager)
            {
                if (!IsVoiceLinePlaying.GetIfVoiceLinePlaying())
                {
                    triggerEnter.Invoke();
                    hasActivated = true;
                    IsVoiceLinePlaying.VoicelinePlaying(lengthVoiceLine);
                    return;
                }
                else return;
            }

            triggerEnter.Invoke();
            hasActivated = true;
        }        
    }

}
