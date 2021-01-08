using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] bool openDoor = true;
    public Door[] doorToOpen;
    public Lockable lockableToOpen;
    public float keyOpenDistance = 0.15f;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void Update()
    {
        if (openDoor)
        {
            foreach (var door in doorToOpen)
            {
                if (door.GetDistance(gameObject.transform) <= keyOpenDistance)
                {
                    if (door.locked) { audioManager.PlaySound("Unlock"); door.UnlockDoor();}                     
                }
            }          
        }

        if (!openDoor)
        {
            if (lockableToOpen.GetDistance(gameObject.transform) <= keyOpenDistance)
            {
                if (lockableToOpen.GetIfLocked()) { lockableToOpen.UnlockDoor(); audioManager.PlaySound("Unlock"); }                   
            }
        }

    }
}
