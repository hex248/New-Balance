using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class SidebarItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    AthleteManager AM;
    public Athlete currentAthlete;
    public TextMeshProUGUI nameText;
    public GameObject popup;

    public bool hover = false;

    void Start()
    {
        AM = FindObjectOfType<AthleteManager>();
    }

    void LateUpdate()
    {
        if (currentAthlete.athleteName != "") {
            GetComponent<Image>().sprite = AM.athleteIcons[currentAthlete.iconIDX];
            nameText.text = currentAthlete.athleteName;
            
            if (hover) {
                popup.SetActive(true);
            }
            else
            {
                popup.SetActive(false);
            }
            currentAthlete = AM.athletes.Find(a => a.id == currentAthlete.id);
        }
        else
        {
            popup.SetActive(false);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hover = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hover = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (currentAthlete != null)
        {
            AM.SetAthleteByID(currentAthlete.id);
        }
    }
}
