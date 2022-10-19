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
                Debug.Log(string.Format("Clickable is of type {0}", clickable.GetType()));
                gameManager.activeProperty = (Property) clickable; // Todo make this more OOP-ish
            }
        }
    }
}
