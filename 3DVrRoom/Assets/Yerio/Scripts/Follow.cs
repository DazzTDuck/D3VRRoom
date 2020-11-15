using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform target;


    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = target.transform.position;
        transform.rotation = target.transform.rotation;
    }
}
