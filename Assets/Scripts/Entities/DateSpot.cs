using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DateSpot : Property
{
    [SerializeField]
    private string[] genericDialogues;
    [SerializeField]
    private Dictionary<string, string[]> uniqueDialogues;

    public DialogueTrigger trigger;

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
    [SerializeField]
    private TextMeshProUGUI buyButtonText;
    [SerializeField]
    private TextMeshProUGUI upgradeButtonText;


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
        chatCanvas.SetActive(false);
        UpdateUI();
    }

    override
    public void UpdateUI()
    {
        rentValueText.text = "";
        propertyNameText.text = PropertyName;
        propertyLevelText.text = string.Format("Level {0}", GetLevel());
        // upgradeCostText.text = GetUpgradeCostByLevel();
        propertyDescriptionText.text = GetDescriptionByLevel();
        // Todo: Add date button for locations
        buyButtonText.text = GetBuyCostString();
        upgradeButtonText.text = GetUpgradeCostString();
    }

    override
    public void AdvanceDay()
    {

    }
}