using UnityEngine.Audio;
using System;
using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;

    public float BGMFadeInTime;
    public float BGMFadeOutTime;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        // DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.audioSource = gameObject.AddComponent<AudioSource>();
            s.audioSource.clip = s.clip;
            s.audioSource.volume = s.volume;
            s.audioSource.pitch = s.pitch;
            s.audioSource.loop = s.loop;
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning(string.Format("Sound '{0}' was not found.", name));
            return;
        }
        s.audioSource.Play();
    }

    public IEnumerator PlayBGM(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning(string.Format("Sound '{0}' was not found.", name));
            yield return null;
        }


        float timeToFade = BGMFadeInTime;
        float timeElapsed = 0;

        while (timeElapsed < timeToFade)
        {
            s.audioSource.volume = Mathf.Lerp(0, 1, timeElapsed / timeToFade);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        s.audioSource.Play();
    }

    public IEnumerator StopBGM()
    {
        float timeToFade = BGMFadeOutTime;
        float timeElapsed = 0;

        foreach (Sound s in sounds)
        {
            if (s.audioSource != null && s.isBGM && s.audioSource.isPlaying)
            {
                while (timeElapsed < timeToFade)
                {
                    s.audioSource.volume = Mathf.Lerp(1, 0, timeElapsed / timeToFade);
                    timeElapsed += Time.deltaTime;
                    yield return null;
                }
                s.audioSource.Stop();
            }
        }
    }
}
