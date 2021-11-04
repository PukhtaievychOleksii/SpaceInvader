using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Saver 
{
   

    public static  void SavePlayers()
    {
        int Count = DataHolder.Game.Players.Count;
        if(Count > 0) PlayerPrefs.SetInt("PlayersCount", Count);

        for (int i = 0;i < Count; i++)
        {
            string keyName = "Player" + (i + 1).ToString() + "Name";
            string keyScore = "Player" + (i + 1).ToString() + "Score";

            if (!PlayerPrefs.HasKey(keyName))
            {
                PlayerPrefs.SetString(keyName, DataHolder.Game.Players[i].NickName);
                PlayerPrefs.SetInt(keyScore, DataHolder.Game.Players[i].Score);
            }
            else
            {
                if(DataHolder.Game.Players[i].NickName == DataHolder.Game.PlayerNickName)
                {
                    int score1 = PlayerPrefs.GetInt(keyScore);
                    int score2 = DataHolder.Game.Players[i].Score;
                    if (score2 > score1)
                    {
                        PlayerPrefs.SetInt(keyScore, DataHolder.Game.Players[i].Score);
                    }
                } 
                
            }
            
        }

    }

    public static List<Player> LoadPlayers()
    {
        List<Player> Players = new List<Player>();
        int Count = PlayerPrefs.GetInt("PlayersCount");
        for(int i = 0;i < Count; i++)
        {
            string NickName = PlayerPrefs.GetString("Player" + (i + 1).ToString() + "Name");
            int Score = PlayerPrefs.GetInt("Player" + (i + 1).ToString() + "Score");
            Player player = new Player(NickName, Score);
            if (!Players.Contains(player)) Players.Add(player);
        }
        return Players;
    }

    public static void SortPlayersByScore(ref List<Player> Players)
    {
        for (int i = 0; i < Players.Count; i++)
        {
            int next = i + 1;
            int prev = i - 1;
            if (next > Players.Count - 1) next = i;
            if (prev < 0) prev = i;
            if (Players[i].Score < Players[next].Score)
            {
                Player tmp = Players[i];
                Players[i] = Players[next];
                Players[next] = tmp;
                i = 0;
            }
            if (Players[i].Score > Players[prev].Score)
            {
                Player tmp = Players[i];
                Players[i] = Players[prev];
                Players[prev] = tmp;
                i = 0;
            }
        }
    }

    public static void SaveSound(bool sound)
    {
        float value;
        if (sound) value = 1;
        else value = 0;
        PlayerPrefs.SetFloat("Sound", value);
    }

    public static bool LoadSound()
    {
        float value = PlayerPrefs.GetFloat("Sound");
        if (value == 1) return true;
        return false;
    }

}
