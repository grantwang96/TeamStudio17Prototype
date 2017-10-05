using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelMaterials")]
public class LevelMaterials : ScriptableObject {

    public Transform[] solidTiles;
    public Transform[] weakTiles;
    public Transform[] dangerTiles;

    public Transform playerHighlight;
    public Transform critPathHighlight;

    public Vector2[] dirs;

    [Range(1,5)]
    public int unitSize;

    [Range(0.1f, 0.95f)]
    public float blockDensity;
    
    [Range(8, 30)]
    public int mapHalfWidth;
    [Range(8, 30)]
    public int mapHalfHeight;

    [Range(1, 10)]
    public int cushioning;

    [Range(2, 8)]
    public int roomCount;

    public Vector2 mapCenter;
}
