using Naninovel;
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

    public LeanTweenType panelOpenEaseType;
    public LeanTweenType panelCloseEaseType;
    public float panelAnimationDuration;

    public LeanTweenType moneyAnimationEaseType;
    public float moneyAnimationDuration;
    public Vector3[] moneyAnimationPath;

    public GameObject billAnimationPrefab;
    public GameObject heartAnimationPrefab;
    public GameObject moneyIncreaseAnimationPrefab;

    private AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        upgradeButton.onClick.AddListener(UpgradeActiveProperty);
        buyButton.onClick.AddListener(BuyActiveProperty);
        advanceDayButton.onClick.AddListener(AdvanceDay);

        audioManager = FindObjectOfType<AudioManager>();
        //audioManager.Play("Everyday Routine");
    }

    // Update is called once per frame
    void Update()
    {
        // Todo, make efficient
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
        CheckIfMaxLevel();
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
        int moneyObtained = ((Home)activeProperty).CollectRent();
        // Todo: Add animation

        PlayMoneyAnimation(moneyObtained);

        PlayBillAnimation();

        player.AddMoney(moneyObtained);
    }

    public void PlayMoneyAnimation(int moneyObtained)
    {
        var moneyUIAnimation = Instantiate(moneyIncreaseAnimationPrefab, GameObject.Find("TotalMoneyText").transform);
        moneyUIAnimation.GetComponent<TextMeshProUGUI>().text = string.Format("${0}", moneyObtained);

        LeanTween.scale(moneyUIAnimation, new Vector3(1, 1, 1), 0.35f)
            .setEase(panelOpenEaseType);

        LeanTween.moveSplineLocal(moneyUIAnimation, moneyAnimationPath, moneyAnimationDuration)
            .setEase(moneyAnimationEaseType)
            .setOnComplete(delegate () { Destroy(moneyUIAnimation); });
    }

    private void PlayBillAnimation()
    {
        var billAnimation = Instantiate(billAnimationPrefab);
        billAnimation.transform.SetPosX(-3.75f);
        billAnimation.transform.SetPosY(-2.5f);
        billAnimation.transform.SetPosZ(60f);
        billAnimation.transform.localScale = new Vector3(0.5f, 0.5f, -3);
        Destroy(billAnimation, 2f);
    }

    public void PlayHeartAnimation()
    {
        var heartAnimation = Instantiate(heartAnimationPrefab);
        heartAnimation.transform.SetPosX(-3.75f);
        heartAnimation.transform.SetPosY(-2.5f);
        heartAnimation.transform.SetPosZ(60f);
        heartAnimation.transform.localScale = new Vector3(0.25f, 0.25f, -5);
        Destroy(heartAnimation, 2f);
    }

    public void IncreaseRelationshipActiveCharacter(int relationshipPoints)
    {
        var character = ((Home)activeProperty).Tenant;
        character.GainRelationshipPoints(relationshipPoints);
        PlayHeartAnimation();

        // Todo: Add meter change animation
        character.SetRelationshipMeter();
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
        Invoke("NullifyActiveProperty", 0.5f);
        HideDetailsPanel();
    }

    private void NullifyActiveProperty()
    {
        activeProperty = null;
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
        upgradeButton.interactable = true;
        upgradeButton.gameObject.SetActive(true);
    }
    public void DisableUpgradeButton()
    {
        // upgradeButton.interactable = false;
        upgradeButton.gameObject.SetActive(false);
    }

    public void HideDetailsPanel()
    {
        LeanTween.scale(detailsPanel, new Vector3(0, 0, 0), panelAnimationDuration)
            .setEase(panelCloseEaseType)
            .setOnComplete(delegate () { detailsPanel.SetActive(false); });
    }

    public void ShowDetailsPanel()
    {
        detailsPanel.SetActive(true);
        LeanTween.scale(detailsPanel, new Vector3(1, 1, 1), panelAnimationDuration)
            .setEase(panelOpenEaseType);
    }

    public void CheckIfMaxLevel()
    {
        if (activeProperty.GetLevel() == Property.MAX_LEVEL)
        {
            upgradeButton.interactable = false;
            upgradeButton.GetComponentInChildren<TextMeshProUGUI>().text = "MAX";
        } else
        {
            upgradeButton.interactable = true;
        }
    }

    public void ResetMaxedUpgradeButton()
    {
        upgradeButton.interactable = true;
    }

    public void StopBGM()
    {
        StopAllCoroutines();
        StartCoroutine(audioManager.StopBGM());
    }

    public void ResumeBGM()
    {
        StopAllCoroutines();
        StartCoroutine(audioManager.PlayBGM("Everyday Routine"));
    }

}
