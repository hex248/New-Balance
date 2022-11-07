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
    [SerializeField] SidebarItem[] sideBarItems;
    
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

    [SerializeField] GameObject[] variableElements;

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
        if (buttonType == "home")
        {
            GM.LoadScene("Main");
        }
        else if (buttonType == "customise")
        {
            GM.LoadScene("Customise");
        }
        else if (buttonType == "shop")
        {
            GM.LoadScene("Shop");
        }
    }

    private void Update()
    {
        if (!GM.allDataLoaded)
        {
            HideDynamic();
            return;
        }
        
        if (GM.newlyLoaded)
        {
            ShowDynamic();
            if (selectedAthleteNameInputField != null) selectedAthleteNameInputField.text = AM.selectedAthlete.athleteName;
        }
        

        if (SceneManager.GetActiveScene().name == "Main")
        {
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
                sideBarItems[i].currentAthlete = AM.athletes[i];
            }

            if (AM.athletes[AM.selectedAthleteIDX].active)
            {
                testRunButton.SetActive(false);
                testStopRunButton.SetActive(true);
                if (progressBar != null) progressBar.SetActive(true);
            }
            else
            {
                testRunButton.SetActive(true);
                testStopRunButton.SetActive(false);
                if (progressBar != null) progressBar.SetActive(false);
            }
        }
        else if (SceneManager.GetActiveScene().name == "Customise")
        {
            for (int i = 0; i < AM.athletes.Count; i++)
            {
                sideBarItems[i].currentAthlete = AM.athletes[i];
            }

            if (AM.athletes[AM.selectedAthleteIDX].active)
            {
                if (progressBar != null) progressBar.SetActive(true);
            }
            else
            {
                if (progressBar != null) progressBar.SetActive(false);
            }
        }
        else if (SceneManager.GetActiveScene().name == "Shop")
        {
            for (int i = 0; i < AM.athletes.Count; i++)
            {
                sideBarItems[i].currentAthlete = AM.athletes[i];
            }

            if (AM.athletes[AM.selectedAthleteIDX].active)
            {
                if (progressBar != null) progressBar.SetActive(true);
            }
            else
            {
                if (progressBar != null) progressBar.SetActive(false);
            }
        }

        levelProgress.maximum = PM.player.xpNeeded;
        levelProgress.current = PM.player.xp;
        levelText.text = $"LVL {PM.player.level}";
        creditText.text = $"{PM.player.credits}";
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
        AM.StartActivity(4, Sport.running);
    }

    public void StopActivity()
    {
        AM.StopActivity();
    }

    void HideDynamic()
    {
        for (int i = 0; i < variableElements.Length; i++)
        {
            variableElements[i].SetActive(false);
        }
    }

    void ShowDynamic()
    {
        for (int i = 0; i < variableElements.Length; i++)
        {
            variableElements[i].SetActive(true);
        }
    }
}
