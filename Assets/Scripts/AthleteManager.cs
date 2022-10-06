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
    public Sprite icon;
    public Sprite sprite;
    public Sprite sleepSprite;

    public bool active = false;

    public Athlete(int id, string name, Sport sport, Sprite icon, Sprite sprite, Sprite sleepSprite)
    {
        this.id = id;
        this.athleteName = name;
        this.sport = sport;
        this.icon = icon;
        this.sprite = sprite;
        this.sleepSprite = sleepSprite;
    }
}

public class AthleteManager : MonoBehaviour
{
    public List<Athlete> athletes = new List<Athlete>();
    public Athlete selectedAthlete;

    [SerializeField] Sprite[] athleteIcons;
    [SerializeField] Sprite[] athleteSprites;
    [SerializeField] Sprite[] sleepSprites;

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

    public Athlete SetAthleteByID(int id)
    {
        Athlete newAthlete = athletes.Find(a => a.id == id);
        selectedAthlete = newAthlete;
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

    public void SetAthletes(List<Athlete> savedAthletes = null)
    {
        if (savedAthletes != null)
        {
            athletes = savedAthletes;
            selectedAthlete = athletes[0];
        }
        else
        {
            athletes.Add(new Athlete(athletes.Count,
                "campbell",
                Sport.running,
                athleteIcons[Random.Range(0, athleteIcons.Length)],
                athleteSprites[Random.Range(0, athleteSprites.Length)],
                sleepSprites[Random.Range(0, sleepSprites.Length)]));
            selectedAthlete = athletes[0];
            GS.SaveAthletes(athletes);
        }
    }
}
