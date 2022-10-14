using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Home : Property
{
    [SerializeField]
    private int rentValue;
    public int RentValue
    { get; }

    [SerializeField]
    private Character tenant;
    public Character Tenant
    { get; }
    
    public int CollectRent()
    {
        return RentValue;
    }
}
