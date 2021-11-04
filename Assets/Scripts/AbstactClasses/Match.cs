using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Match : MonoBehaviour
{
    public Player Player;
    public delegate void matchAction();
    public matchAction OnWon;
    public matchAction OnLost;
    //public matchAction OnWon;
    void Start()
    {
    }

    public void SetAppropriatePlayer()
    {
        for (int i = 0; i < DataHolder.Game.Players.Count; i++)
        {
            if (DataHolder.Game.Players[i].NickName == DataHolder.Game.PlayerNickName)
            {
                Player = DataHolder.Game.Players[i];
                return;
            }
        }

        throw new Exception();
    }

    void Update()
    {

    }

    public void UpdateScore(int increment)
    {
        Player.Score += increment;
        for (int i = 0; i < DataHolder.Game.Players.Count; i++)
        {
            Player player = DataHolder.Game.Players[i]; 
              if (player.NickName == Player.NickName)
            {
                DataHolder.Game.Players[i] = Player;
            }
            DataHolder.UIWorldHandler.UpdateScoreTest();
        }

    }


}
