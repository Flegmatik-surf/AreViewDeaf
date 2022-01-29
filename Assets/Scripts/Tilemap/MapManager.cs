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

        if (dataFromTile[tile].type == "sandScorpio")
        {
            
            SandScorpio();
        }
        else {
            inSand = false;
            StopCoroutine(SandDamage(10));
        }

    }

    void SandScorpio()
    {
        if (!inSand) {
            StartCoroutine(SandDamage(5));
        }
        
    }

    IEnumerator SandDamage(int cooldown)
    {
        inSand = true;
        yield return new WaitForSeconds(cooldown);
        SignalSandDamage?.Invoke();
        inSand = false;
        yield return null;
    }

}
