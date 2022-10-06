using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class SidebarItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    AthleteManager athleteManager;
    public Athlete currentAthlete;
    public TextMeshProUGUI nameText;
    public GameObject popup;

    public bool hover = false;

    void Start()
    {
        athleteManager = FindObjectOfType<AthleteManager>();
    }

    void Update()
    {
        if (currentAthlete != null) {
            GetComponent<Image>().sprite = currentAthlete.icon;
            nameText.text = currentAthlete.athleteName;
            
            if (hover) {
                popup.SetActive(true);
            }
            else
            {
                popup.SetActive(false);
            }
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
            athleteManager.SetAthleteByID(currentAthlete.id);
        }
    }
}
