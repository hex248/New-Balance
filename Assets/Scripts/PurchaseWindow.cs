using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PurchaseWindow : MonoBehaviour
{
    [SerializeField] Image colourPreview;

    public GameObject insufficientFunds;
    [SerializeField] TextMeshProUGUI priceText;
    
    public Color desiredColour;
    public string type;
    public string id;
    public int price;
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
        colourPreview.color = desiredColour;
        priceText.text = $"{price}";
    }

    public void Purchase()
    {
        if (type == "clothing")
        {
            if (PM.player.credits >= price)
            {
                PM.player.credits -= price;
                PM.player.purchasedClothes.Add(id);
                FindObjectOfType<AthleteCustomise>().HidePurchaseWindow();
            }
            else
            {
                insufficientFunds.SetActive(true);
            }
        }
    }
}
