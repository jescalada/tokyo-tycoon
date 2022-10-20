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

    [SerializeField]
    private Character tenant;
    public Character Tenant
    { get; }


    [SerializeField]
    private bool collectedDailyRent;
    public bool CollectedDailyRent
    { get; private set; }

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


    override
    public void Click()
    {
        detailsCanvas.SetActive(true);
        rentValueText.text = string.Format("${0} per day", rentValueByLevel[Level]);
        propertyNameText.text = PropertyName;
        propertyLevelText.text = string.Format("Level {0}", Level);
        // upgradeCostText.text = GetUpgradeCostByLevel();
        propertyDescriptionText.text = GetDescriptionByLevel();
        // Todo set Tenant picture to UI
        chatCanvas.SetActive(true);
    }
    public int CollectRent()
    {
        if (collectedDailyRent) Debug.Log("You already collected rent for the day!");
        collectedDailyRent = true;
        return rentValueByLevel[Level];
    }
    override
    public void AdvanceDay()
    {
        // Enable Collect button for this instance (will be done with collectedDailyRent)
        collectedDailyRent = false;
    }
}
