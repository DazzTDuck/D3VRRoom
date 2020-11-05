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

        Debug.Log("detected");
        triggerEnter.Invoke();
        hasActivated = true;
    }

    public void Kinker()
    {
        Debug.Log("quack");
    }
}
