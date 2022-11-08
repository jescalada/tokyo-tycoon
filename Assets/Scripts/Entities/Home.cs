using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using XNodeEditor;

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

    public int UniqueDialogueCounter = 1;

    public static int UNIQUE_DIALOGUES = 10;
    public static int COLLECT_RENT_DIALOGUES_FRIENDS = 5;
    public static int COLLECT_RENT_DIALOGUES_CLOSE_FRIENDS = 10;
    public static int COLLECT_RENT_DIALOGUES_LOVERS = 15;

    public static Dictionary<Character.RelationshipLevel, int> IDLE_DIALOGUES_BY_RELATIONSHIP;
    public static Dictionary<Character.RelationshipLevel, int> RENT_COLLECTION_DIALOGUES_BY_RELATIONSHIP;

    public void Start()
    {
        IDLE_DIALOGUES_BY_RELATIONSHIP = new Dictionary<Character.RelationshipLevel, int>();
        RENT_COLLECTION_DIALOGUES_BY_RELATIONSHIP = new Dictionary<Character.RelationshipLevel, int>();

        IDLE_DIALOGUES_BY_RELATIONSHIP.Add(Character.RelationshipLevel.Stranger, 3);        // 3 Unique
        IDLE_DIALOGUES_BY_RELATIONSHIP.Add(Character.RelationshipLevel.Acquaintance, 5);    // 3 Unique
        IDLE_DIALOGUES_BY_RELATIONSHIP.Add(Character.RelationshipLevel.Friend, 7);          // 4 Unique
        IDLE_DIALOGUES_BY_RELATIONSHIP.Add(Character.RelationshipLevel.CloseFriend, 10);    // 7 Unique
        IDLE_DIALOGUES_BY_RELATIONSHIP.Add(Character.RelationshipLevel.Lover, 15);          // 13 Unique

        RENT_COLLECTION_DIALOGUES_BY_RELATIONSHIP.Add(Character.RelationshipLevel.Friend, 7);          // 5 Unique
        RENT_COLLECTION_DIALOGUES_BY_RELATIONSHIP.Add(Character.RelationshipLevel.CloseFriend, 10);    // 8 Unique
        RENT_COLLECTION_DIALOGUES_BY_RELATIONSHIP.Add(Character.RelationshipLevel.Lover, 15);          // 12 Unique
    }

    override
    public void OnMouseDown()
    {
        var gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (gameManager.activeProperty != null) return;
        gameManager.activeProperty = this;
        detailsCanvas.SetActive(true);
        UpdateUI();

        DialogueTrigger trigger = GetComponent<DialogueTrigger>();
        
        if (!Owned())
        {
            TriggerNotOwnedDialogue(trigger);
            return;
        }

        if (!collectedDailyRent && UniqueDialogueCounter < UNIQUE_DIALOGUES)
        {
            TriggerUniqueDialogue(trigger);
        } else if (!collectedDailyRent)
        {
            TriggerCollectionDialogue(trigger);
        }
        else
        {
            TriggerIdleDialogue(trigger);
        }
    }
    public void TriggerNotOwnedDialogue(DialogueTrigger trigger)
    {
        // Triggers unique dialogues which are displayed as you get to know the tenant
        trigger.Label = "NotOwned";
        trigger.Trigger();
    }

    public void TriggerUniqueDialogue(DialogueTrigger trigger)
    {
        // Triggers unique dialogues which are displayed as you get to know the tenant
        trigger.Label = string.Format("Intro{0}", UniqueDialogueCounter);
        trigger.Trigger();
        UniqueDialogueCounter++;
    }

    public void TriggerCollectionDialogue(DialogueTrigger trigger)
    {
        int availableDialogues = RENT_COLLECTION_DIALOGUES_BY_RELATIONSHIP.GetValueOrDefault(Tenant.CurrentRelationshipLevel);
        int randomNumber = Random.Range(1, availableDialogues + 1);
        trigger.Label = string.Format("Rent{0}", randomNumber);
        trigger.Trigger();
    }

    public void TriggerIdleDialogue(DialogueTrigger trigger)
    {
        // Randomly selects a dialogue among many possible idle ones
        int availableDialogues = IDLE_DIALOGUES_BY_RELATIONSHIP.GetValueOrDefault(Tenant.CurrentRelationshipLevel);
        int randomNumber = Random.Range(1, availableDialogues + 1);
        trigger.Label = string.Format("Idle{0}", randomNumber);
        trigger.Trigger();
    }

    override
    public void UpdateUI()
    {
        rentValueText.text = string.Format("${0} per day", rentValueByLevel[GetLevel()]);
        propertyNameText.text = PropertyName;
        propertyLevelText.text = string.Format("Level {0}", GetLevel());
        // upgradeCostText.text = GetUpgradeCostByLevel();
        // propertyDescriptionText.text = GetDescriptionByLevel();
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
