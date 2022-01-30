using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] GameObject arrival;
    [SerializeField] GameObject cameraArrival;
    [SerializeField] int roomNumber =0;
    [SerializeField] float waitingTimeRoomOne = 0f;
    [SerializeField] bool startingStateIsSee = true;
    Camera camera;

    public static event Action<float> RoomOneScriptedSignal;
    public static event Action<bool> RoomSwitchSignal;

    private void Start()
    {
        camera = (Camera)FindObjectOfType(typeof(Camera));
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag== "Player")
        {
            SoundManager.PlaySound(SoundManager.Sound.Portal_Pass);
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
            
            collision.transform.position = new Vector3(arrival.transform.position.x, arrival.transform.position.y, 0);
            camera.transform.position = cameraArrival.transform.position;
            if(roomNumber == 1)
            {
                RoomOneScriptedSignal?.Invoke(waitingTimeRoomOne);
            } else if(roomNumber == 2)
            {
                RoomSwitchSignal?.Invoke(startingStateIsSee);
            }
        }
    }
}
