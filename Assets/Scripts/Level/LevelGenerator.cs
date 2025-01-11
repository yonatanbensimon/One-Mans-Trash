using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelGenerator : MonoBehaviour
{
    private Unity.Mathematics.Random rng = new Unity.Mathematics.Random();

    [SerializeField]
    private int tileCount = 10;

    [SerializeField] 
    private GameObject tilePrefab;

    [SerializeField]
    private Vector3 tileOffset;
    private Vector3 currentTilePos = Vector3.zero;

    private LevelTile[] tiles;
    private int lastTileIndex = 0;

    [SerializeField]
    private Vector2Int minMaxCollectibleFrequency = new Vector2Int(5, 10);

    [SerializeField]
    private GameObject[] collectibleObjects;

    [SerializeField]
    private int nextCollectibleIn = 1;

    [SerializeField]
    private GameObject regenerateBehindObject;

    [SerializeField]
    private float regenerationDistance;

    void Start()
    {
        rng.InitState();
        tiles = new LevelTile[tileCount];
        for (int i = 0; i < tileCount; i++) {
            tiles[i] = new LevelTile();
            RegenerateTile(tiles[i]);
        }
    }

    public void RegenerateLastTile() {
        RegenerateTile(tiles[lastTileIndex]);
        lastTileIndex = (lastTileIndex + 1) % tileCount;

        nextCollectibleIn--;
    }

    void RegenerateTile(LevelTile tile) {
        if (tile.tileObject != null) {
            Destroy(tile.tileObject);
        }
        GameObject tileObject = Instantiate(tilePrefab, gameObject.transform);
        tileObject.transform.position = currentTilePos;
        
        tile.tileObject = tileObject;

        if (nextCollectibleIn <= 0) {
            GameObject collectible = Instantiate(collectibleObjects[rng.NextInt(collectibleObjects.Count())], tileObject.transform);
            collectible.transform.localPosition = Vector3.zero;
            tile.containedObjects.Add(collectible);
            nextCollectibleIn = rng.NextInt(minMaxCollectibleFrequency.x, minMaxCollectibleFrequency.y + 1);
        }
        else {
            nextCollectibleIn--;
        }

        currentTilePos += tileOffset;
    }

    void FixedUpdate() {
        LevelTile lastTile = tiles[lastTileIndex];
        Transform unloaderTransform = regenerateBehindObject.transform;
        Transform tileTransform = lastTile.tileObject.transform;
        if (Vector3.Dot(tileTransform.position - unloaderTransform.position, unloaderTransform.forward) < 0
            && Vector3.Distance(tileTransform.position, unloaderTransform.position) > regenerationDistance) 
        {
            RegenerateLastTile();
        }
    }
}
