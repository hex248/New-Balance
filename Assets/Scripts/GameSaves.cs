using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class GameSaves : MonoBehaviour
{
    string athleteFilePath;
    string playerFilePath;

    void Start()
    {
        athleteFilePath = Application.persistentDataPath + "/athletes.BIGBOOT";
        playerFilePath = Application.persistentDataPath + "/player.BIGBOOT";
    }

    public void SaveAthletes(List<Athlete> toSave)
    {
        BinaryFormatter bf = new BinaryFormatter(); 
        FileStream file = File.Create(athleteFilePath); 
        AthleteSave data = new AthleteSave();
        data.athletes = toSave;
        bf.Serialize(file, data);
        file.Close();
    }

    public void SavePlayer(Player toSave)
    {
        BinaryFormatter bf = new BinaryFormatter(); 
        FileStream file = File.Create(playerFilePath); 
        PlayerSave data = new PlayerSave();
        data.player = toSave;
        bf.Serialize(file, data);
        file.Close();
    }

    public AthleteSave LoadAthletes()
    {
        if (File.Exists(athleteFilePath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(athleteFilePath, FileMode.Open);
            AthleteSave data = (AthleteSave)bf.Deserialize(file);
            file.Close();
            return data;
        }
        else
        {
            return null;
        }
    }

    public PlayerSave LoadPlayer()
    {
        if (File.Exists(playerFilePath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(playerFilePath, FileMode.Open);
            PlayerSave data = (PlayerSave)bf.Deserialize(file);
            file.Close();
            return data;
        }
        else
        {
            return null;
        }
    }
}

[Serializable]
public class AthleteSave
{
    public List<Athlete> athletes;
}

[Serializable]
public class PlayerSave
{
    public Player player;
}