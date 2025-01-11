using System;
using Unity.VisualScripting;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField]
    private int tileCount = 10;

    [SerializeField] 
    private GameObject tilePrefab;

    [SerializeField]
    private Vector3 tileOffset;
    private Vector3 currentTilePos = Vector3.zero;

    private LevelTile[] tiles;
    private int lastTileIndex = 0;

    private float tempCounter = 0;

    void Start()
    {
        tiles = new LevelTile[tileCount];
        for (int i = 0; i < tileCount; i++) {
            RegenerateTile(ref tiles[i]);
        }
    }

    public void RegenerateLastTile() {
        RegenerateTile(ref tiles[lastTileIndex]);
        lastTileIndex = (lastTileIndex + 1) % tileCount;
    }

    void RegenerateTile(ref LevelTile tile) {
        Destroy(tile.gameObject);

        GameObject tileObject = Instantiate(tilePrefab, gameObject.transform);
        tileObject.transform.position = currentTilePos;
        currentTilePos += tileOffset;
        
        tile.gameObject = tileObject;
    }
}
