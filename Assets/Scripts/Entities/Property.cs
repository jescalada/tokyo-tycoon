using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Property : MonoBehaviour
{
    private static readonly int MAX_LEVEL = 10;
    public string Name
    { get; } = "Property";
    public int Cost
    { get; } = 0;
    public int Level
    { get; private set; } = 0;
    public bool Owned
    { get; private set; } = false;

    private readonly string[] upgradeNameByLevel = new string[MAX_LEVEL + 1];
    private readonly int[] upgradeCostByLevel = new int[MAX_LEVEL + 1];
    private readonly string[] descriptionByLevel = new string[MAX_LEVEL + 1];
    
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
            Debug.Log(string.Format("Upgraded {0} to level {1}.", Name, Level));
        }
        else
        {
            // Todo add visual confirmation if upgrade failed
            Debug.Log(string.Format("Upgrade failed for {0}. Current level: {1}.", Name, Level));
        }
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
