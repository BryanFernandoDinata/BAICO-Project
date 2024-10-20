using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;

public class AudioManager : MonoBehaviour 
{
    public static AudioManager instance;
    public AudioSource[] sfx;
    public AudioSource[] bgm;

    // Use this for initialization
    void Start () 
    {
        instance = this;

        DontDestroyOnLoad(this.gameObject);

        if(PlayerPrefs.HasKey("Mute"))
        {
            if(PlayerPrefs.GetInt("Mute") == 1)
            {
                Mute();
            }else
            {
                
            }
        }
	}
	
    public void PlaySFX(int soundToPlay)
    {
        if (soundToPlay < sfx.Length)
        {
            sfx[soundToPlay].Play();
        }
    }

    public void PlayBGM(int musicToPlay)
    {
        if (!bgm[musicToPlay].isPlaying)
        {
            StopMusic();

            if (musicToPlay < bgm.Length)
            {
                bgm[musicToPlay].Play();
            }
        }
    }

    public void StopMusic()
    {
        for(int i = 0; i < bgm.Length; i++)
        {
            bgm[i].Stop();
        }
    }
    public void Mute()
    {
        foreach(AudioSource a in bgm)
        {
            a.volume = 0;
        }
        foreach(AudioSource a in sfx)
        {
            a.volume = 0;
        }
    }
    public void UnMute()
    {
        foreach(AudioSource a in bgm)
        {
            a.volume = 0.2f;
        }
        foreach(AudioSource a in sfx)
        {
            a.volume = 1;
        }
    }
}
