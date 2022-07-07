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
    public bool isMoving;
    private bool canMove=true;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        anim = this.GetComponent<Animator>();
        canMove = true;
        AudioMixer sfxMixer = Resources.Load("Mixers/SFXMixer") as AudioMixer;
        AudioMixerGroup outputAudioMixerGroup = sfxMixer.FindMatchingGroups("Master")[0];
        print(outputAudioMixerGroup);
    }

    public IEnumerator FreezePlayer(float time)
    {
        canMove = false;
        yield return new WaitForSeconds(time);
        canMove = true;
    }


    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("Speed", 0f);

        float h_input = 0;
        float v_input = 0;

        if (canMove)
        {
            if (Input.GetKey(KeyCode.Z)||Input.GetKey(KeyCode.UpArrow))
            {
                Direction = 0f;
                anim.SetFloat("Speed", 1f);
                v_input += 1;
            }

            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                Direction = 1f;
                anim.SetFloat("Speed", 1f);
                v_input -= 1;
            }

            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                Direction = 3f;
                anim.SetFloat("Speed", 1f);
                h_input += 1;
            }

            if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.LeftArrow))
            {
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
