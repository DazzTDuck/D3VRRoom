using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] bool startTimerOnStart = true;

    [Header("--Timer--")]
    [SerializeField] TMP_Text[] timerText;
    [SerializeField] float minutesToFinishGame;
    bool startTimer = false;
    float timerSeconds;

    [Header("--Code Randomizer--")]
    [SerializeField] int minCodeRange;
    [SerializeField] int maxCodeRange;

    [Header("--Lighting--")]
    [SerializeField] GameObject[] lights;

    [Header("--PrinterCode--")]
    [SerializeField] PrinterPuzzle printerPuzzle;
    [SerializeField] Keypad maintananceVaultKeypad;

    [Header("--Dumbell Code--")]
    [SerializeField] TMP_Text number1;
    [SerializeField] TMP_Text number2;
    [SerializeField] TMP_Text number3;
    [SerializeField] TMP_Text number4;
    [SerializeField] TMP_Text number5;
    [SerializeField] Keypad officeVaultKeypad;
    bool codeSet = false;

    [Header("--Dumbell Puzzle--")]
    [SerializeField] GameObject[] dumbells;
    [SerializeField] GameObject[] hidingPlacesDumbell;
    [SerializeField] GameObject[] dumbellsInDrawer;

    int amountDumbellsToHide = 3;
    List<GameObject> dumbellsChosen = new List<GameObject>();

    [Header("--Hidden Key Office--")]
    [SerializeField] GameObject[] greenKeyHidingPlaces;

    [Header("--Valves Puzzle--")]
    [SerializeField] ValvePuzzle valves;
    [SerializeField] Texture[] codeTextures;
    [SerializeField] Material codeMaterial1, codeMaterial2, codeMaterial3;
    Texture texturesOne, texturesTwo, texturesThree;
    [Space]
    [SerializeField] GameObject[] greenGaugeHidingPlaces;
    [SerializeField] SnapSystem greenGaugeSnap;

    [Header("--Hidden Fuze--")]
    [SerializeField] GameObject[] fuzeHidingPlaces;
    [SerializeField] FuzeBoxPuzzle fuzeSnap;

    //private
    PauseMenuManager pauseManager;
    AudioManager audioManager;
    BossBarks bossBarks;
    private void Awake()
    {
        pauseManager = GetComponent<PauseMenuManager>();
        audioManager = GetComponent<AudioManager>();
        bossBarks = FindObjectOfType<BossBarks>();
    }

    public void StartGame()
    {
        SetDumbellNumbers();
        HideDumbellsAndGreenKey();
        RandomizeValveCodes();
        SetSecondVaultCode();
        HideGreenGaugeAndFuze();
    }

    private void Start()
    {
        StartGame();

        if (startTimerOnStart)
        {
            StartTimer();
        }
    }

    private void Update()
    {
        UpdateTimer();
    }

    public void StartTimer()
    {
        timerSeconds = minutesToFinishGame * 60;
        startTimer = true;
    }

    void SetSecondVaultCode()
    {
        string code = GetCode().ToString();
        printerPuzzle.SetCode(code);
        maintananceVaultKeypad.SetCorrectCode(code);
    }

    void UpdateTimer()
    {
        if (startTimer && !pauseManager.paused)
        {
            timerSeconds -= Time.deltaTime;

            int hours = (int)(timerSeconds / 3600) % 24;
            int minutes = (int)(timerSeconds / 60) % 60;
            int seconds = (int)(timerSeconds % 60);

            bool lower10Seconds = seconds < 10;
            bool lower10minutes = minutes < 10;

            foreach (var text in timerText)
            {
                if (lower10Seconds && lower10minutes)
                    text.text = $"0{hours}:0{minutes}:0{seconds}";
                else if(lower10minutes)
                    text.text = $"0{hours}:0{minutes}:{seconds}";
                else if(lower10Seconds)
                    text.text = $"0{hours}:{minutes}:0{seconds}";
                else
                    text.text = $"0{hours}:{minutes}:{seconds}";
            }

            if(minutes == 20 && seconds == 00)
            {
                bossBarks.CallBark(1);
            }
            if (minutes == 10 && seconds == 00)
            {
                bossBarks.CallBark(2);
            }
            if (minutes == 5 && seconds == 00)
            {
                bossBarks.CallBark(3);
            }
            if (minutes == 1 && seconds == 00)
            {
                bossBarks.CallBark(4);
            }

            if (seconds == 6 && minutes == 0)
            {
                bossBarks.CallBark(5);
            }

            if(timerSeconds == 0)
            {
                //end
                startTimer = false;
                StartCoroutine(ResetGame());
            }
        }
    }

    void SetDumbellNumbers()
    {
        if (!codeSet)
        {
            codeSet = true;

            var code = GetCode().ToString();
            number1.text = code.Substring(0, 1);
            number2.text = code.Substring(1, 1);
            number3.text = code.Substring(2, 1);
            number4.text = code.Substring(3, 1);
            number5.text = code.Substring(4, 1);

            officeVaultKeypad.SetCorrectCode(code);
        }      
    }

    void HideDumbellsAndGreenKey()
    {
        for (int i = 0; i < dumbellsInDrawer.Length; i++)
        {
            dumbellsInDrawer[i].SetActive(true);
        }

        while (dumbellsChosen.Count < amountDumbellsToHide)
        {
            var index = Random.Range(0, dumbells.Length - 1);

            if (!dumbellsChosen.Contains(dumbells[index]))
            {
                dumbellsChosen.Add(dumbells[index]);
            }
        }

        var dumbell0Canvas = dumbellsChosen[0].GetComponentInChildren<Canvas>();
        var dumbell1Canvas = dumbellsChosen[1].GetComponentInChildren<Canvas>();
        var dumbell2Canvas = dumbellsChosen[2].GetComponentInChildren<Canvas>();

        dumbell0Canvas.gameObject.SetActive(false);
        dumbell1Canvas.gameObject.SetActive(false);
        dumbell2Canvas.gameObject.SetActive(false);

        var randomDumbell = hidingPlacesDumbell[Random.Range(0, hidingPlacesDumbell.Length - 1)];
        randomDumbell.SetActive(true);

        //greenKey
        var greenKey = greenKeyHidingPlaces[Random.Range(0, greenKeyHidingPlaces.Length - 1)];
        greenKey.SetActive(true);
        //

        for (int i = 0; i < dumbellsChosen.Count; i++)
        {
            var snapSystem = dumbellsChosen[i].GetComponent<SnapSystem>();
            snapSystem.enabled = true;
            snapSystem.SnapPlaceShow(false, false);
        }

        var dumbell0Snap = dumbellsChosen[0].GetComponent<SnapSystem>();
        var dumbell1Snap = dumbellsChosen[1].GetComponent<SnapSystem>();
        var dumbell2Snap = dumbellsChosen[2].GetComponent<SnapSystem>();

        dumbell0Snap.ObjectToSnap = dumbellsInDrawer[0].transform;
        dumbell0Snap.OnSnapped.AddListener(() => dumbell0Canvas.gameObject.SetActive(true));
        dumbell1Snap.ObjectToSnap = randomDumbell.transform;
        dumbell1Snap.OnSnapped.AddListener(() => dumbell1Canvas.gameObject.SetActive(true));
        dumbell2Snap.ObjectToSnap = dumbellsInDrawer[1].transform;
        dumbell2Snap.OnSnapped.AddListener(() => dumbell2Canvas.gameObject.SetActive(true));
    }

    void HideGreenGaugeAndFuze()
    {
        var gauge = greenGaugeHidingPlaces[Random.Range(0, greenGaugeHidingPlaces.Length - 1)];
        gauge.SetActive(true);

        greenGaugeSnap.ObjectToSnap = gauge.transform;

        var fuze = fuzeHidingPlaces[Random.Range(0, fuzeHidingPlaces.Length - 1)];
        fuze.SetActive(true);

        fuzeSnap.newFuze = fuze;
    }

    public int GetCode()
    {
        return Random.Range(minCodeRange, maxCodeRange);
    }


    public void ChangeLightState(bool state)
    {
        foreach (var light in lights)
        {
            light.SetActive(state);
        }
    }


    void RandomizeValveCodes()
    {
        List<Texture> selectedTextures = new List<Texture>();

        while (selectedTextures.Count < 3)
        {
            int randomIndex = Random.Range(0, codeTextures.Length - 1);

            Texture selected = codeTextures[randomIndex];

            if (!selectedTextures.Contains(selected))
            {
                selectedTextures.Add(selected);
            }
        }

        texturesOne = selectedTextures[0];
        texturesTwo = selectedTextures[1];
        texturesThree = selectedTextures[2];

        codeMaterial1.SetTexture("Texture2D_CEE8BD67", texturesOne);
        codeMaterial2.SetTexture("Texture2D_CEE8BD67", texturesTwo);
        codeMaterial3.SetTexture("Texture2D_CEE8BD67", texturesThree);

        //set Valves
        int code1 = GetValveCode(texturesOne.name);
        int code2 = GetValveCode(texturesTwo.name);
        int code3 = GetValveCode(texturesThree.name);
        valves.SetNewCodes(code1, code2, code3);
    }

    public int GetValveCode(string name)
    {
        return int.Parse(name.TrimStart('c', 'o', 'd', 'e'));
    }

    public IEnumerator ResetGame()
    {
        BlackScreen.FadeIn();

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        StopCoroutine(ResetGame());
    }

}
