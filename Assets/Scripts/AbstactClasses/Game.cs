using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public  class Game : MonoBehaviour
{
    
    public List<Player> Players = new List<Player>();
    [HideInInspector]
    public Match Match;
    [HideInInspector]
    public LevelGenarator LevelGenarator;
    [HideInInspector]
    public List<Asteroid> Asteroids = new List<Asteroid>();
    [HideInInspector]
    public List<Alien> Aliens = new List<Alien>();
    public string PlayerNickName;
    
    
    void Awake()
    {
        DataHolder.SetGame(this);
        Cursor.visible = false;
        StartMatch();
        Match.OnLost += Saver.SavePlayers;
        Match.OnLost += LoadMenu;
        
        
        
    }
    private void Start()
    {
        Players = Saver.LoadPlayers();
        LevelGenarator = GetComponent<LevelGenarator>();
        DataHolder.Factory.AddPlayer("Antony", 100000);
        DataHolder.Factory.AddPlayer("CrazyHead", 35000);
        DataHolder.Factory.AddPlayer("Losser", 1000);
        DataHolder.Factory.AddPlayer(PlayerNickName, 0);
        DataHolder.Factory.AddPlayer("The Rock", 50000);
        DataHolder.Factory.AddPlayer("CR7", 70000);
        Match.SetAppropriatePlayer();
        ResetPlayerScore();
    }

    void Update()
    {
        
    }

    private void ResetPlayerScore()
    {
        for(int i = 0;i < Players.Count; i++)
        {
            if(Match.Player.NickName == Players[i].NickName)
            {
                Match.Player.Score = 0;
                Players[i] = Match.Player;
                return;
            }
        }
    }
    public void StartMatch()
    {
        Match = gameObject.AddComponent<Match>();
    }

    public void LoadMenu()
    {
        StartCoroutine(LoadMenuCoroutine());
    }

    private IEnumerator LoadMenuCoroutine()
    {
        yield return new WaitForSeconds(DataHolder.actionWaitTime);
        Loader.LoadScene(Scenes.Menu);
    }

     
}
