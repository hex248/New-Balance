using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public enum ButtonType {
    shop,
    customisation,
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

    [SerializeField] GameObject testRunButton;
    [SerializeField] GameObject testStopRunButton;
    [SerializeField] GameObject progressBar;

    [SerializeField] ProgressBar levelProgress;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI creditText;

    AthleteManager AM;
    PlayerManager PM;
    GameManager GM;


    void Start()
    {
        AM = FindObjectOfType<AthleteManager>();
        PM = FindObjectOfType<PlayerManager>();
        GM = FindObjectOfType<GameManager>();
    }

    public void UIButtonPress(string buttonType)
    {
        if (buttonType == "customise")
        {
            SceneManager.LoadScene("Customise");
        }
        else if (buttonType == "home")
        {
            SceneManager.LoadScene("Main");
        }
    }

    private void LateUpdate()
    {
        if (!GM.allDataLoaded)
        {
            return;
        }

        if (SceneManager.GetActiveScene().name == "Main")
        {
            if (GM.newlyLoaded)
            {
                selectedAthleteNameInputField.text = AM.selectedAthlete.athleteName;
            }
            selectedAthleteName.text = AM.selectedAthlete.athleteName;
            selectedAthleteSport.text = $"Sport: {AM.selectedAthlete.sport}";
            selectedAthleteStatus.text = AM.selectedAthlete.active ? "Active" : "Asleep";

            // if (AM.selectedAthlete.active)
            // {
            //     selectedAthleteSprite.sprite = AM.athleteSprites[AM.selectedAthlete.spriteIDX];
            // }
            // else
            // {
            //     selectedAthleteSprite.sprite = AM.sleepSprites[AM.selectedAthlete.sleepSpriteIDX];
            // }

            for (int i = 0; i < AM.athletes.Count; i++)
            {
                sideBarPlaceholders[i].GetComponent<SidebarItem>().currentAthlete = AM.athletes[i];
            }

            if (AM.athletes[AM.selectedAthleteIDX].active)
            {
                testRunButton.SetActive(false);
                testStopRunButton.SetActive(true);
                progressBar.SetActive(true);
            }
            else
            {
                testRunButton.SetActive(true);
                testStopRunButton.SetActive(false);
                progressBar.SetActive(false);
            }

            levelProgress.maximum = PM.player.xpNeeded;
            levelProgress.current = PM.player.xp;
            levelText.text = $"LVL {PM.player.level}";
            creditText.text = $"Credits: {PM.player.credits}";
        }
        else if (SceneManager.GetActiveScene().name == "Customise")
        {
            if (GM.newlyLoaded)
            {
                selectedAthleteNameInputField.text = AM.selectedAthlete.athleteName;
            }
            
            for (int i = 0; i < AM.athletes.Count; i++)
            {
                sideBarPlaceholders[i].GetComponent<SidebarItem>().currentAthlete = AM.athletes[i];
            }

            levelProgress.maximum = PM.player.xpNeeded;
            levelProgress.current = PM.player.xp;
            levelText.text = $"LVL {PM.player.level}";
            creditText.text = $"Credits: {PM.player.credits}";
        }
    }

    public void ToggleCurrentStatus() {
        AM.SetStatusByID(AM.selectedAthlete.id, !AM.selectedAthlete.active);
    }

    public void NameInputFieldChanged()
    {
        if (selectedAthleteNameInputField.text != "")
        {
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
