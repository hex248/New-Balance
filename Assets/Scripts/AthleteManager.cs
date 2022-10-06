using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public enum Sport
{
    running,
    basketball,
    skateboarding
}

[Serializable]
public class Athlete
{
    public int id;
    public string athleteName;
    public Sport sport;
    public int iconIDX;
    public int spriteIDX;
    public int sleepSpriteIDX;

    public bool active = false;

    public Athlete(int id, string name, Sport sport, int iconIDX, int spriteIDX, int sleepSpriteIDX)
    {
        this.id = id;
        this.athleteName = name;
        this.sport = sport;
        this.iconIDX = iconIDX;
        this.spriteIDX = spriteIDX;
        this.sleepSpriteIDX = sleepSpriteIDX;
    }
}

public class AthleteManager : MonoBehaviour
{
    public List<Athlete> athletes = new List<Athlete>();
    public Athlete selectedAthlete;
    public int selectedAthleteIDX;

    public Sprite[] athleteIcons;
    public Sprite[] athleteSprites;
    public Sprite[] sleepSprites;

    GameManager GM;
    GameSaves GS;

    void Start()
    {
        GM = FindObjectOfType<GameManager>();
        GS = FindObjectOfType<GameSaves>();
        // athletes.Add(new Athlete(athletes.Count, "latif", Sport.running, athleteIcons[Random.Range(0, athleteIcons.Length)], athleteSprites[Random.Range(0, athleteSprites.Length)], sleepSprites[Random.Range(0, sleepSprites.Length)]));
        // athletes.Add(new Athlete(athletes.Count, "oliver", Sport.basketball, athleteIcons[Random.Range(0, athleteIcons.Length)], athleteSprites[Random.Range(0, athleteSprites.Length)], sleepSprites[Random.Range(0, sleepSprites.Length)]));
        // athletes.Add(new Athlete(athletes.Count, "trinity", Sport.skateboarding, athleteIcons[Random.Range(0, athleteIcons.Length)], athleteSprites[Random.Range(0, athleteSprites.Length)], sleepSprites[Random.Range(0, sleepSprites.Length)]));
        // athletes.Add(new Athlete(athletes.Count, "adelina", Sport.running, athleteIcons[Random.Range(0, athleteIcons.Length)], athleteSprites[Random.Range(0, athleteSprites.Length)], sleepSprites[Random.Range(0, sleepSprites.Length)]));
        // selectedAthlete = athletes[0];
        // Debug.Log(athletes[0].athleteName);
    }

    void Update()
    {
        CheckActivities();
    }

    void OnApplicationQuit()
    {
        GS.SaveAthletes(athletes);
    }

    public Athlete SetAthleteByID(int id)
    {
        Athlete newAthlete = athletes.Find(a => a.id == id);
        selectedAthlete = newAthlete;
        selectedAthleteIDX = id;
        return newAthlete;
    }
    
    public void SetStatusByID(int id, bool active)
    {
        int athleteIndex = athletes.FindIndex(a => a.id == id);
        athletes[athleteIndex].active = active;
    }

    void CheckActivities()
    {
        for (int i = 0; i < athletes.Count; i++)
        {
            Athlete a = athletes[i];
            if (a.active)
            {
                // check start time 
            }
        }
    }

    public bool SetAthletes(List<Athlete> savedAthletes = null)
    {
        if (savedAthletes != null)
        {
            athletes = savedAthletes;
            selectedAthlete = athletes[0];
            selectedAthleteIDX = 0;
            return true;
        }
        else
        {
            athletes.Add(new Athlete(athletes.Count,
                "give me a name",
                Sport.running,
                UnityEngine.Random.Range(0, athleteIcons.Length),
                UnityEngine.Random.Range(0, athleteSprites.Length),
                UnityEngine.Random.Range(0, sleepSprites.Length)
            ));
            selectedAthlete = athletes[0];
            selectedAthleteIDX = 0;
            GS.SaveAthletes(athletes);
            return true;
        }
    }
}
