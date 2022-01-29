using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    [SerializeField] private Tilemap map;
    [SerializeField] private List<TileData> tileDatas;
    private Dictionary<TileBase, TileData> dataFromTile;

    bool sandDamage = false;
    bool inSand = false;

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

    public int GetTileDamage(Vector2 worldPosition)
    {
        Vector3Int gridPosition = map.WorldToCell(worldPosition);
        TileBase tile = map.GetTile(gridPosition);
        int damage = dataFromTile[tile].damage;
        return damage;
        
    }

    public void Effect(Vector2 worldPosition)
    {
        Vector3Int gridPosition = map.WorldToCell(worldPosition);
        TileBase tile = map.GetTile(gridPosition);
        if (dataFromTile[tile].type == "sand")
        {
            
            Sand();
        }
        else {
            StopCoroutine(SandDamage(3));
        }

    }

    void Sand()
    {
        print(inSand);
        if (!inSand) {
            print("mais nonnnnnnnnnnnnnnnnnnnnnnnnnnnnnnn");
            StartCoroutine(SandDamage(10)); 
        }
        
    }

    IEnumerator SandDamage(int cooldown)
    {
        inSand = true;
        yield return new WaitForSeconds(cooldown);
        sandDamage = true;
        print("zzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz");
        inSand = false;
        yield return null;
    }

}
