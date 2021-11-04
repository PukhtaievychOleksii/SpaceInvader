using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundButton : MonoBehaviour
{
    private Image image; 
    public Sprite SoundOn;
    public Sprite SoundOf;
    public AudioSource AudioSource;
    public AudioClip Track;
    public bool Sound;
    void Awake()
    {
        GameObject child = transform.GetChild(0).gameObject;
        image = child.GetComponent<Image>();
        
    }
    private void Start()
    {
        AudioSource.clip = Track;
        AudioSource.Play();
        Sound = Saver.LoadSound();
        if (Sound) image.sprite = SoundOn;
        else
        {
            image.sprite = SoundOf;
            AudioSource.Stop();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateSoundButton()
    {
        if (Sound)
        {
            image.sprite = SoundOf;
            Sound = false;
            AudioSource.Stop();
        }
        else
        {
            image.sprite = SoundOn;
            Sound = true;
            AudioSource.Play();
        } 
    }

    
}
