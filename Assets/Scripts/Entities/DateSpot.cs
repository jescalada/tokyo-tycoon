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

    public Material redGlowyUI;
    public Material greenGlowyUI;

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
            gameManager.CheckIfMaxLevel();
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
        SetButtonTextColors();
        buyButtonText.text = GetBuyCostString();
        upgradeButtonText.text = GetUpgradeCostString();
    }

    public void SetButtonTextColors()
    {
        buyButtonText.color = redTextColour;
        buyButtonText.colorGradientPreset = redGradient;
        buyButtonText.fontMaterial = redGlowyUI;

        upgradeButtonText.color = redTextColour;
        upgradeButtonText.colorGradientPreset = redGradient;
        upgradeButtonText.fontMaterial = redGlowyUI;

        if (!Owned())
        {
            if (FindObjectOfType<GameManager>().GetComponent<Player>().CheckMoney(GetCost()))
            {
                buyButtonText.color = greenTextColour;
                buyButtonText.colorGradientPreset = greenGradient;
                buyButtonText.fontMaterial = greenGlowyUI;
            }
        }
        else
        {
            if (FindObjectOfType<GameManager>().GetComponent<Player>().CheckMoney(GetUpgradeCostByLevel()))
            {
                upgradeButtonText.color = greenTextColour;
                upgradeButtonText.colorGradientPreset = greenGradient;
                upgradeButtonText.fontMaterial = greenGlowyUI;
            }
            if (GetLevel() < MAX_LEVEL)
            {

            }
        }
    }

    override
    public void AdvanceDay()
    {

    }
}