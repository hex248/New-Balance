using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameManager : MonoBehaviour
{
    public bool allDataLoaded = false;
    public bool newlyLoaded = false;
    public int framesNewlyLoaded = 0;
    AthleteManager AM;
    UIManager UIM;
    GameSaves GS;

    void Start()
    {
        bool athletesLoaded = false;
        AM = FindObjectOfType<AthleteManager>();
        UIM = FindObjectOfType<UIManager>();
        GS = FindObjectOfType<GameSaves>();

        // load all data
        var athleteData = GS.LoadAthletes();
        if (athleteData != null) {
            athletesLoaded = AM.SetAthletes(athleteData.athletes);
        }
        else
        {
            athletesLoaded = AM.SetAthletes();
        }
        if (athletesLoaded)
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
}