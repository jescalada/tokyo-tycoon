using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Home : Property
{
    [SerializeField]
    private int[] rentValueByLevel = new int[MAX_LEVEL];
    public int RentValueByLevel
    { get; }

    [SerializeField]
    private Character tenant;
    public Character Tenant
    { get; }

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


    override
    public void Click()
    {
        detailsCanvas.SetActive(true);
        rentValueText.text = string.Format("${0} per day", rentValueByLevel);
        propertyNameText.text = PropertyName;
        propertyLevelText.text = string.Format("Level {0}", Level);
        // upgradeCostText.text = GetUpgradeCostByLevel(Level);
        // Todo set Tenant picture to UI
        chatCanvas.SetActive(true);
    }
    public int CollectRent()
    {
        return RentValueByLevel;
    }
}
