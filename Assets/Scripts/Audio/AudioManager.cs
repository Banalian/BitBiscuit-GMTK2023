using System;
using Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    
    public Sound[] sounds;
    
    public static AudioManager Instance;

    [field:SerializeField]
    public float globalVolume { get; private set; } = 1f;
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        
        DontDestroyOnLoad(gameObject);
        
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume * globalVolume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }
    
    public void Play(string soundName)
    {
        Sound s = Array.Find(sounds, sound => sound.name == soundName);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.PlayOneShot(s.source.clip);
    }

    public void Play(SoundBank sound)
    {
        if(sound == SoundBank.None) return;
        
        Play(sound.ToString());
    }

    public void SetGlobalVolume(float volume)
    {
        globalVolume = volume;
        foreach (Sound s in sounds)
        {
            s.source.volume = s.volume * globalVolume;
        }
    }
}
