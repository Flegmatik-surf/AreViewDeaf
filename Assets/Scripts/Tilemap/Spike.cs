using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.Tilemaps;


public class Spike : MonoBehaviour
{
    [SerializeField] AnimatedTile tile;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        print(tile.m_AnimationStartFrame);
    }
}
