using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public Door doorToOpen;
    public Lockable lockableToOpen;
    public float keyOpenDistance = 0.15f;
    private void Update()
    {
        if (doorToOpen)
        {
            if (doorToOpen.GetDistance(gameObject.transform) <= keyOpenDistance)
            {
                if (doorToOpen.locked)
                    doorToOpen.UnlockDoor();
            }
        }

        if (lockableToOpen)
        {
            if (lockableToOpen.GetDistance(gameObject.transform) <= keyOpenDistance)
            {
                if (lockableToOpen.GetIfLocked())
                    lockableToOpen.UnlockDoor();

            }
        }

    }
}
