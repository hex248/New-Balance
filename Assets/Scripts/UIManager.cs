using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum ButtonType {
    shop,
    athletes,
    sponsors,
    leaderboard
}

public class UIManager : MonoBehaviour
{
    [SerializeField] Image[] sideBarPlaceholders;
    
    public TextMeshProUGUI selectedAthleteName;
    public TextMeshProUGUI selectedAthleteSport;
    public TextMeshProUGUI selectedAthleteStatus;
    public SpriteRenderer selectedAthleteSprite;

    AthleteManager athleteManager;
    GameManager GM;

    void Start()
    {
        athleteManager = FindObjectOfType<AthleteManager>();
        GM = FindObjectOfType<GameManager>();
    }

    public void UIButtonPress(int num)
    {
        ButtonType buttonType = (ButtonType)num;
        Debug.Log(buttonType);
    }

    private void LateUpdate()
    {
        if (!GM.allDataLoaded)
        {
            return;
        }
        selectedAthleteName.text = athleteManager.selectedAthlete.athleteName;
        selectedAthleteSport.text = $"Sport: {athleteManager.selectedAthlete.sport}";
        selectedAthleteStatus.text = athleteManager.selectedAthlete.active ? "Active" : "Asleep";

        if (athleteManager.selectedAthlete.active)
        {
            selectedAthleteSprite.sprite = athleteManager.selectedAthlete.sprite;
        }
        else
        {
            selectedAthleteSprite.sprite = athleteManager.selectedAthlete.sleepSprite;
        }

        for (int i = 0; i < athleteManager.athletes.Count; i++)
        {
            sideBarPlaceholders[i].GetComponent<SidebarItem>().currentAthlete = athleteManager.athletes[i];
        }
    }

    public void ToggleCurrentStatus() {
        athleteManager.SetStatusByID(athleteManager.selectedAthlete.id, !athleteManager.selectedAthlete.active);
    }
}
