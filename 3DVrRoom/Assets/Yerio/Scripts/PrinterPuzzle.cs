using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PrinterPuzzle : MonoBehaviour
{
    [SerializeField] Color buttonColorCanPrint;
    [SerializeField] Color buttonColorCantPrint;
    [SerializeField] Image printerButtonImage;
    [SerializeField] TMP_Text printerButtonText;
    [SerializeField] TMP_Text paperCodeText;

    [SerializeField] Transform paper;
    [SerializeField] Transform paperMoveToPoint;
    [SerializeField] GameObject interactablePaper;
    [SerializeField] float printSpeed = 1;

    bool hasPaper = false;
    bool isPrinting = false;
    bool codeHasPrinted = false;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void Start()
    {
        printerButtonText.text = "NEEDS\nPAPER";
        printerButtonImage.color = buttonColorCantPrint;
        interactablePaper.SetActive(false);

        //HasPaper();
    }

    private void Update()
    {
        if (isPrinting)
        {
            if (!paper.gameObject.activeSelf) { paper.gameObject.SetActive(true); }

                paper.position = Vector3.Lerp(paper.position, paperMoveToPoint.position, printSpeed * Time.deltaTime);
                paper.rotation = Quaternion.Slerp(paper.rotation, paperMoveToPoint.rotation, printSpeed * Time.deltaTime);

            if (Vector3.Distance(paper.position, paperMoveToPoint.position) < 0.03f)
            { isPrinting = false; printerButtonText.text = "PRINT"; interactablePaper.SetActive(true); paper.gameObject.SetActive(false); codeHasPrinted = true; }
        }
    }

    public void Sound()
    {
        if (!hasPaper) { audioManager.PlaySound("PrinterNoPaper"); audioManager.PlaySound("OfficeInteractivePrinter"); }

        if (hasPaper) { audioManager.PlaySound("MRPrinterWorking"); audioManager.PlaySound("PrinterPrinting"); }
    }

    public void SetCode(string code)
    {
        paperCodeText.text = code.ToString();
    }

    public void PrintCode()
    {
        if (hasPaper && !codeHasPrinted)
        {
            isPrinting = true;
            printerButtonText.text = "PRINTING...";
        }
    }

    public void HasPaper() 
    { 
        hasPaper = true;
        printerButtonText.text = "PRINT";
        printerButtonImage.color = buttonColorCanPrint;
    }
}
