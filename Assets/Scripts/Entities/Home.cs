using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    [SerializeField]
    private GameObject detailsCanvas;
    [SerializeField]
    private GameObject chatCanvas;
    override
    public void Click()
    {
        detailsCanvas.SetActive(true);
        // Todo set Tenant picture to UI
        chatCanvas.SetActive(true);
    }
    public int CollectRent()
    {
        return RentValue;
    }
}
