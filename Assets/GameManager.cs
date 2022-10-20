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
        moneyTextTop.text = player.Money.ToString();
        dateTextTop.text = string.Format("${0}", timeController);
    }

    public void UpgradeActiveProperty()
    {
        int upgradeCost = activeProperty.GetUpgradeCostByLevel();
        if (player.CheckMoney(upgradeCost))
        {
            activeProperty.Upgrade();
            player.SpendMoney(upgradeCost);
        }
        else
        {
            Debug.Log("You don't have enough money to upgrade this property!");
        }
    }

    public void BuyActiveProperty()
    {
        int buyCost = activeProperty.Cost;
        if (player.CheckMoney(buyCost))
        {
            activeProperty.Buy();
            player.SpendMoney(buyCost);
        }
        else
        {
            Debug.Log("You don't have enough money to buy this property!");
        }

    }

    public void CollectRentActiveProperty()
    {
        // Prevent the rent collection from showing up when clicking a DateSpot
        int moneyObtained = ((Home)activeProperty).CollectRent();
        player.AddMoney(moneyObtained);
    }

    public void AdvanceDay()
    {
        timeController.AdvanceDay();
        // Upon advancing a day, should be able to collect rent from homes once again (rent can be collected once a day)
        // Iterate through the player's properties, and advance the day on all of them
    }

    public void enableRentButton()
    {
        collectRentButton.interactable = true;
    }
    public void disableRentButton()
    {
        collectRentButton.interactable = false;
    }

}
