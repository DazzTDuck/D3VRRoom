using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class PlayerLocalMotion : MonoBehaviour
{
    public SteamVR_Action_Vector2 input;
    public float speed = 1;

    CharacterController controller;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if(input.axis.magnitude > 0.1)
        {
            Vector3 direction = Player.instance.hmdTransform.TransformDirection(new Vector3(input.axis.x, 0, input.axis.y));
            controller.Move(speed * Time.deltaTime * Vector3.ProjectOnPlane(direction.normalized, Vector3.up) - new Vector3(0, 9.81f, 0) * Time.deltaTime);
        }       
    }
}
