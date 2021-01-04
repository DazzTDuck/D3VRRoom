﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWalking : MonoBehaviour
{
    [SerializeField] float walkSpeed = 2f;
    [SerializeField] Transform moveTo;
    [SerializeField] Transform start;
    [SerializeField] Transform bossTransform;

    static bool startWalking = false;

    private void Start()
    {
        //StartWalking();
    }

    private void Update()
    {
        if (startWalking)
        {
            if (!bossTransform.gameObject.activeSelf)
                bossTransform.gameObject.SetActive(true);

            if (bossTransform.position != moveTo.position)
                bossTransform.position = Vector3.MoveTowards(bossTransform.position, moveTo.position, walkSpeed * Time.deltaTime);

            if (bossTransform.position == moveTo.position)
            {
                startWalking = false;
                bossTransform.gameObject.SetActive(false);

                //bossTransform.position = start.position;
            }
        }
    }

    public void StartWalking() { startWalking = true; }
}
