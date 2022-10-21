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

    [SerializeField]
    private int level;

    [SerializeField]
    private bool owned;

    [SerializeField]
    private string[] upgradeNameByLevel = new string[MAX_LEVEL];
    [SerializeField]
    private int[] upgradeCostByLevel = new int[MAX_LEVEL];
    [SerializeField]
    private string[] descriptionByLevel = new string[MAX_LEVEL];

    public void Buy()
    {
        owned = true;
        level = 1;
    }
    public void Upgrade()
    {
        if (level <= MAX_LEVEL)
        {
            level++;
            // Todo add visual confirmation if successfully upgraded
            Debug.Log(string.Format("Upgraded {0} to level {1}.", PropertyName, level));
        }
        else
        {
            // Todo add visual confirmation if upgrade failed
            Debug.Log(string.Format("Upgrade failed for {0}. Current level: {1}.", PropertyName, level));
        }
    }

    public abstract void Click();

    public abstract void AdvanceDay();

    public abstract void UpdateUI();

    public string GetUpgradeNameByLevel()
    {
        return upgradeNameByLevel[level];
    }
    public string GetDescriptionByLevel()
    {
        return descriptionByLevel[level];
    }
    public int GetUpgradeCostByLevel()
    {
        return upgradeCostByLevel[level];
    }

    public int GetLevel()
    {
        return level;
    }

    public int GetCost()
    {
        return cost;
    }

    public bool Owned()
    {
        return owned;
    }

    public string GetBuyCostString()
    {
        return string.Format("Buy\n${0}", cost);
    }

    public string GetUpgradeCostString()
    {
        return string.Format("{0}\n${1}", GetUpgradeNameByLevel(), GetUpgradeCostByLevel());
    }
}
