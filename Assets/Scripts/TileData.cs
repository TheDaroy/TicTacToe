using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Test/Attack")]
public class TileData : ScriptableObject
{
    [SerializeField] GameObject TilePrefab;
    public GameObject SpawnTile(Vector2 spawnLocation)
    {
        return Instantiate(TilePrefab, spawnLocation, Quaternion.identity);
    }
}
