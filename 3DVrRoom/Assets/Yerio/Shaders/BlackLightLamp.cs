using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class BlackLightLamp : MonoBehaviour
{
    [SerializeField] Material reveal;
    [SerializeField] Light light;

    float minDistance = 0;
    [SerializeField] float maxDistance;

    float minStrength = 0;
    [SerializeField] float maxStrength = 20;

    float distance;
    float strength;
    float percentage;

    void Update()
    {
        distance = Vector3.Distance(transform.position, light.gameObject.transform.position);
        distance = Mathf.Clamp(distance, minDistance, maxDistance);

        percentage = distance * 100 / maxDistance;
        var newPercentage = 100 - percentage;

        strength = (newPercentage / 100) * maxStrength;
        //Debug.Log(strength);

        if (light.enabled)
        {
            reveal.SetFloat("Vector1_4E73A3A0", strength);
            reveal.SetVector("Vector4_58A462A8", light.transform.position);
            reveal.SetVector("Vector4_D5976873", -light.transform.forward);
            reveal.SetFloat("Vector1_985FC512", light.spotAngle);
        }
        else
        {
            reveal.SetFloat("Vector1_4E73A3A0", 0);
        }
    }
}
