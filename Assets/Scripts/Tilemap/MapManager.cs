using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{

    [SerializeField] private Tilemap map;
    [SerializeField] private List<TileData> tileDatas;
    private Dictionary<TileBase, TileData> dataFromTile;

    //sand
    bool inSand = false;
    public static event Action SignalSandDamage;

    //sound
    bool isPlaying=false;

    //player
    GameObject player;
    bool isMoving;

    private void Awake()
    {
        dataFromTile = new Dictionary<TileBase, TileData>();
        foreach(var tileData in tileDatas)
        {
            foreach(var tile in tileData.tiles)
            {
                dataFromTile.Add(tile, tileData);
            }
        }
        player= GameObject.FindGameObjectWithTag("Player");
        
    }

    private void Update()
    {
        isMoving = player.GetComponent<PlayerMovement>().isMoving;
    }
    public float GetTileWalkingSpeed(Vector2 worldPosition)
    {
        Vector3Int gridPosition = map.WorldToCell(worldPosition);
        TileBase tile = map.GetTile(gridPosition);
        float walkingSpeed = dataFromTile[tile].walkingSpeed;
        return walkingSpeed;      
    }


    public void Effect(Vector2 worldPosition)
    {
        Vector3Int gridPosition = map.WorldToCell(worldPosition);
        TileBase tile = map.GetTile(gridPosition);
        if (tile) {

            if (dataFromTile[tile].type == "sandScorpio" && isMoving)
            {
                SoundManager.Sound[] soundSand = { SoundManager.Sound.Sand_1, SoundManager.Sound.Sand_2, SoundManager.Sound.Sand_3 };
                int number = 0;
                if (!isPlaying)
                {
                    number = (number + 1) % (soundSand.Length);
                    StartCoroutine(SoundCooldown(soundSand[number]));
                }
                SandScorpio();
            }
            else
            {
                inSand = false;
                StopCoroutine(SandDamage(10));
                if (dataFromTile[tile].type == "sand" && isMoving)
                {
                    SoundManager.Sound[] soundSand = { SoundManager.Sound.Sand_1, SoundManager.Sound.Sand_2, SoundManager.Sound.Sand_3 };
                    int number = 0;
                    if (!isPlaying)
                    {
                        number = (number + 1) % (soundSand.Length);
                        StartCoroutine(SoundCooldown(soundSand[number]));
                    }
                }
                if (dataFromTile[tile].type == "donjon" && isMoving)
                {
                    SoundManager.Sound[] soundSand = { SoundManager.Sound.Donjon_1, SoundManager.Sound.Donjon_2, SoundManager.Sound.Donjon_3 };
                    int number = 0;
                    if (!isPlaying)
                    {
                        number = (number + 1) % (soundSand.Length);
                        StartCoroutine(SoundCooldown(soundSand[number]));
                    }
                }
                if (dataFromTile[tile].type == "grass" && isMoving)
                {

                }
            }
        }

    }

    void SandScorpio()
    {
        if (!inSand) {
            StartCoroutine(SandDamage(5));
        }
        
    }

    void Spike()
    {

    }

    IEnumerator SandDamage(int cooldown)
    {
        inSand = true;
        yield return new WaitForSeconds(cooldown);
        SignalSandDamage?.Invoke();
        inSand = false;
        yield return null;
    }

    IEnumerator SoundCooldown(SoundManager.Sound sound)
    {
        isPlaying = true;
        SoundManager.PlaySound(sound);
        yield return new WaitForSeconds(0.2f);
        isPlaying = false;

        yield return null;
    }

}
