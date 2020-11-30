using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vault : MonoBehaviour
{
    [SerializeField] Transform vaultDoor;
    [SerializeField] float openSpeed = 2f;

    float endRotationY = 0;
    bool openDoor = false;
    bool getRotation = false;
    Quaternion newRotation;

    private void Update()
    {
        if (openDoor && vaultDoor.rotation.y != endRotationY)
        {
            if (!getRotation)
            {
                newRotation = vaultDoor.rotation * Quaternion.AngleAxis(-90, Vector3.up);
                getRotation = true;
            }

            vaultDoor.rotation = Quaternion.Slerp(vaultDoor.rotation, newRotation, openSpeed * Time.deltaTime);
        }
    }
    public void OpenVaultDoor()
    {
        openDoor = true;
    }
}
