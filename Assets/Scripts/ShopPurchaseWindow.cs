using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopPurchaseWindow : MonoBehaviour
{
    [SerializeField] Image preview;

    [SerializeField] GameObject insufficientFunds;

    [SerializeField] TextMeshProUGUI priceText;

    [Header("")]
    public ItemType itemType;
    public string itemName;
    public string id;
    public int price;
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
    }

    public void Purchase()
    {
        if (itemType == ItemType.shoes)
        {
            if (PM.player.credits >= price)
            {
                PM.player.credits -= price;
                PM.player.purchasedShoes.Add(id);
                FindObjectOfType<Shop>().HidePurchaseWindow();
            }
        }
    }
}
