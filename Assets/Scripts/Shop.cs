using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ItemType
{
    shoes,
    athlete
}

public class Shop : MonoBehaviour
{
    PlayerManager PM;
    [SerializeField] ShopPurchaseWindow purchaseWindow;
    
    void Start()
    {
        purchaseWindow.gameObject.SetActive(false);
        PM = FindObjectOfType<PlayerManager>();
    }

    void Update()
    {
        for (int i = 0; i < PM.player.purchasedShoes.Count; i++)
        {
            GameObject shoe = GameObject.Find(PM.player.purchasedShoes[i]);
            if (shoe != null)
            {
                shoe.GetComponent<ShopItem>().locked = false;
                shoe.GetComponent<ShopItem>().purchased = true;
                shoe.transform.Find("Button").GetComponent<Button>().interactable = false;
            }
        }
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
        purchaseWindow.insufficientFunds.SetActive(false);
        purchaseWindow.gameObject.SetActive(false);
    }
}
