using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    [SerializeField]
    private GameManager gameManager;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
            if (hit)
            {
                IClickable clickable = hit.collider.GetComponent<IClickable>();
                clickable?.Click();
                Property property = (Property) clickable;
                gameManager.activeProperty = property; // Todo make this more OOP-ish
                
                string clickableType = property.GetType().ToString();
                if (clickableType.Equals("Home"))
                {
                    Home home = (Home) clickable;
                    if (home.CollectedDailyRent())
                    {
                        gameManager.DisableRentButton();
                    }
                    else
                    {
                        gameManager.EnableRentButton();
                    }
                }

                if (property.Owned())
                {
                    gameManager.DisableBuyButton();
                    gameManager.EnableUpgradeButton();
                    if (property.GetLevel() >= Property.MAX_LEVEL)
                    {
                        gameManager.SetMaxedUpgradeButton();
                    }
                } else
                {
                    gameManager.ResetMaxedUpgradeButton();
                    gameManager.EnableBuyButton();
                    gameManager.DisableUpgradeButton();
                }

                
            }
        }
    }
}