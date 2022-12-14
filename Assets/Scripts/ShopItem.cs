using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class ShopItem : MonoBehaviour
{
    public Sprite preview;

    public ItemType itemType;
    public string itemName;
    public string itemID;
    public int price;

    public bool locked = false;
    public bool purchased = false;

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
        if (purchased)
        {
            transform.Find("check").gameObject.SetActive(true);
        }
        else
        {
            transform.Find("check").gameObject.SetActive(false);
        }
        transform.Find("Preview").GetComponent<Image>().sprite = preview;
    }

    public void Clicked()
    {
        FindObjectOfType<Shop>().PurchaseWindow(gameObject);
    }
}
