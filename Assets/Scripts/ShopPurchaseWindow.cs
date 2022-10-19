using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopPurchaseWindow : MonoBehaviour
{
    [SerializeField] Image preview;
    [SerializeField] TextMeshProUGUI itemNameText;
    [SerializeField] TextMeshProUGUI itemPriceText;
    [SerializeField] GameObject insufficientFunds;

    [Header("")]
    public ItemType itemType;
    public string itemName;
    public string itemID;
    public int itemPrice;
    public Sprite previewImage;

    PlayerManager PM;

    void Start()
    {
        PM = FindObjectOfType<PlayerManager>();
    }
    void Awake()
    {
        insufficientFunds.SetActive(false);
    }

    void Update()
    {
        preview.sprite = previewImage;
        itemNameText.text = itemName;
        itemPriceText.text = $"{itemPrice}";
        preview.SetNativeSize();
    }

    public void Purchase()
    {
        if (itemType == ItemType.shoes)
        {
            if (PM.player.credits >= itemPrice)
            {
                PM.player.credits -= itemPrice;
                PM.player.purchasedShoes.Add(itemID);
                FindObjectOfType<Shop>().HidePurchaseWindow();
            }
            else
            {
                insufficientFunds.SetActive(true);
            }
        }
    }
}
