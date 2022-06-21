using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Fader : MonoBehaviour
{
    //SpriteRenderer spriteRenderer;
    //private IEnumerator coroutine;
    private Color spriteRendererColor;
    private GameObject image;
    [SerializeField] private AudioMixer musicMixer;
    [SerializeField] private AudioMixer sfxMixer;
    [SerializeField] private float maxVolumeMusic;
    [SerializeField] private float minVolumeMusic;
    [SerializeField] private float maxVolumeSFX;
    [SerializeField] private float minVolumeSFX;

    void Awake()
    {
        spriteRendererColor = GetComponent<Image>().color;
    }
    public IEnumerator FadeOutSound(float time)
    {
        //augmenter musique
        float soundVolume;
        sfxMixer.GetFloat("sfxVolume", out soundVolume);
        float startVolume = soundVolume;
        while (soundVolume < maxVolumeSFX)
        {
            soundVolume += (maxVolumeSFX - startVolume)*Time.unscaledDeltaTime / time;
            sfxMixer.SetFloat("sfxVolume", soundVolume);
            yield return null;
        }
    }
    public IEnumerator FadeOutMusic(float time)
    {
        //augmenter musique
        float musicVolume;
        musicMixer.GetFloat("mainVolume", out musicVolume);
        float startVolume = musicVolume;
        while (musicVolume < maxVolumeMusic)
        {
            musicVolume += (maxVolumeMusic - startVolume)*Time.unscaledDeltaTime / time;
            musicMixer.SetFloat("mainVolume", musicVolume);
            yield return null;
        }
    }

    public IEnumerator FadeInSound(float time)
    {
        //augmenter musique
        float soundVolume;
        sfxMixer.GetFloat("sfxVolume", out soundVolume);
        float startVolume = soundVolume;
        while (soundVolume > minVolumeSFX)
        {
            soundVolume -= (startVolume - minVolumeSFX)*Time.unscaledDeltaTime / time;
            sfxMixer.SetFloat("sfxVolume", soundVolume);
            yield return null;
        }
    }
    public IEnumerator FadeInMusic(float time)
    {
        //augmenter musique
        float musicVolume;
        musicMixer.GetFloat("mainVolume", out musicVolume);
        float startVolume = musicVolume;
        while (musicVolume > minVolumeMusic)
        {
            musicVolume -=(startVolume-minVolumeMusic) * Time.unscaledDeltaTime / time;
            musicMixer.SetFloat("mainVolume", musicVolume);
            yield return null;
        }
    }
    public IEnumerator FadeOutGraphic(float time)
    {
        //Augmenter lumière
        while (spriteRendererColor.a < 1)
        {
            //print(spriteRendererColor.a);
            spriteRendererColor = gameObject.GetComponent<Image>().color;
            spriteRendererColor.a += Time.unscaledDeltaTime / time;
            gameObject.GetComponent<Image>().color = spriteRendererColor;
            yield return null;
        }
    }



    public IEnumerator FadeInGraphic(float time)
    {
        //mettre le son 



        while (spriteRendererColor.a > 0)
        {
            spriteRendererColor = gameObject.GetComponent<Image>().color;
            spriteRendererColor.a -= Time.unscaledDeltaTime / time;
            gameObject.GetComponent<Image>().color = spriteRendererColor;
            yield return null;
        }
    }

    public IEnumerator FadeIn(float time)
    {
        StartCoroutine(FadeInGraphic(time));
        StartCoroutine(FadeOutMusic(time));
        
        
        StartCoroutine(FadeInSound(time));
        yield return null;
    }

    public IEnumerator FadeOut(float time)
    {
        
        StartCoroutine(FadeOutGraphic(time));

        StartCoroutine(FadeInMusic(time));

        StartCoroutine(FadeOutSound(time));

        yield return null;
    }

    public void FadeInGlobal(float time)
    {
        StartCoroutine(FadeIn(time));
    }

    public void FadeOutGlobal(float time)
    {
        StartCoroutine(FadeOut(time));
    }


}