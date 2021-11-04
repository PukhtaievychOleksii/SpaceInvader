using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public SoundButton SoundButton;
    // Start is called before the first frame update
    void Awake()
    {
       SoundButton.Sound = Saver.LoadSound();
    }

    private void Start()
    {
        SetCursor();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadSpace()
    {
        Saver.SaveSound(SoundButton.Sound);
        Loader.LoadScene(Scenes.Space);
    }

    public void Quit()
    {
        Saver.SaveSound(SoundButton.Sound);
        //UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }

    private void SetCursor()
    {
        Cursor.visible = true;
        Cursor.SetCursor(DataHolder.UIMenuHandler.CursorTexture, Vector2.zero, CursorMode.ForceSoftware);
        
    }
}
