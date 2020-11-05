using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class SubtitleManager : MonoBehaviour
{
    [Header("--Canvas--")]
    public GameObject subtilePanel;
    public TMP_Text subtitleText;
    public TMP_Text nameText;
    [Header("--Lines--")]
    public Line[] startingStageLines;
    int startingStageLineIndex;
    public Line[] officeStageLines;
    int officeStageLineIndex;
    public Line[] maintenanceStageLines;
    int maintenanceStageLineIndex;

    Line[] currentLinesSelected;

    /// <summary>
    /// Here you can set the text with a line from the line stages
    /// </summary>
    /// <param name="lineIndex">here you can give the index of the line you want to show</param>
    /// <param name="stage">you can select what stage you want to select for the correct lines, 
    /// you can choose out of 'starting', 'office' or 'maintenance'</param>
    public void SetText(int lineIndex, float timeToShow, string stage = "starting")
    {
        switch (stage)
        {
            case "starting":
                currentLinesSelected = startingStageLines;
                break;
            case "office":
                currentLinesSelected = officeStageLines;
                break;
            case "maintenance":
                currentLinesSelected = maintenanceStageLines;
                break;
            default:
                Debug.LogError("A stage has not been selected, change it to 'starting', 'office' or 'maintenance'");
                break;
        }
    }

    IEnumerator ShowTextTimed(float time)
    {
        yield return new WaitForSeconds(time);
    }
}
