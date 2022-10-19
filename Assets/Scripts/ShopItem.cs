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

    void Update()
    {
        if (Application.isPlaying)
        {
            
        }
        else
        {
            transform.Find("Preview").GetComponent<Image>().sprite = preview;
        }
    }

    public void Clicked()
    {
        FindObjectOfType<Shop>().PurchaseWindow(gameObject);
    }
}
