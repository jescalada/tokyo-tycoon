using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Home : Property
{
    public int RentValue
    { get; }
    public Character Tenant
    { get; }
    
    public int CollectRent()
    {
        return RentValue;
    }
}
