using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class HeadClippingFix : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        SteamVR_Fade.View(Color.black, 0.05f);
    }
    private void OnCollisionStay(Collision collision)
    {
        SteamVR_Fade.View(Color.black, 0.02f);
    }
    private void OnCollisionExit(Collision collision)
    {
        SteamVR_Fade.View(Color.clear, 0.2f);
    }
}

