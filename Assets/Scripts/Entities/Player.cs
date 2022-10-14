using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public string Name
    { get; set; }
    public int Money
    { get; }
    public List<Property> OwnedProperties
    { get; }

}
