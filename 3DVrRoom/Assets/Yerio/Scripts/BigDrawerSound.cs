using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigDrawerSound : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;

    public void PlayOpeningSound() { audioSource.Play(); }

}
