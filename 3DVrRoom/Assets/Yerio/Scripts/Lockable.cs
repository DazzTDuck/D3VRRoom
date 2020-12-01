using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class Lockable : MonoBehaviour
{
    [SerializeField] GameObject moveableObject;
    [SerializeField] GameObject inmoveableObject;
    [SerializeField] Transform keyCheck;
    [SerializeField] bool locked = true;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GetComponent<AudioManager>();

        if (locked)
        {
            moveableObject.SetActive(false);
            inmoveableObject.SetActive(true);
        }
        else
        {
            moveableObject.SetActive(true);
            inmoveableObject.SetActive(false);
        }
            
    }

    public void UnlockDoor()
    {
        moveableObject.SetActive(true);
        inmoveableObject.SetActive(false);
        locked = false;

        //play unlock sound
    }

    public bool GetIfLocked()
    {
        return locked;
    }

    public float GetDistance(Transform transform)
    {
        return Vector3.Distance(keyCheck.position, transform.position);
    }
}
