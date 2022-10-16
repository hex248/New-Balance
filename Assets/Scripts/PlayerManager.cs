using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

[Serializable]
public class Player
{
    public int credits;
    public int xp;
    public int xpNeeded;
    public int level;
    public List<string> purchasedClothes = new List<string>();

    public Player(int credits, int xp, int xpNeeded, int level)
    {
        this.credits = credits;
        this.xp = xp;
        this.xpNeeded = xpNeeded;
        this.level = level;
    }
}

public class PlayerManager : MonoBehaviour
{
    public Player player;

    GameManager GM;
    GameSaves GS;

    void Start()
    {
        GM = FindObjectOfType<GameManager>();
        GS = FindObjectOfType<GameSaves>();
    }

    void OnApplicationQuit()
    {
        GS.SavePlayer(player);
    }

    public bool SetPlayer(Player savedPlayer = null)
    {
        if (savedPlayer != null)
        {
            player = savedPlayer;
            return true;
        }
        else
        {
            player = new Player(0,0,100,1);
            GS.SavePlayer(player);
            return true;
        }
    }

    public void AddCredits(int amount)
    {
        StartCoroutine(GraduallyIncreaseCredits(player.credits+amount, 0.01f));
    }

    IEnumerator GraduallyIncreaseCredits(int target, float speed)
    {
        while(player.credits < target)
        {
            player.credits++;
            yield return new WaitForSeconds(speed);
        }
    }

    public void AddXP(int amount)
    {
        StartCoroutine(GraduallyIncreaseXP(player.xp+amount, 0.01f));
    }

    IEnumerator GraduallyIncreaseXP(int target, float speed)
    {
        while(player.xp < target)
        {
            player.xp++;
            if (player.xp >= player.xpNeeded)
            {
                player.xp -= player.xpNeeded;
                player.level++;

                player.xpNeeded = (int)Mathf.Round(player.xpNeeded * 1.5f); // exponential xp system
            }
            yield return new WaitForSeconds(speed);
        }
    }
}
