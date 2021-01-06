using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWalking : MonoBehaviour
{
    [SerializeField] float walkSpeed = 2f;
    [SerializeField] Transform moveTo;
    [SerializeField] Transform start;
    [SerializeField] Transform bossTransform;

    [Header("DOOR SEQUENCE 1")]
    [SerializeField] Line line;
    [SerializeField] AudioSource audioSource;
    SubtitleManager subtitleManager;
    AudioManager audioManager;

    static bool startWalking = false;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
        subtitleManager = FindObjectOfType<SubtitleManager>();
    }
    private void Start()
    {
        //StartWalking();
    }

    private void Update()
    {
        if (startWalking)
        {
            if (!bossTransform.gameObject.activeSelf) { bossTransform.gameObject.SetActive(true); }
                               

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

    public void StartWalking() 
    { 
        startWalking = true;
        StartCoroutine(SE2());
    }

    IEnumerator SE2()
    {
        yield return new WaitForSeconds(0.5f);

        if (!IsVoiceLinePlaying.GetIfVoiceLinePlaying())
        {
            subtitleManager.SetupSubtitle(line.line, line.name, line.lineLength);
            audioSource.Play();
            IsVoiceLinePlaying.VoicelinePlaying(line.lineLength);
        }

        yield return new WaitForSeconds(line.lineLength + 1f);
        audioManager.PlaySound("SE_2.2");
    }

}
