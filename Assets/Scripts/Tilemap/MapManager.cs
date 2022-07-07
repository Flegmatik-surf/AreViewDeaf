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
        if (tile && isMoving) {

            if (dataFromTile[tile].type == "sandScorpio")
            {
                SoundManager.Sound[] soundSand = { SoundManager.Sound.Sand_1, SoundManager.Sound.Sand_2, SoundManager.Sound.Sand_3 };
                int number = 0;
                if (!isPlaying)
                {
                    number = (number + 1) % (soundSand.Length);
                    StartCoroutine(SoundCooldown(soundSand[number]));
                    StartCoroutine(SoundCooldown(SoundManager.Sound.Scorpio_Attack));
                }
                SandScorpio();
            }
            else if(isMoving)
            {
                inSand = false;
                StopCoroutine(SandDamage(2));
                if (dataFromTile[tile].type == "sand")
                {
                    SoundManager.Sound[] soundSand = { SoundManager.Sound.Sand_1, SoundManager.Sound.Sand_2, SoundManager.Sound.Sand_3 };
                    int number = 0;
                    if (!isPlaying)
                    {
                        number = (number + 1) % (soundSand.Length);
                        StartCoroutine(SoundCooldown(soundSand[number]));
                    }
                }
                if (dataFromTile[tile].type == "donjon")
                {
                    SoundManager.Sound[] soundSand = { SoundManager.Sound.Donjon_1, SoundManager.Sound.Donjon_2, SoundManager.Sound.Donjon_3 };
                    int number = 0;
                    if (!isPlaying)
                    {
                        number = (number + 1) % (soundSand.Length);
                        StartCoroutine(SoundCooldown(soundSand[number]));
                    }
                }
                if (dataFromTile[tile].type == "grass")
                {
                    SoundManager.Sound[] sound= { SoundManager.Sound.Grass_1, SoundManager.Sound.Grass_2, SoundManager.Sound.Grass_3 };
                    int number = 0;
                    if (!isPlaying)
                    {
                        number = (number + 1) % (sound.Length);
                        StartCoroutine(SoundCooldown(sound[number]));
                    }
                }
                if (dataFromTile[tile].type == "winter")
                {
                    SoundManager.Sound[] sound = { SoundManager.Sound.Snow_1, SoundManager.Sound.Snow2 };
                    int number = 0;
                    if (!isPlaying)
                    {
                        number = (number + 1) % (sound.Length);
                        StartCoroutine(SoundCooldown(sound[number]));
                    }
                }
                if (dataFromTile[tile].type == "water")
                {
                    SoundManager.Sound[] sound = { SoundManager.Sound.Water_1, SoundManager.Sound.Water_2 };
                    int number = 0;
                    if (!isPlaying)
                    {
                        number = (number + 1) % (sound.Length);
                        StartCoroutine(SoundCooldown(sound[number]));
                    }
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
