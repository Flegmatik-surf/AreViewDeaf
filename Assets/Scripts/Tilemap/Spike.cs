using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.Tilemaps;


public class Spike : MonoBehaviour
{
    [SerializeField] AnimatedTile tile;
    bool start = true;
    
    void Start()
    {
        //tile.m_AnimationStartFrame;
    }

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
        print("not up");
        yield return new WaitForSeconds(1f);
        print("trigger");
        yield return new WaitForSeconds(1f);
        print("up");
        yield return new WaitForSeconds(2f);
        start = true;
    }

}
