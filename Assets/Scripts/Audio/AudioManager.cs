using System;
using System.Collections;
using Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    
    public Sound[] sounds;
    
    public static AudioManager Instance;

    [field:SerializeField]
    public float globalVolume { get; private set; } = 1f;
    
    [field:SerializeField]
    public float musicVolume { get; private set; } = 1f;
    
    [field:SerializeField]
    public float soundVolume { get; private set; } = 1f;
    
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
            s.source.volume = s.volume * globalVolume * (s.name == "GameLoop" ? musicVolume : soundVolume);
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }
    
    /// <summary>
    /// Play a given sound
    /// </summary>
    /// <param name="soundName">Name of the sound to play</param>
    /// <param name="easeInTime">if above 0, take some time to easy in the sound</param>
    public void Play(string soundName, float easeInTime = 0f)
    {
        Sound s = Array.Find(sounds, sound => sound.name == soundName);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        if (s.loop)
        {
            s.source.loop = true;
            s.source.Play();
        }
        else
        {
            s.source.loop = false;
            s.source.PlayOneShot(s.source.clip);
        }

        if (easeInTime > 0f)
        {
            StartCoroutine(EaseInSoundCoroutine(soundName, easeInTime));
        }
    }

    public void Play(SoundBank sound)
    {
        if(sound == SoundBank.None) return;
        
        Play(sound.ToString());
    }
    
    /// <summary>
    /// Coroutine to ease in the sound
    /// </summary>
    /// <param name="soundName">Name of the sound to ease in</param>
    /// <param name="easeInTime">Time to spend easing in the sounds</param>
    /// <returns>Coroutine stuff</returns>
    private IEnumerator EaseInSoundCoroutine(string soundName, float easeInTime)
    {
        Sound s = Array.Find(sounds, sound => sound.name == soundName);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            yield break;
        }
        
        float time = 0f;
        while (time < easeInTime)
        {
            s.source.volume = Mathf.Lerp(0f, s.volume * globalVolume, time / easeInTime);
            time += Time.deltaTime;
            yield return null;
        }
    }

    public void SetGlobalVolume(float volume)
    {
        globalVolume = volume;
        foreach (Sound s in sounds)
        {
            s.source.volume = s.volume * globalVolume;
        }
    }
    
    public void SetMusicVolume(float volume)
    {
        musicVolume = volume;
        foreach (Sound s in sounds)
        {
            if (s.name == "GameLoop")
            {
                s.source.volume = s.volume * globalVolume * musicVolume;
                break;
            }
        }
    }
    
    public void SetSoundVolume(float volume)
    {
        soundVolume = volume;
        foreach (Sound s in sounds)
        {
            if (s.name != "GameLoop")
            {
                s.source.volume = s.volume * globalVolume * soundVolume;
            }
        }
    }
}
