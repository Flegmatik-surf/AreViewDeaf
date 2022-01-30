using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundAssets : MonoBehaviour
{
    public static SoundAssets instance;

    public SoundAudioClip[] soundAudioClipsArray;

    [System.Serializable]
    public class SoundAudioClip
    {
        public AudioMixer sfxMixer;
        public SoundManager.Sound sound;
        public AudioClip audioClip;
    }

    public void Awake()
    {
        if (instance)
        {
            Debug.Log("Il y a déjà une instance de SoundManager : Autodestruction lancée ");
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

}
