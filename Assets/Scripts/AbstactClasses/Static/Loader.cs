using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Scenes
{
    Menu,
    Space
};
public static class Loader 
{
    public static void LoadScene(Scenes scene)
    {
        SceneManager.LoadScene(scene.ToString()); 
    }
    
}
