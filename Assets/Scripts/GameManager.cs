using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameManager : MonoBehaviour
{
    public bool allDataLoaded = false;
    public bool newlyLoaded = false;
    public int framesNewlyLoaded = 0;
    public bool testMode = false;
    AthleteManager AM;
    PlayerManager PM;
    UIManager UIM;
    GameSaves GS;

    void Start()
    {
        PlayerPrefs.SetString("testMode", $"{testMode}".ToLower());
        bool athletesLoaded = false;
        bool playerLoaded = false;
        AM = FindObjectOfType<AthleteManager>();
        PM = FindObjectOfType<PlayerManager>();
        UIM = FindObjectOfType<UIManager>();
        GS = FindObjectOfType<GameSaves>();

        // load all data
        var athleteData = GS.LoadAthletes();
        if (athleteData != null)
        {
            athletesLoaded = AM.SetAthletes(athleteData.athletes);
        }
        else
        {
            athletesLoaded = AM.SetAthletes();
        }
        var playerData = GS.LoadPlayer();
        if (playerData != null)
        {
            playerLoaded = PM.SetPlayer(playerData.player);
        }
        else
        {
            playerLoaded = PM.SetPlayer();
        }
        if (athletesLoaded && playerLoaded)
        {
            allDataLoaded = true;
            newlyLoaded = true;
        }
    }
    void Update()
    {
        if (newlyLoaded)
        {
            framesNewlyLoaded++;
            if (framesNewlyLoaded > 1)
            {
                newlyLoaded = false;
            }
        }
    }

    public void LoadScene(string name)
    {
        SaveAll();
        SceneManager.LoadScene(name);
    }

    public void SaveAll()
    {
        GS.SavePlayer(PM.player);
        GS.SaveAthletes(AM.athletes);
    }
}