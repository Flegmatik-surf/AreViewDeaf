using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject titleScreenContainer;
    [SerializeField] private GameObject settingsScreenContainer;

    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private TMP_Dropdown qualityDropdown;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private AudioMixer musicAudioMixer;
    [SerializeField] private AudioMixer sfxAudioMixer;
    [SerializeField] private float defaultVolume = 0f;
    private int defaultResolution;


    [SerializeField] private int startScene = 1;
    [SerializeField] private int savedScene = 1;
    AudioSource audioSource;
    Resolution[] resolutions;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        print(audioSource);

        resolutions = Screen.resolutions;
        List<string> options = new List<string>();
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.height)
            {
                defaultResolution = i;
            }
        }
        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions(options);

        ApplyDefaults();
    }


    public void GoToSettingsScreen()
    {
        audioSource.Play();
        titleScreenContainer.SetActive(false);
        settingsScreenContainer.SetActive(true);
    }

    public void GoToTitleScreen()
    {
        audioSource.Play();
        titleScreenContainer.SetActive(true);
        settingsScreenContainer.SetActive(false);
    }


    public void SetGraphicsQuality(int qualityIndex)
    {
        Debug.Log("quality : " + qualityIndex);
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetResolution(int resolutionInt)
    {
        Resolution resolution = resolutions[resolutionInt];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetVolume(float volume)
    {
        musicAudioMixer.SetFloat("mainVolume", volume);
        sfxAudioMixer.SetFloat("sfxVolume", volume);
    }


    public void SetToDefaults()
    {
        ApplyDefaults();
    }

    public void QuitGame()
    {
        audioSource.Play();
        Debug.Log("On quitte le jeu !");
        Application.Quit();
    }


    //--------------- auxilary functions ------------------

    private void ApplyDefaults()
    {
        volumeSlider.value = defaultVolume;
        SetVolume(defaultVolume);

        resolutionDropdown.value = defaultResolution;
        resolutionDropdown.RefreshShownValue();
        SetResolution(defaultResolution);
        qualityDropdown.value = 1;
        qualityDropdown.RefreshShownValue();
        SetGraphicsQuality(1);
    }
}
