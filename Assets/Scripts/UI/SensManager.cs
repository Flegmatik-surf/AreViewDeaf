using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensManager : MonoBehaviour
{
    [SerializeField] private Fader fader;
    [SerializeField] bool see = true;
    [SerializeField] float time = 10f;

    bool canSwitch = true;
    // Start is called before the first frame update

    private void Awake()
    {
        Teleport.RoomSwitchSignal += OnRoomSwitchSignal;
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

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            if (see && canSwitch)
            {
                StartCoroutine(SeeToBlind(time));
            }
            else if (canSwitch)
            {
                StartCoroutine(BlindToSee(time));
            }
        }

    }

    IEnumerator BlindToSee(float time)
    {
        canSwitch = false;
        fader.FadeInGlobal(1f);
        yield return new WaitForSeconds(time);
        fader.FadeOutGlobal(1f);
        canSwitch = true;
    }

    IEnumerator SeeToBlind(float time)
    {
        canSwitch = false;
        fader.FadeOutGlobal(1f);
        yield return new WaitForSeconds(time);
        fader.FadeInGlobal(1f);
        canSwitch = true;
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
}
