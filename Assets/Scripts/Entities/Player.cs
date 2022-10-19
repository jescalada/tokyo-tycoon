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
    public int Money
    { get; private set; }
    public List<Property> OwnedProperties
    { get; }

    public void BuyProperty(Property property)
    {
        OwnedProperties.Add(property);
    }

    public void AddMoney(int money)
    {
        Money += money;
    }
    public void SpendMoney(int cost)
    {
        if (Money < cost) throw new Exception(); // Todo create custom exceptions
        Money -= cost;
    }

    public bool CheckMoney(int cost)
    {
        return Money >= cost;
    }
}
