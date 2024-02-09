using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int width, height;

    [SerializeField] private Tile gridTilePrefab;

    void Start() {
        GenerateGrid();
    }

    void GenerateGrid() {
        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                var spawnedGridTile = Instantiate(gridTilePrefab, new Vector3(x, y), Quaternion.identity);
                spawnedGridTile.name = $"Tile @ {x}, {y}";
            }
        }
    }
}
