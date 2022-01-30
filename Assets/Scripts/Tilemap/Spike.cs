using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.Tilemaps;
using System;

public class Spike : MonoBehaviour
{
    [SerializeField] AnimatedTile tile;
    bool start = true;
    bool up = false;
    public static event Action SignalSpike;
    bool isInvicibl = false;



    // Update is called once per frame
    void Update()
    {
        if (start == true)
        {
            StartCoroutine(SpikeSound());
        }
    }

    IEnumerator SpikeSound()
    {
        start = false;
        SoundManager.PlaySound(SoundManager.Sound.Spike_down);
        up = false;
        yield return new WaitForSeconds(1f);
        SoundManager.PlaySound(SoundManager.Sound.Spike_trigger);
        yield return new WaitForSeconds(1f);
        SoundManager.PlaySound(SoundManager.Sound.Spike_up);
        up = true;
        yield return new WaitForSeconds(2f);
        start = true;
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        print(up);
        print("ccccccccccccccccccc");
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Rigidbody2D>().WakeUp();
            bool invicibility = collision.gameObject.GetComponent<PlayerManager>().invincibilty;
            if (up && invicibility==false)
            {
                SignalSpike?.Invoke();
            }
        }
    }

}
