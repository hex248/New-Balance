using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public int credits;
    public int xp;
    public int xpNeeded;
    public int level;
}

public class PlayerManager : MonoBehaviour
{
    Player player;

    GameManager GM;
    GameSaves GS;

    void Start()
    {
        GM = FindObjectOfType<GameManager>();
        GS = FindObjectOfType<GameSaves>();
    }

    public void SetPlayer(Player savedPlayer = null)
    {
        if (savedPlayer != null)
        {
            player = savedPlayer;
            
        }
    }
}
