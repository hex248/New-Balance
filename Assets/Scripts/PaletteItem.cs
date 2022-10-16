using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaletteItem : MonoBehaviour
{
    public bool locked = false;
    public int price = 50;

    void Update()
    {
        if (locked)
        {
            transform.Find("lock").gameObject.SetActive(true);
        }
        else
        {
            transform.Find("lock").gameObject.SetActive(false);
        }
    }

    public void Click()
    {
        if (!locked)
        {
            Color desiredColour = GetComponent<Image>().color;

            FindObjectOfType<AthleteCustomise>().ChangeColour(desiredColour);
        }
        else
        {
            FindObjectOfType<AthleteCustomise>().PurchaseWindow(gameObject);
        }
    }
}
