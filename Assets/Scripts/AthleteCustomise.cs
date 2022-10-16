using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

// public enum Colors {
//     red,
//     orange,
//     yellow,
//     green,
//     blue,
//     purple,
//     white,
//     black,
//     grey,
// }

public class AthleteCustomise : MonoBehaviour
{
    AthleteSprite AS;
    AthleteManager AM;
    GameObject selectedPart;
    [SerializeField] TextMeshProUGUI selectedPartText;

    [SerializeField] GameObject skinPalette;
    [SerializeField] GameObject bodyPalette;

    void Start()
    {
        AS = FindObjectOfType<AthleteSprite>();
        AM = FindObjectOfType<AthleteManager>();
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
        else if (AS.feet.GetComponent<AvatarClick>().clicked)
        {
            selectedPart = AS.feet;
            AS.feet.GetComponent<AvatarClick>().clicked = false;
        }
        else if (AS.shoes.GetComponent<AvatarClick>().clicked)
        {
            selectedPart = AS.shoes;
            AS.shoes.GetComponent<AvatarClick>().clicked = false;
        }
        if (selectedPart != null)
        {
            selectedPartText.text = Capitalise(selectedPart.name);
            if (selectedPart.name == "skin")
            {
                skinPalette.SetActive(true);
                bodyPalette.SetActive(false);
            }
            else if (selectedPart.name == "body")
            {
                skinPalette.SetActive(false);
                bodyPalette.SetActive(true);
            }
            else if (selectedPart.name == "shoes")
            {
                skinPalette.SetActive(false);
                bodyPalette.SetActive(false);
            }
        }
        else
        {
            skinPalette.SetActive(false);
            bodyPalette.SetActive(false);
            selectedPartText.text = "No part selected";
        }
    }

    public void ChangeColour()
    {
        if (selectedPart != null && selectedPart.name == "body")
        {
            Color desiredColour = EventSystem.current.currentSelectedGameObject.GetComponent<Image>().color;
            AM.athletes[AM.selectedAthleteIDX].bodyRGB = new int[] {(int)Mathf.Round(desiredColour.r * 255), (int)Mathf.Round(desiredColour.g * 255), (int)Mathf.Round(desiredColour.b * 255)};
        }
        else if (selectedPart != null && selectedPart.name == "skin")
        {
            Color desiredColour = EventSystem.current.currentSelectedGameObject.GetComponent<Image>().color;
            AM.athletes[AM.selectedAthleteIDX].skinRGB = new int[] {(int)Mathf.Round(desiredColour.r * 255), (int)Mathf.Round(desiredColour.g * 255), (int)Mathf.Round(desiredColour.b * 255)};
        }
    }

    string Capitalise(string word)
    {
        return char.ToUpper(word[0]) + word.Substring(1);
    }
}
