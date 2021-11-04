using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataHolder
{
    public static float actionWaitTime = 3f;

    public static Game Game { get; private set; }

    public static void SetGame(Game game)
    {
        Game = game;
    }

    public static Camera MainCamera { get; private set;}
    public static void SetMainCamera(Camera camera)
    {
        MainCamera = camera;
    }

    public static Factory Factory { get; private set; }

    public static void SetFactory(Factory factory)
    {
        Factory = factory;
    }

    public static int NumberOfAsteroidTypes { get; private set;}
    public static void SetNumbeOfAsteroidTypes(int number)
    {
        NumberOfAsteroidTypes = number;
    }

    public static EffectsHandler EffectsHandler { get; private set; }

    public static void SetEffectsHandler(EffectsHandler effectsHandler)
    {
        EffectsHandler = effectsHandler;
    }

    public static UIWorldHandler UIWorldHandler { get; private set; }
    public static void SetUIWorldHandler(UIWorldHandler UIHandler)
    {
        UIWorldHandler = UIHandler;
    }

    public static SpaceShip SpaceShip { get; private set;}
    public static void SetSpaceShip(SpaceShip spaceShip)
    {
        SpaceShip = spaceShip;
    }


    public static UIMenuHandler UIMenuHandler { get; private set; }
    public static void SetUIMenuHandler(UIMenuHandler UIHandler)
    {
        UIMenuHandler = UIHandler;
    }
    
}
