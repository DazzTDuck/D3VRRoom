using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
public class BlackLightLamp : MonoBehaviour
{
    public Material reveal;
    public Light light;

    void Update()
    {
        reveal.SetVector("Vector4_58A462A8", light.transform.position);
        reveal.SetVector("Vector4_D5976873", -light.transform.forward);
        reveal.SetFloat("Vector1_985FC512", light.spotAngle);
    }
}
