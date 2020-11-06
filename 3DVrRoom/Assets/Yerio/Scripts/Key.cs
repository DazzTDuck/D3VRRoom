using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public Door doorToOpen;
    public float keyOpenDistance = 0.15f;
    private void Update()
    {
        if (doorToOpen.GetDistance(gameObject.transform) <= keyOpenDistance)
        {
            if (doorToOpen.locked)
                doorToOpen.UnlockDoor();
        }
    }
}
