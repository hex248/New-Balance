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
    public Activity activity;

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

[Serializable]
public class Activity
{
    public float startTimeMS;
    public float endTimeMS;

    public string name;
    public Sport sport;

    public Activity(float startTimeMS, float endTimeMS, string name, Sport sport)
    {
        this.startTimeMS = startTimeMS;
        this.endTimeMS = endTimeMS;
        this.name = name;
        this.sport = sport;
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

        // Debug.Log(DateTime.now);
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
        if (athletes[athleteIndex].active)
        {
            // ending exercise
            athletes[athleteIndex].activity = null;
        }
        athletes[athleteIndex].active = active;
    }

    void CheckActivities()
    {
        for (int i = 0; i < athletes.Count; i++)
        {
            Athlete a = athletes[i];
            if (a.active)
            {
                DateTime currentDate = new DateTime();
                float timeMS = Mathf.Abs((long)(currentDate - new DateTime(1970, 1, 1)).TotalMilliseconds);
                Debug.Log(timeMS);
                if (timeMS >= a.activity.endTimeMS)
                {
                    // check how long the activity was
                    float activityDuration = a.activity.endTimeMS - a.activity.startTimeMS;
                    athletes[i].active = false;
                }
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

    public void StartActivity(int hours, Sport sport)
    {
        SetStatusByID(selectedAthlete.id, true);

        // 1 game hour is 10 minutes in realtime
        Debug.Log(DateTime.Now);

        DateTime currentDate = new DateTime();
        float timeMS = Mathf.Abs((long)(currentDate - new DateTime(1970, 1, 1)).TotalMilliseconds);
        Debug.Log(timeMS);
        // 10 mins in ms is 600 000
        float taskDuration = 600000 * hours;
        float startTime = timeMS;
        float endTime = startTime + taskDuration;

        athletes[selectedAthleteIDX].activity = new Activity(startTime, endTime, $"{hours} hour(s) {sport}", sport);
    }

    public void StopActivity()
    {
        
    }
}
