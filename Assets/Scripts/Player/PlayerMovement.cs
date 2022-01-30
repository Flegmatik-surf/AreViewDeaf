using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private float Direction = 1f;
    private Rigidbody2D rb;
    private Animator anim;
    private MapManager mapManager;
    public bool isMoving;
    private bool canMove=true;

    // Start is called before the first frame update//
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        anim = this.GetComponent<Animator>();
        canMove = true;
        AudioMixer sfxMixer = Resources.Load("Mixers/SFXMixer") as AudioMixer;
        AudioMixerGroup outputAudioMixerGroup = sfxMixer.FindMatchingGroups("Master")[0];
        print(outputAudioMixerGroup);
    }

    private void Awake()
    {
        mapManager = FindObjectOfType<MapManager>();
        SceneManagerScript.startSceneSignal += OnStartingScene;
        Teleport.RoomOneScriptedSignal += OnScriptedRoomOneChange;
    }

    private void OnScriptedRoomOneChange(float time)
    {
        StartCoroutine(FreezePlayer(time));
    }

    private void OnStartingScene(int sceneIndex)
    {
        if(sceneIndex == 1)
        {
            StartCoroutine(FreezePlayer(5f));
        }
    }

    private IEnumerator FreezePlayer(float time)
    {
        canMove = false;
        yield return new WaitForSeconds(time);
        canMove = true;
    }


    // Update is called once per frame
    void Update()
    { 
        try
        {
            speed = mapManager.GetTileWalkingSpeed(transform.position);
        }
        catch
        {
            speed = 5;
        }
        

        //anim.SetFloat("Direction", 1f);

        anim.SetFloat("Speed", 0f);

        float h_input = 0;
        float v_input = 0;
        //anim.SetBool("Up", false);
        //anim.SetBool("Down", false);
        //anim.SetBool("Left", false);
        //anim.SetBool("Right", false);

        if (canMove)
        {
            if (Input.GetKey(KeyCode.Z))
            {
                //anim.SetBool("Up", true);
                Direction = 0f;
                anim.SetFloat("Speed", 1f);
                v_input += 1;
            }

            if (Input.GetKey(KeyCode.S))
            {
                //anim.SetBool("Down", true);
                Direction = 1f;
                anim.SetFloat("Speed", 1f);
                v_input -= 1;
            }

            if (Input.GetKey(KeyCode.D))
            {
                //anim.SetBool("Right", true);
                Direction = 3f;
                anim.SetFloat("Speed", 1f);
                h_input += 1;
            }

            if (Input.GetKey(KeyCode.Q))
            {
                //anim.SetBool("Left", true);
                Direction = 2f;
                anim.SetFloat("Speed", 1f);
                h_input -= 1;
            }

            anim.SetFloat("Direction", Direction);

            rb.velocity = new Vector2(h_input, v_input).normalized * speed;
            if (rb.velocity.magnitude > 0.01)
            {
                isMoving = true;
            }
            else
            {
                isMoving = false;
            }
        }
    }
}
