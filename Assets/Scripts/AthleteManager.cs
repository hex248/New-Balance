using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

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
    
    public int[] hairRGB;
    public int[] skinRGB;
    public int[] bodyRGB;
    public int[] feetRGB;

    public string shoes;

    public bool active = false;

    public Athlete(int id, string name, Sport sport, int iconIDX, int spriteIDX, int sleepSpriteIDX)
    {
        this.id = id;
        this.athleteName = name;
        this.sport = sport;
        this.iconIDX = iconIDX;
        this.spriteIDX = spriteIDX;
        this.sleepSpriteIDX = sleepSpriteIDX;
        this.hairRGB = new int[] {8,5,4};
        this.skinRGB = new int[] {230,181,148};
        this.bodyRGB = new int[] {255,255,255};
        this.feetRGB = new int[] {255,255,255};
        this.shoes = "Default";
    }
}

[Serializable]
public class Activity
{
    public long startUnix;
    public long endUnix;

    public string name;
    public Sport sport;

    public Activity(long startUnix, long endUnix, string name, Sport sport)
    {
        this.startUnix = startUnix;
        this.endUnix = endUnix;
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
    PlayerManager PM;

    [SerializeField] ProgressBar activityProgressBar;

    void Start()
    {
        GM = FindObjectOfType<GameManager>();
        GS = FindObjectOfType<GameSaves>();
        PM = FindObjectOfType<PlayerManager>();
    }
    void Awake()
    {
        StartCoroutine(CheckActivities());
    }

    void Update()
    {
        if (!GM.allDataLoaded)
        {
            return;
        }
        UpdateActivityProgressBar();
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

    IEnumerator CheckActivities()
    {
        for(;;)
        {
            for (int i = 0; i < athletes.Count; i++)
            {
                Athlete a = athletes[i];
                if (a.active)
                {
                    long unixTime = GetUnixTime();
                    if (unixTime >= a.activity.endUnix)
                    {
                        // check how long the activity was
                        long activityDuration = a.activity.endUnix - a.activity.startUnix;
                        // 60 second realtime = 1 credit & 1 xp
                        // calculate rewards
                        float creditMultiplier = 1.0f;
                        int credits = (int)Mathf.Round((activityDuration / 60) * creditMultiplier);
                        float xpMultiplier = 1f;
                        int xp = (int)Mathf.Round((activityDuration / 60) * xpMultiplier);

                        PM.AddCredits(credits);
                        PM.AddXP(xp);

                        athletes[i].active = false;
                    }
                }
            }
            yield return new WaitForSeconds(1.0f);
        }
    }

    public void UpdateActivityProgressBar()
    {
        if (!athletes[selectedAthleteIDX].active) return;
        if (activityProgressBar == null) return;

        long unixTime = GetUnixTime();
        
        Activity currentActivity = athletes[selectedAthleteIDX].activity;
        activityProgressBar.maximum = currentActivity.endUnix-currentActivity.startUnix; // activity length
        activityProgressBar.current = unixTime-currentActivity.startUnix; // time since start of activity
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

        // 1 game hour is 10 minutes in realtime (temporarily 1 minute)

        long unixTime = GetUnixTime();

        // 10 mins in s is 600
        long taskDuration = 3600 * hours; //!CHANGE TO 600 WHEN DONE TESTING
        long startTime = unixTime;
        long endTime = startTime + taskDuration;

        athletes[selectedAthleteIDX].activity = new Activity(startTime, endTime, $"{hours} hour(s) {sport}", sport);
    }

    public void StopActivity()
    {
        SetStatusByID(selectedAthlete.id, false);

        athletes[selectedAthleteIDX].activity = null;
    }

    long GetUnixTime()
    {
        DateTime time = DateTime.Now;
        long unixTime = ((DateTimeOffset)time).ToUnixTimeSeconds();
        return unixTime;
    }
}

