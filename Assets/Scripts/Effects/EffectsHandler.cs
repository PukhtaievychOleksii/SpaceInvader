using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsHandler : MonoBehaviour
{
    public AudioClip LaserSound;
    public AudioClip BlowSound;
    public  AudioClip CrashSound;
    public  AudioClip RocketSound;
    public  GameObject ExplosionPrefab;
    List<AudioSource> AudioSources = new List<AudioSource>();
    private Animator explosionAnimator;
    private bool sound;
    private float volume =0.2f;
    
    void Start()
    {
        DataHolder.SetEffectsHandler(this);
        sound = Saver.LoadSound();
    }

    // Update is called once per frame
    
    /*public void PlayLaser()
    {
        AudioSource.clip = LaserSound;
        AudioSource.volume = 0.2f;

        if (sound) AudioSource.Play();
    }

    public void PlayCrash()
    {
        AudioSource.clip = CrashSound;
        AudioSource.volume = 0.2f;
        if (sound) AudioSource.Play();
    }
    
    public void StartPlayingRocketSound()
    {
        AudioSource.clip = RocketSound;
        
        AudioSource.volume = 0.2f;
        if (sound) AudioSource.Play();
    }*/

    /*public void StopRocketSound(){
        AudioSource.Stop();
        AudioSource.loop = false;
    }

   */ public void MakeExplosion(Vector3 position)
    {
        explosionAnimator =Instantiate(ExplosionPrefab, position, Quaternion.identity).GetComponentInChildren<Animator>();
        PlaySound(BlowSound);
    }

    public void PlaySound(AudioClip audioClip)
    {
        AudioSource audioSource;
        if (AudioSources.Count == 0) AudioSources.Add(gameObject.AddComponent<AudioSource>());
        AudioSource lastSource = AudioSources[AudioSources.Count - 1];
        if (lastSource.clip != null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        } else audioSource = lastSource;
        audioSource.volume = volume;
        audioSource.clip = audioClip;
        AudioSources.Add(audioSource);
        if (sound) audioSource.Play();
    }
}
