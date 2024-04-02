using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public static AudioManager Instance { get { return instance; } }



    public static Dictionary<string, AudioClip> audioLibrary = new Dictionary<string, AudioClip>();
    private AudioSource sfxSource;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

        sfxSource = gameObject.AddComponent<AudioSource>();
    }

    public void PlaySFX(string path, float volume = 0.3f)
    {

        AudioClip clip = GetAudioClip(path);
        if (clip == null) { Debug.LogWarning("Music: " + path + " not found!"); return; }
        sfxSource.volume = volume;
        sfxSource.PlayOneShot(clip);
    }

    public static AudioClip GetAudioClip(string key)
    {
        if (audioLibrary.ContainsKey(key))
        {
            return audioLibrary[key];
        }
        else
        {
            AudioClip audioClip = Resources.Load<AudioClip>(key);
            audioLibrary.Add(key, audioClip);
            return audioClip;
        }
    }

}



