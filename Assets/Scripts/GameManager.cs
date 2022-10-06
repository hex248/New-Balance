using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameManager : MonoBehaviour
{
    public bool allDataLoaded;
    AthleteManager AM;
    UIManager UIM;
    GameSaves GS;

    void Start()
    {
        AM = FindObjectOfType<AthleteManager>();
        UIM = FindObjectOfType<UIManager>();
        GS = FindObjectOfType<GameSaves>();

        // load all data
        var athleteData = GS.LoadAthletes();
        if (athleteData != null) {
            AM.SetAthletes(athleteData.athletes);
        }
        else {
            AM.SetAthletes();
        }
    }
}