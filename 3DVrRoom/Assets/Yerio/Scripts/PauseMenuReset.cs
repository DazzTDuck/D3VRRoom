using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuReset : MonoBehaviour
{
    VRUiButton button;
    GameManager gameManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        button = GetComponent<VRUiButton>();

        button.OnClick.AddListener(() => { StartCoroutine(gameManager.ResetGame(false)); });
    }
}
