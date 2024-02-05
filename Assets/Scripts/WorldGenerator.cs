using UnityEngine;
using UnityEngine.Tilemaps;
using static WorldInformation;

public class WorldGenerator : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private TileBase[] tiles;

    void Start()
    {
        GenerateWorld();
    }

    private void GenerateWorld()
    {
        for (int i = 0; i < Width; ++i)
        {
            for (int j = 0; j < Depth; ++j)
            {
                if (Blocks[i, j] != BlockType.Air) // object with id = 255 is air
                {
					tilemap.SetTile(new Vector3Int(i, j), tiles[(int)Blocks[i, j]]);
                }
            }
        }
    }
}
