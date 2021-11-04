using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Player 
{
    public int Score;
    public string NickName;
    public Player(string nickName,int score)
    {
        Score = score;
        NickName = nickName;
    }
}
