using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Property : MonoBehaviour, IClickable
{
    public static readonly int MAX_LEVEL = 10;
    
    public string PropertyName;
    
    [SerializeField]
    private int cost;
    public int Cost
    { get; } = 0;

    [SerializeField]
    private int level;
    public int Level
    { get; private set; } = 0;

    [SerializeField]
    private bool owned;
    public bool Owned
    { get; private set; } = false;

    [SerializeField]
    private string[] upgradeNameByLevel = new string[MAX_LEVEL];
    [SerializeField]
    private int[] upgradeCostByLevel = new int[MAX_LEVEL];
    [SerializeField]
    private string[] descriptionByLevel = new string[MAX_LEVEL];

    [SerializeField]
    private GameObject detailsCanvas;

    public void Buy()
    {
        Owned = true;
        Level = 1;
    }
    public void Upgrade()
    {
        if (Level <= MAX_LEVEL)
        {
            Level++;
            // Todo add visual confirmation if successfully upgraded
            Debug.Log(string.Format("Upgraded {0} to level {1}.", PropertyName, Level));
        }
        else
        {
            // Todo add visual confirmation if upgrade failed
            Debug.Log(string.Format("Upgrade failed for {0}. Current level: {1}.", PropertyName, Level));
        }
    }

    public virtual void Click()
    {
        // Set and update the details of this Property
        detailsCanvas.SetActive(true);
    }
    public string GetUpgradeNameByLevel()
    {
        return upgradeNameByLevel[Level];
    }
    public string GetDescriptionByLevel()
    {
        return descriptionByLevel[Level];
    }
    public int GetUpgradeCostByLevel()
    {
        return upgradeCostByLevel[Level];
    }

}
