using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SensManagerScripted : MonoBehaviour
{
    [SerializeField] private Fader fader;
    [SerializeField] bool see = true;
    [SerializeField] float time = 10f;
    GameObject player;
    // Start is called before the first frame update

    private void Awake()
    {
        Teleport.RoomOneScriptedSignal += OnRoomOneSignal;
        Teleport.RoomSwitchSignal += OnRoomSwitchSignal;
        SceneManagerScript.startSceneSignal += OnStartingScene;
        player = GameObject.FindGameObjectWithTag("Player");


    }

    private void Update()
    {
        //Pour la scène finale 
        if (Input.GetKeyDown("w"))
        {
            fader.FadeInGlobal(1f);
        }

        //debogue
        if (Input.GetKeyDown("x"))
        {
            fader.FadeOutGlobal(1f);
        }
        if (SceneManager.GetActiveScene().buildIndex == 10)
        {
            if (Input.GetKeyDown("f"))
            {
                fader.FadeInGlobal(1f);
            }

            //debogue
            if (Input.GetKeyDown("g"))
            {
                fader.FadeOutGlobal(1f);
            }
        }


    }

    IEnumerator BlindToSee(float time)
    {
        fader.FadeInGlobal(1f);
        yield return new WaitForSeconds(time);
        fader.FadeOutGlobal(1f);
    }

    IEnumerator SeeToBlind(float time)
    {
        fader.FadeOutGlobal(1f);
        yield return new WaitForSeconds(time);
        fader.FadeInGlobal(1f);
    }

    private void OnRoomOneSignal(float time)
    {
        StartCoroutine(BlindToSee(time));
    }

    private void OnRoomSwitchSignal(bool startingState)
    {
        see = startingState;
        if (see)
        {
            fader.FadeInGlobal(1f);
        }
        else
        {
            fader.FadeOutGlobal(1f);
        }
    }

    private void OnDestroy()
    {
        Teleport.RoomOneScriptedSignal -= OnRoomOneSignal;
        Teleport.RoomSwitchSignal -= OnRoomSwitchSignal;
        SceneManagerScript.startSceneSignal -= OnStartingScene;
    }

    private void OnStartingScene(int sceneIndex)
    {
        //Premiere salle d'introduction, on voit et on entends
        if(sceneIndex == 2)
        {
            fader.FadeInSound(0);
            fader.FadeInGraphic(0);

        }
        else
        {
            StartCoroutine(player.GetComponent<PlayerMovement>().FreezePlayer(time));
            StartCoroutine(BlindToSee(time));
        }
    }
}



