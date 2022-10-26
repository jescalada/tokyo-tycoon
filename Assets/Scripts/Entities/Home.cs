using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Home : Property
{
    [SerializeField]
    private int[] rentValueByLevel = new int[MAX_LEVEL];
    public int[] RentValueByLevel
    { get; }

    public Character Tenant;


    [SerializeField]
    private bool collectedDailyRent;
    
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


    override
    public void Click()
    {
        detailsCanvas.SetActive(true);
        UpdateUI();
        chatCanvas.SetActive(true);
    }
    override
    public void UpdateUI()
    {
        rentValueText.text = string.Format("${0} per day", rentValueByLevel[GetLevel()]);
        propertyNameText.text = PropertyName;
        propertyLevelText.text = string.Format("Level {0}", GetLevel());
        // upgradeCostText.text = GetUpgradeCostByLevel();
        propertyDescriptionText.text = GetDescriptionByLevel();
        // Todo set Tenant picture to UI
        buyButtonText.text = GetBuyCostString();
        upgradeButtonText.text = GetUpgradeCostString();
    }

    public int CollectRent()
    {
        if (collectedDailyRent) Debug.Log("You already collected rent for the day!");
        collectedDailyRent = true;
        return rentValueByLevel[GetLevel()];
    }
    override
    public void AdvanceDay()
    {
        // Enable Collect button for this instance (will be done with collectedDailyRent)
        collectedDailyRent = false;
    }

    public bool CollectedDailyRent()
    {
        return collectedDailyRent;
    }
}
