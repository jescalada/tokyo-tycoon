using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DateSpot : Property
{
    [SerializeField]
    private string[] genericDialogues;
    [SerializeField]
    private Dictionary<string, string[]> uniqueDialogues;

    [SerializeField]
    private GameObject detailsCanvas;
    [SerializeField]
    private GameObject chatCanvas;
    [SerializeField]
    private TextMeshProUGUI rentValueText;
    [SerializeField]
    private TextMeshProUGUI propertyNameText;
    [SerializeField]
    private TextMeshProUGUI upgradeCostText;
    [SerializeField]
    private TextMeshProUGUI propertyLevelText;
    [SerializeField]
    private TextMeshProUGUI propertyDescriptionText;


    public string GetRandomDialogue()
    {
        int random = Random.Range(0, genericDialogues.Length - 1);
        return genericDialogues[random];
    }
    public string GetUniqueDialogue(string characterName)
    {
        string[] uniqueLines = uniqueDialogues[characterName];
        int random = Random.Range(0, uniqueLines.Length - 1);
        return uniqueLines[random];
    }
    override
    public void Click()
    {
        detailsCanvas.SetActive(true);
        rentValueText.text = "";
        propertyNameText.text = PropertyName;
        propertyLevelText.text = string.Format("Level {0}", Level);
        // upgradeCostText.text = GetUpgradeCostByLevel();
        propertyDescriptionText.text = GetDescriptionByLevel();
        // Todo: Add date button for locations
    }
    override
    public void AdvanceDay()
    {

    }
}