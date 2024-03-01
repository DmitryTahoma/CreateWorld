using UnityEngine;
using UnityEngine.Tilemaps;
using static WorldInformation;

public class WorldGenerator : MonoBehaviour
{
    [SerializeField] private Tilemap frontTilemap;
    [SerializeField] private Tilemap backTilemap;

    void Start()
    {
        GenerateWorld();
    }

    private void GenerateWorld()
    {
        WorldInformation worldInformation = WorldInformation.Instance;

        int halfOfWidth = Width / 2;
        for (int i = 0; i < Width; ++i)
        {
            for (int j = 0; j < Depth; ++j)
            {
                if (Blocks[i, j] != BlockType.Air) // object with id = 255 is air
                {
					frontTilemap.SetTile(new Vector3Int(i - halfOfWidth, j), worldInformation.Tiles[(int)Blocks[i, j]]);
					backTilemap.SetTile(new Vector3Int(i - halfOfWidth, j), worldInformation.Tiles[(int)Blocks[i, j]]);
                }
            }
        }
    }
}
