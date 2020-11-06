using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool locked = true;

    public Transform keyCheck;
    public Collider doorOpenCollider;
    public Collider handleCollider;

   public void UnlockDoor()
    {
        if (locked)
        {
            locked = false;
            Debug.Log("unlocked door");
        }
    }

    public void OpenDoor()
    {
        if (!locked)
        {
            handleCollider.enabled = false;
            doorOpenCollider.enabled = true;
        }
        else
        {
            //play sound
        }
    }
    public void CloseDoor()
    {
        doorOpenCollider.enabled = false;
        handleCollider.enabled = true;
    }


    public float GetDistance(Transform transform)
    {
        return Vector3.Distance(keyCheck.position, transform.position);
    }
}
