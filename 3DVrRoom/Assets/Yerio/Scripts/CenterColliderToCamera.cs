using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterColliderToCamera : MonoBehaviour
{
    [SerializeField] Transform cameraTransform;
    [SerializeField] CharacterController characterController;

    // Update is called once per frame
    void Update()
    {
        var camPosistion = cameraTransform.transform.localPosition;

        camPosistion.y = characterController.center.y;
        camPosistion.Normalize();

        characterController.center = camPosistion;
    }
}
