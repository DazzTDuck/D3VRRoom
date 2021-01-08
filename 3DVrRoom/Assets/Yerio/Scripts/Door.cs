using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Door : MonoBehaviour
{
    public bool locked = true;

    public Transform keyCheck;
    public Collider doorOpenCollider;
    public Collider handleCollider;
    [SerializeField] AudioSource doorCloseSound;

    [SerializeField, Space] UnityEvent onDoorOpen;

    Quaternion closedRotation;

    private void Awake()
    {
        //CloseDoor();
        doorOpenCollider.enabled = false;
        handleCollider.enabled = true;
    }

    private void Start()
    {
        closedRotation = transform.rotation;
    }

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
            Debug.Log("open");

            onDoorOpen.Invoke();
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
        transform.rotation = closedRotation;
        LockDoor();
        doorCloseSound.Play();
    }
    public void LockDoor()
    {
        locked = true;
    }

    public float GetDistance(Transform transform)
    {
        return Vector3.Distance(keyCheck.position, transform.position);
    }
}
