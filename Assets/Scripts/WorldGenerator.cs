using UnityEngine;
using UnityEngine.Tilemaps;
using static WorldInformation;

public class WorldGenerator : MonoBehaviour
{
    [SerializeField] private Tilemap frontTilemap;
    [SerializeField] private Tilemap backTilemap;
    [SerializeField] private TileBase[] tiles;

    void Start()
    {
        GenerateWorld();
    }

    private void GenerateWorld()
    {
        int halfOfWidth = Width / 2;
        for (int i = 0; i < Width; ++i)
        {
            for (int j = 0; j < Depth; ++j)
            {
                if (Blocks[i, j] != BlockType.Air) // object with id = 255 is air
                {
					frontTilemap.SetTile(new Vector3Int(i - halfOfWidth, j), tiles[(int)Blocks[i, j]]);
					backTilemap.SetTile(new Vector3Int(i - halfOfWidth, j), tiles[(int)Blocks[i, j]]);
                }
            }
        }
    }
}
