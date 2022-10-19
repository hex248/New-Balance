using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    shoes,
    athlete
}

public class Shop : MonoBehaviour
{
    [SerializeField] ShopPurchaseWindow purchaseWindow;
    
    void Start()
    {
        purchaseWindow.gameObject.SetActive(false);
    }

    void Update()
    {
        
    }

    public void PurchaseWindow(GameObject itemObject)
    {
        ShopItem item = itemObject.GetComponent<ShopItem>();
        purchaseWindow.itemType = item.itemType;
        purchaseWindow.itemName = item.itemName;
        purchaseWindow.itemID = item.itemID;
        purchaseWindow.itemPrice = item.price;
        purchaseWindow.previewImage = item.preview;
        
        purchaseWindow.gameObject.SetActive(true);
    }

    public void HidePurchaseWindow()
    {
        purchaseWindow.gameObject.SetActive(false);
    }
}
