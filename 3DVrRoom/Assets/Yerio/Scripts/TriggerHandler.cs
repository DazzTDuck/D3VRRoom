using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerHandler : MonoBehaviour
{
    public UnityEvent triggerEnter;
    bool hasActivated = false;

    public void OnTriggerEnter(Collider other)
    {
        if (hasActivated) return;

        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log("detected");
            triggerEnter.Invoke();
            hasActivated = true;
        }        
    }
}
