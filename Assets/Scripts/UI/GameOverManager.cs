using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{

    [SerializeField] private int titleScreenScene = 1;
    [SerializeField] private int savedScene = 1;
    AudioSource clicButtonAudioSource;

    private void Awake()
    {
        clicButtonAudioSource = GetComponent<AudioSource>();
        print(clicButtonAudioSource);
        //savedScene = PlayerPrefs.GetInt("savedLevel");
    }

    public void ContinueGame()
    {
        clicButtonAudioSource.Play();
        print("Yeahhh let's continue");
        UnityEngine.SceneManagement.SceneManager.LoadScene(savedScene);
    }

    public void GoToTitleScreen()
    {
        clicButtonAudioSource.Play();
        print("Retry!");
        UnityEngine.SceneManagement.SceneManager.LoadScene(titleScreenScene);
    }

    public void QuitGame()
    {
        clicButtonAudioSource.Play();
        Debug.Log("On quitte le jeu !");
        Application.Quit();
    }


   
}
