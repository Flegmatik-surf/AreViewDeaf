using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public static class SoundManager
{
    public enum Sound
    {
        //voices


        //Sound Effects
        Sand_1, Sand_2, Sand_3, Donjon_1, Donjon_2, Donjon_3, Spike_up, Spike_down, Spike_trigger, Scorpio_Attack, Scorpio_Moving, Snow_1,Snow2,Water_1,Water_2, Portal_1,Portal_2,
        Portal_Pass,Lion,Laser,Laser_Load,Laser_Shoot,Laser_Work,Grass_1,Grass_2,Grass_3,Horse,Snake,Human,Cat

    }

    

    //private static GameObject oneShotGameObject;
    //private static AudioSource oneShotAudioSource;

    public static void PlaySound(Sound sound, float volume = 1f)
    {
        GameObject soundGameObject = new GameObject("Sound");
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.clip = GetAudioClip(sound);
        audioSource.outputAudioMixerGroup = GetAudioMixer(sound).FindMatchingGroups("Master")[0];
        audioSource.volume = volume;
        audioSource.Play();
        Object.Destroy(soundGameObject, audioSource.clip.length);
    }

    public static AudioClip GetAudioClip(Sound sound)
    {
        foreach (SoundAssets.SoundAudioClip soundAudioClip in SoundAssets.instance.soundAudioClipsArray)
        {
            if (soundAudioClip.sound == sound)
            {
                return soundAudioClip.audioClip;
            }
        }
        Debug.LogError("Sound " + sound + " not found");
        return null;
    }

    public static AudioMixer GetAudioMixer(Sound sound)
    {
        foreach (SoundAssets.SoundAudioClip soundAudioClip in SoundAssets.instance.soundAudioClipsArray)
        {
            
            if (soundAudioClip.sound == sound)
            {
                return soundAudioClip.sfxMixer;
            }
        }
        Debug.LogError("Sound " + sound + " not found");
        return null;
    }


}