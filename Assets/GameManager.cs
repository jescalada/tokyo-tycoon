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
    private TextMeshProUGUI moneyTextTop;
    [SerializeField]
    private TextMeshProUGUI dateTextTop;

    [SerializeField]
    private GameObject detailsPanel;

    // Start is called before the first frame update
    void Start()
    {
        upgradeButton.onClick.AddListener(UpgradeActiveProperty);
        buyButton.onClick.AddListener(BuyActiveProperty);
        advanceDayButton.onClick.AddListener(AdvanceDay);
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
        // Todo: Add animation
        player.AddMoney(moneyObtained);
    }

    public void IncreaseRelationshipActiveCharacter(int relationshipPoints)
    {
        // Todo: Add animation
        var character = ((Home)activeProperty).Tenant;
        character.GainRelationshipPoints(relationshipPoints);
        character.SetRelationshipMeter();
        Debug.Log(string.Format("Increasing relationship with {0} by {1}", character.Name, relationshipPoints));
    }

    public void DecreaseRelationshipActiveCharacter(int relationshipPoints)
    {
        // Todo: Add animation
        var character = ((Home)activeProperty).Tenant;
        character.LoseRelationshipPoints(relationshipPoints);
        character.SetRelationshipMeter();
    }

    public void AdvanceDay()
    {
        // Todo add before advancing day
        timeController.AdvanceDay();
        HideDetailsPanel();
        // Upon advancing a day, should be able to collect rent from homes once again (rent can be collected once a day)
        // Iterate through the player's properties, and advance the day on all of them
        foreach (Property property in player.GetOwnedProperties())
        {
            property.AdvanceDay();
        }
    }

    public void ResetActiveProperty()
    {
        activeProperty = null;
        HideDetailsPanel();
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

    public void HideDetailsPanel()
    {
        LeanTween.scale(detailsPanel, new Vector3(0, 0, 0), 0.5f).setOnComplete(delegate () { detailsPanel.SetActive(false); });
    }

    public void ShowDetailsPanel()
    {
        detailsPanel.SetActive(true);
        LeanTween.scale(detailsPanel, new Vector3(1, 1, 1), 0.5f);
    }

    public void SetMaxedUpgradeButton()
    {
        upgradeButton.interactable = false;
    }
    public void ResetMaxedUpgradeButton()
    {
        upgradeButton.interactable = true;
    }

}
