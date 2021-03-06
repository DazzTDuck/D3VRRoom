﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class VRHeadBlocking : MonoBehaviour
{
    public GameObject player;
    [SerializeField] bool fadeEnabled = false;
    [SerializeField] Color fadeColor = Color.black;

    [HideInInspector] public bool isCollision;

    private int layerMask;
    private Collider[] objs = new Collider[10];
    private Vector3 prevHeadPos;
    private int fadeState = 0;
    private float fadeDuration = .5f;
    private float internalFadeTimer = 0f;
    private float backupCap = .2f;

    private void Start()
    {
        layerMask = 1 << 8;
        layerMask = ~layerMask;

        prevHeadPos = transform.position;

        FadeToColor();
        Invoke("FadeFromColor", fadeDuration);
    }

    private void FadeToColor()
    {
        SteamVR_Fade.View(Color.clear, 0f);
        SteamVR_Fade.View(fadeColor, fadeDuration);
    }

    private void FadeFromColor()
    {
        SteamVR_Fade.View(fadeColor, 0f);
        SteamVR_Fade.View(Color.clear, fadeDuration);
    }

    private int DetectHit(Vector3 loc)
    {
        int hits = 0;
        int size = Physics.OverlapSphereNonAlloc(loc, backupCap, objs, layerMask, QueryTriggerInteraction.Ignore);
        for (int i = 0; i < size; i++)
        {
            if (objs[i].tag != "Player")
            {
                hits++;
            }
        }
        return hits;
    }

    public void Update()
    {
        if (player != null)
        {

            // Fade state control
            if (fadeEnabled)
            {
                internalFadeTimer -= Time.deltaTime;
                if (fadeState == 1 && internalFadeTimer <= 0)
                {
                    internalFadeTimer = fadeDuration;
                    FadeFromColor();
                    fadeState = 2;
                }
                else if (fadeState == 2 && internalFadeTimer <= 0) fadeState = 0;
            }

            int hits = DetectHit(transform.position);

            // No collision
            if (hits == 0)
            {
                isCollision = false;
                prevHeadPos = transform.position;
            }
            // Collision
            else
            {
                // Player pushback
                bool notCapped = true;
                isCollision = true;
                Vector3 headDiff = transform.position - prevHeadPos;
                if (Mathf.Abs(headDiff.x) > backupCap)
                {
                    if (headDiff.x > 0) headDiff.x = backupCap;
                    else headDiff.x = backupCap * -1;
                    notCapped = false;
                }
                if (Mathf.Abs(headDiff.z) > backupCap)
                {
                    if (headDiff.z > 0) headDiff.z = backupCap;
                    else headDiff.z = backupCap * -1;
                    notCapped = false;
                }
                if (Mathf.Abs(headDiff.y) > backupCap)
                {
                    if (headDiff.y > 0) headDiff.y = backupCap;
                    else headDiff.y = backupCap * -1;
                    notCapped = false;
                }
                Vector3 adjHeadPos = new Vector3(player.transform.position.x - headDiff.x,
                                                 player.transform.position.y - headDiff.y,
                                                 player.transform.position.z - headDiff.z);
                player.transform.SetPositionAndRotation(adjHeadPos, player.transform.rotation);
                // Trigger fade if enabled
                if (fadeEnabled && notCapped && fadeState == 0)
                {
                    internalFadeTimer = fadeDuration;
                    FadeToColor();
                    fadeState = 1;
                }
            }
        }
    }
}

