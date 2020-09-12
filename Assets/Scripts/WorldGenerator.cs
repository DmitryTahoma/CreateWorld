using System.Collections.Generic;
using UnityEngine;
using static WorldInformation;

public class WorldGenerator : MonoBehaviour
{
    private List<List<BlockInteraction>> _interactions = new List<List<BlockInteraction>>();

    [SerializeField] private Transform _point;

    void Start()
    {
        GenerateWorld();
    }

    private void GenerateWorld()
    {
        for (int i = 0; i < Width; ++i)
        {
            _interactions.Add(new List<BlockInteraction>());
            for (int j = 0; j < Depth; ++j)
            {
                if (Blocks[i, j] != 255) // object with id = 255 is air
                {
                    GameObject block = Instantiate(GetObjectById(i, j), new Vector2(i - Width / 2, j), new Quaternion(), _point);
                    BlockInteraction interaction = block.GetComponent<BlockInteraction>();
                    _interactions[i].Add(interaction);

                    if (i == 0)
                    {
                        if (j != 0)
                            if (Blocks[i, j - 1] != 255)
                                interaction.BindInteraction(_interactions[i][j - 1]);
                    }
                    else
                    {
                        if (j != 0)
                        {
                            if (Blocks[i, j - 1] != 255)
                                interaction.BindInteraction(_interactions[i][j - 1]);
                            if (Blocks[i - 1, j - 1] != 255)
                                interaction.BindInteraction(_interactions[i - 1][j - 1]);
                        }
                        if (Blocks[i - 1, j] != 255)
                            interaction.BindInteraction(_interactions[i - 1][j]);
                        if (j != Depth - 1)
                            if (Blocks[i - 1, j + 1] != 255)
                                interaction.BindInteraction(_interactions[i - 1][j + 1]);
                    }
                }
                else
                    _interactions[i].Add(null);
            }
        }
    }
}
