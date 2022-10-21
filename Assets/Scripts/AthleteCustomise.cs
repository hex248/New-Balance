using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class AthleteCustomise : MonoBehaviour
{
    AthleteSprite AS;
    AthleteManager AM;
    PlayerManager PM;
    GameObject selectedPart;
    [SerializeField] TextMeshProUGUI selectedPartText;

    [SerializeField] GameObject skinPalette;
    [SerializeField] GameObject bodyPalette;
    [SerializeField] GameObject shoeSelection;

    [SerializeField] PurchaseWindow purchaseWindow;

    void Start()
    {
        AS = FindObjectOfType<AthleteSprite>();
        AM = FindObjectOfType<AthleteManager>();
        PM = FindObjectOfType<PlayerManager>();
        selectedPart = AS.skin;
        purchaseWindow.gameObject.SetActive(false);
    }

    void Update()
    {
        if (AS.skin.GetComponent<AvatarClick>().clicked)
        {
            selectedPart = AS.skin;
            AS.skin.GetComponent<AvatarClick>().clicked = false;
        }
        else if (AS.body.GetComponent<AvatarClick>().clicked)
        {
            selectedPart = AS.body;
            AS.body.GetComponent<AvatarClick>().clicked = false;
        }
        else if (AS.shoes.GetComponent<AvatarClick>().clicked)
        {
            selectedPart = AS.shoes;
            AS.shoes.GetComponent<AvatarClick>().clicked = false;
        }
        if (selectedPart != null)
        {
            if (selectedPart.name == "skin")
            {
                selectedPartText.text = "Adjust Skin Tone";
                skinPalette.SetActive(true);
                bodyPalette.SetActive(false);
                shoeSelection.SetActive(false);
            }
            else if (selectedPart.name == "body")
            {
                selectedPartText.text = "Clothing Colour";
                skinPalette.SetActive(false);
                bodyPalette.SetActive(true);
                shoeSelection.SetActive(false);
            }
            else if (selectedPart.name == "shoes")
            {
                selectedPartText.text = "Shoes";
                skinPalette.SetActive(false);
                bodyPalette.SetActive(false);
                shoeSelection.SetActive(true);
            }
        }
        else
        {
            skinPalette.SetActive(false);
            bodyPalette.SetActive(false);
            shoeSelection.SetActive(false);
            selectedPartText.text = "";
        }

        for (int i = 0; i < PM.player.purchasedClothes.Count; i++)
        {
            GameObject paletteItem = GameObject.Find(PM.player.purchasedClothes[i]);
            if (paletteItem != null) paletteItem.GetComponent<PaletteItem>().locked = false;
        }
        for (int i = 0; i < PM.player.purchasedShoes.Count; i++)
        {
            GameObject shoe = transform.Find($"Customisation Options/Shoes/{PM.player.purchasedShoes[i]}").gameObject;
            if (shoe != null) shoe.GetComponent<LockerItem>().locked = false;
        }
    }

    public void ChangeColour(Color newColour)
    {
        if (selectedPart != null && selectedPart.name == "body")
        {
            AM.athletes[AM.selectedAthleteIDX].bodyRGB = new int[] {(int)Mathf.Round(newColour.r * 255), (int)Mathf.Round(newColour.g * 255), (int)Mathf.Round(newColour.b * 255)};
        }
        else if (selectedPart != null && selectedPart.name == "skin")
        {
            AM.athletes[AM.selectedAthleteIDX].skinRGB = new int[] {(int)Mathf.Round(newColour.r * 255), (int)Mathf.Round(newColour.g * 255), (int)Mathf.Round(newColour.b * 255)};
        }
    }
    
    public void ChangeShoe(LockerItem shoe)
    {
        AM.athletes[AM.selectedAthleteIDX].shoes = shoe.itemName;
        Debug.Log(shoe.itemName);
    }

    string Capitalise(string word)
    {
        return char.ToUpper(word[0]) + word.Substring(1);
    }
    
    public void PurchaseWindow(GameObject gameObject)
    {
        purchaseWindow.gameObject.SetActive(true);
        purchaseWindow.desiredColour = gameObject.GetComponent<Image>().color;
        purchaseWindow.type = "clothing"; //!HARDCODED FIX LATER
        purchaseWindow.id = gameObject.name;
        purchaseWindow.price = gameObject.GetComponent<PaletteItem>().price;
    }

    public void HidePurchaseWindow()
    {
        purchaseWindow.insufficientFunds.SetActive(false);
        purchaseWindow.gameObject.SetActive(false);
    }
}
