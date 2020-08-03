using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    void Start()
    {
        for(int i = 0; i < WorldInformation.Width; ++i)
            for(int j = 0; j < WorldInformation.Depth; ++j)
            {
                if (WorldInformation.Blocks[i, j] != 255) // object with id = 255 is air
                {
                    GameObject block = Instantiate(WorldInformation.GetObjectById(i, j));
                    block.transform.position = new Vector2(i - WorldInformation.Width / 2 + 0.5f, j);
                }
            }
    }
}
