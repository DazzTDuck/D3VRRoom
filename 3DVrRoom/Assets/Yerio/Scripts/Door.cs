using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (Rigidbody))]
public class Door : MonoBehaviour
{
    public bool locked = true;
    new Rigidbody rigidbody;

    public Transform doorHandle;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.isKinematic = locked;
    }

   public void UnlockDoor()
    {
        if (locked)
        {
            locked = false;
            rigidbody.isKinematic = locked;
            Debug.Log("unlocked door");
        }
    }

    public float GetDistance(Transform transform)
    {
        return Vector3.Distance(doorHandle.position, transform.position);
    }
}
