using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Player player;

    private List<Character> characters;
    private List<Property> properties;

    [SerializeField]
    private TimeController timeController;

    public Property activeProperty;
    
    [SerializeField]
    private Button upgradeButton;
    [SerializeField]
    private Button buyButton;
    [SerializeField]
    private Button advanceDayButton;
    [SerializeField]
    private Button collectRentButton;

    [SerializeField]
    private TextMeshProUGUI moneyTextTop;
    [SerializeField]
    private TextMeshProUGUI dateTextTop;

    [SerializeField]
    private GameObject detailsPanel;
    [SerializeField]
    private GameObject dialoguePanel;

    // Start is called before the first frame update
    void Start()
    {
        upgradeButton.onClick.AddListener(UpgradeActiveProperty);
        buyButton.onClick.AddListener(BuyActiveProperty);
        advanceDayButton.onClick.AddListener(AdvanceDay);
        collectRentButton.onClick.AddListener(CollectRentActiveProperty);
    }

    // Update is called once per frame
    void Update()
    {
        moneyTextTop.text = string.Format("${0}", player.GetMoney());
        dateTextTop.text = timeController.ToString();
    }

    public void UpgradeActiveProperty()
    {
        int upgradeCost = activeProperty.GetUpgradeCostByLevel();
        if (player.CheckMoney(upgradeCost))
        {
            activeProperty.Upgrade();
            activeProperty.UpdateUI();
            player.SpendMoney(upgradeCost);
        }
        else
        {
            // Todo: give visual feedback that money isn't enough
            Debug.Log("You don't have enough money to upgrade this property!");
        }
        if (activeProperty.GetLevel() == Property.MAX_LEVEL)
        {
            SetMaxedUpgradeButton();
            upgradeButton.GetComponentInChildren<TextMeshProUGUI>().text = "MAX";
        }
    }

    public void BuyActiveProperty()
    {
        int buyCost = activeProperty.GetCost();
        if (player.CheckMoney(buyCost))
        {
            activeProperty.Buy();
            activeProperty.UpdateUI();
            player.SpendMoney(buyCost);
            player.BuyProperty(activeProperty);
            DisableBuyButton();
            EnableUpgradeButton();
        }
        else
        {
            // Todo: give visual feedback that money isn't enough
            Debug.Log("You don't have enough money to buy this property!");
        }

    }

    public void CollectRentActiveProperty()
    {
        // Prevent the rent collection from showing up when clicking a DateSpot
        int moneyObtained = ((Home)activeProperty).CollectRent();
        DisableRentButton();
        player.AddMoney(moneyObtained);
    }

    public void AdvanceDay()
    {
        // Todo add before advancing day
        timeController.AdvanceDay();
        detailsPanel.SetActive(false);
        dialoguePanel.SetActive(false);
        // Upon advancing a day, should be able to collect rent from homes once again (rent can be collected once a day)
        // Iterate through the player's properties, and advance the day on all of them
        foreach (Property property in player.GetOwnedProperties())
        {
            property.AdvanceDay();
        }
    }

    public void EnableBuyButton()
    {
        // buyButton.interactable = true;
        buyButton.gameObject.SetActive(true);
    }
    public void DisableBuyButton()
    {
        // buyButton.interactable = false;
        buyButton.gameObject.SetActive(false);
    }
    public void EnableUpgradeButton()
    {
        // upgradeButton.interactable = true;
        upgradeButton.gameObject.SetActive(true);
    }
    public void DisableUpgradeButton()
    {
        // upgradeButton.interactable = false;
        upgradeButton.gameObject.SetActive(false);
    }
    public void SetMaxedUpgradeButton()
    {
        upgradeButton.interactable = false;
    }
    public void ResetMaxedUpgradeButton()
    {
        upgradeButton.interactable = true;
    }

    public void EnableRentButton()
    {
        collectRentButton.interactable = true;
    }
    public void DisableRentButton()
    {
        collectRentButton.interactable = false;
    }

}
