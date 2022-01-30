using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensManagerScripted : MonoBehaviour
{
    [SerializeField] private Fader fader;
    [SerializeField] bool see = true;
    [SerializeField] float time = 10f;

    // Start is called before the first frame update

    private void Awake()
    {
        Teleport.RoomOneScriptedSignal += OnRoomOneSignal;
        Teleport.RoomSwitchSignal += OnRoomSwitchSignal;
        SceneManagerScript.startSceneSignal += OnStartingScene;
    }

    void Start()
    {
        if (see)
        {
            fader.FadeInGlobal(1f);
        }
        else
        {
            fader.FadeOutGlobal(1f);
        }
    }

    private void Update()
    {
        //debogue
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
    }

    private void OnStartingScene(int sceneIndex)
    {
        if(sceneIndex == 1)
        {
            StartCoroutine(BlindToSee(5f));
        }
        else if(sceneIndex == 2)
        {
            fader.FadeOutGlobal(1f);
        }
    }
}



