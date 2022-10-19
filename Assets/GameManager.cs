using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Player player;

    private List<Character> characters;
    private List<Property> properties;
    private TimeController timeController;

    public Property activeProperty;
    
    [SerializeField]
    private Button upgradeButton;
    [SerializeField]
    private Button buyButton;

    // Start is called before the first frame update
    void Start()
    {
        upgradeButton.onClick.AddListener(UpgradeActiveProperty);
        buyButton.onClick.AddListener(BuyActiveProperty);
    }

    // Update is called once per frame
    void Update()
    {
        
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

    public void AdvanceDay()
    {

    }
}
