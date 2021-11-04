using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenarator : MonoBehaviour
{
    public int currentLevel { get; private set; }
    private int maxLevel = 4;
    private int asteroidsToGenerate = 0;
    private int aliensToGenerate = 0;
    private int intelligentAliensToGenerate = 0;
    private float generateAliensFrequency = 30;
    void Start()
    {
        currentLevel = 1;
        StartCoroutine(GenerateLevel());
    }

    void Update()
    {
        
    }
    private IEnumerator GenerateLevel()
    {
        yield return new WaitForSeconds(DataHolder.actionWaitTime);
        asteroidsToGenerate = currentLevel + maxLevel - 1;
        aliensToGenerate = currentLevel;
        intelligentAliensToGenerate = currentLevel + maxLevel - 6;
        GenerateActorsOnLevel();
    }
    public void GenerateActorsOnLevel()
    {
        GenerateAsteroids();
        StartCoroutine(GenerateAliens());

    }
    private void GenerateAsteroids()
    {
        int counter = asteroidsToGenerate;
        for(int i = 0;i < counter; i++)
        {
            DataHolder.Factory.AddAsteroid(GetRandomEdgePosition(), 0);
            asteroidsToGenerate--;
        }
    }

    private Vector3 GetRandomEdgePosition()
    {
        float XScreenPos;
        float YScreenPos;
        float verticalEdgeSpawn = Random.value;
        if (verticalEdgeSpawn > 0.5)
        {
            float leftEdge = Random.value;
            if (leftEdge > 0.5) XScreenPos = 0;
            else XScreenPos = DataHolder.MainCamera.pixelWidth;
            YScreenPos = Random.RandomRange(0, DataHolder.MainCamera.pixelHeight);
        }
        else
        {
            float downEdge = Random.value;
            if (downEdge > 0.5) YScreenPos = 0;
            else YScreenPos = DataHolder.MainCamera.pixelHeight;
            XScreenPos = Random.RandomRange(0, DataHolder.MainCamera.pixelWidth);
        }
        Vector3 Position = DataHolder.MainCamera.ScreenToWorldPoint(new Vector3(XScreenPos, YScreenPos, 0));
        Position = new Vector3(Position.x, Position.y, 0);
        return Position;
    }
    public void TryToGoToNextLevel()
    {
         if(NoObjectsToGenerate() && NoAimOnScene())
        {
            currentLevel++;
            StartCoroutine(GenerateLevel());
            if (currentLevel > maxLevel) DataHolder.Game.Match.OnWon();
        }
        else
        {
            if(DataHolder.Game.Asteroids.Count == 0)
            {
                for(int i = 0;i < aliensToGenerate; i++)
                {
                    GenerateAlien(true, false);
                }
                for (int i = 0; i < intelligentAliensToGenerate; i++)
                {
                    GenerateAlien(true, true);
                }
            }
        }
    }

    private bool NoObjectsToGenerate()
    {
        bool b = asteroidsToGenerate <= 0 && aliensToGenerate <= 0 && intelligentAliensToGenerate <= 0;
        return b;
    }

    private bool NoAimOnScene()
    {
        bool b = DataHolder.Game.Asteroids.Count == 0 && DataHolder.Game.Aliens.Count == 0;
        return b;
    }

    private void GenerateAlien(bool Certain,bool intelligent)
    {
        float chanceToGenerate = Random.Range(0, 100);
        if(chanceToGenerate <= 30 || Certain)
        {
            if (intelligent) intelligentAliensToGenerate--;
            else aliensToGenerate--;
            DataHolder.Factory.AddNewAlien(GetRandomEdgePosition(), currentLevel, intelligent);
           
        }
        
    }

    private IEnumerator GenerateAliens()
    {
        if (aliensToGenerate > 0) GenerateAlien(false, false);
        if (intelligentAliensToGenerate > 0) GenerateAlien(false, true);
        yield return new WaitForSecondsRealtime(DataHolder.actionWaitTime);
        StartCoroutine(GenerateAliens());
    }

    

    
}
