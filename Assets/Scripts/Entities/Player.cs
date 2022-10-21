using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private string playerName;
    public string PlayerName
    { get; set; }

    [SerializeField]
    private int money;

    [SerializeField]
    private List<Property> ownedProperties;

    public void BuyProperty(Property property)
    {
        ownedProperties.Add(property);
    }

    public void AddMoney(int money)
    {
        this.money += money;
    }
    public void SpendMoney(int cost)
    {
        if (money < cost) throw new Exception(); // Todo create custom exceptions
        money -= cost;
    }

    public bool CheckMoney(int cost)
    {
        return money >= cost;
    }

    public int GetMoney()
    {
        return money;
    }

    public List<Property> GetOwnedProperties()
    {
        return ownedProperties;
    }
}
