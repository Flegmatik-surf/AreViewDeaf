using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] GameObject arrival;
    [SerializeField] int roomNumber =0;
    [SerializeField] float waitingTimeRoomOne = 0f;
    [SerializeField] bool startingStateIsSee = true;

    public static event Action<float> RoomOneScriptedSignal;
    public static event Action<bool> RoomSwitchSignal;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag== "Player")
        {
            SoundManager.PlaySound(SoundManager.Sound.Portal_Pass);
            collision.transform.position = arrival.transform.position;
            if(roomNumber == 1)
            {
                RoomOneScriptedSignal?.Invoke(waitingTimeRoomOne);
            } else if(roomNumber == 3)
            {
                RoomSwitchSignal?.Invoke(startingStateIsSee);
            }
        }
    }
}
