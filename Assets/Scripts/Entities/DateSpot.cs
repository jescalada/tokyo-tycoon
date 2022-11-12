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

    public Color redTextColour;
    public Color greenTextColour;

    public TMP_ColorGradient redGradient;
    public TMP_ColorGradient greenGradient;

    override
    public void OnMouseDown()
    {
        var gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (gameManager.activeProperty != null) return;
        gameManager.activeProperty = this;
        gameManager.ShowDetailsPanel();
        gameManager.StopBGM();
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
        rentValueText.color = redTextColour;
        rentValueText.colorGradientPreset = redGradient;

        rentValueText.text = "";
        propertyNameText.text = PropertyName;
        propertyLevelText.text = string.Format("Level {0}", GetLevel());
        if (!owned)
        {
            if (FindObjectOfType<GameManager>().GetComponent<Player>().CheckMoney(GetCost()))
            {
                rentValueText.color = greenTextColour;
                rentValueText.colorGradientPreset = greenGradient;
            }
        }
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