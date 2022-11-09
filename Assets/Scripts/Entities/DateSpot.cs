using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class DateSpot : Property
{
    [SerializeField]
    private string[] genericDialogues;
    [SerializeField]
    private Dictionary<string, string[]> uniqueDialogues;

    public DialogueTrigger trigger;

    [SerializeField]
    private TextMeshProUGUI rentValueText;
    [SerializeField]
    private TextMeshProUGUI propertyNameText;
    [SerializeField]
    private TextMeshProUGUI upgradeCostText;
    [SerializeField]
    private TextMeshProUGUI propertyLevelText;
    [SerializeField]
    private TextMeshProUGUI buyButtonText;
    [SerializeField]
    private TextMeshProUGUI upgradeButtonText;

    override
    public void OnMouseDown()
    {
        var gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (gameManager.activeProperty != null) return;
        gameManager.activeProperty = this;
        gameManager.ShowDetailsPanel();
        UpdateUI();
        if (Owned())
        {
            gameManager.DisableBuyButton();
            gameManager.EnableUpgradeButton();
        }
        else
        {
            gameManager.EnableBuyButton();
            gameManager.DisableUpgradeButton();
        }
        DialogueTrigger trigger = GetComponent<DialogueTrigger>();
        trigger.Label = string.Format("Level{0}", GetLevel());
        trigger.Trigger();
    }

    override
    public void UpdateUI()
    {
        rentValueText.text = "";
        propertyNameText.text = PropertyName;
        propertyLevelText.text = string.Format("Level {0}", GetLevel());
        // upgradeCostText.text = GetUpgradeCostByLevel();
        // propertyDescriptionText.text = GetDescriptionByLevel();
        // Todo: Add date button for locations
        buyButtonText.text = GetBuyCostString();
        upgradeButtonText.text = GetUpgradeCostString();
    }

    override
    public void AdvanceDay()
    {

    }
}