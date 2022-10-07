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
    public TMP_InputField selectedAthleteNameInputField;
    public TextMeshProUGUI selectedAthleteSport;
    public TextMeshProUGUI selectedAthleteStatus;
    public SpriteRenderer selectedAthleteSprite;

    [SerializeField] GameObject testRunButton;
    [SerializeField] GameObject testStopRunButton;

    AthleteManager AM;
    GameManager GM;


    void Start()
    {
        AM = FindObjectOfType<AthleteManager>();
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
        if (GM.newlyLoaded)
        {
            selectedAthleteNameInputField.text = AM.selectedAthlete.athleteName;
        }

        selectedAthleteName.text = AM.selectedAthlete.athleteName;
        selectedAthleteSport.text = $"Sport: {AM.selectedAthlete.sport}";
        selectedAthleteStatus.text = AM.selectedAthlete.active ? "Active" : "Asleep";

        if (AM.selectedAthlete.active)
        {
            selectedAthleteSprite.sprite = AM.athleteSprites[AM.selectedAthlete.spriteIDX];
        }
        else
        {
            selectedAthleteSprite.sprite = AM.sleepSprites[AM.selectedAthlete.sleepSpriteIDX];
        }

        for (int i = 0; i < AM.athletes.Count; i++)
        {
            sideBarPlaceholders[i].GetComponent<SidebarItem>().currentAthlete = AM.athletes[i];
        }

        if (AM.athletes[AM.selectedAthleteIDX].active)
        {
            testRunButton.SetActive(false);
            testStopRunButton.SetActive(true);
        }
        else
        {
            testRunButton.SetActive(true);
            testStopRunButton.SetActive(false);
        }
    }

    public void ToggleCurrentStatus() {
        AM.SetStatusByID(AM.selectedAthlete.id, !AM.selectedAthlete.active);
    }

    public void NameInputFieldChanged()
    {
        if (selectedAthleteNameInputField.text != "")
        {
            Debug.Log(selectedAthleteNameInputField.text);
            AM.selectedAthlete.athleteName = selectedAthleteNameInputField.text;
        }
    }

    public void StartActivity()
    {
        AM.StartActivity(8, Sport.running);
    }

    public void StopActivity()
    {
        AM.StopActivity();
    }
}
