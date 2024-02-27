using UnityEngine;

public static class WorldInformation
{
    private const int _grassHeight = 20;

    public static BlockType[,] Blocks { private set; get; }
    public static int Width { set; get; } = 200;
    public static int Depth { private set; get; } = 350;

    static WorldInformation()
    {
        Blocks = new BlockType[Width, Depth];

        int[] curve = CurveGenerator.GetCurve(_grassHeight, Width, 6);
        for (int i = 0; i < Width; ++i)
            for (int j = 0; j < Depth; ++j)
            {
                if (curve[i] == j)
                    Blocks[i, j] = BlockType.GrassUp;
                else if (curve[i] > j)
                    Blocks[i, j] = BlockType.Ground;
                else
                    Blocks[i, j] = BlockType.Air;
            }

        for (int i = 0; i < Width; ++i)
            for (int j = _grassHeight - 13; j < _grassHeight + 13; ++j)
            {
                if (Blocks[i, j] == 0 && i > 0 && i + 1 < Width)
                {
                    if (Blocks[i - 1, j] == BlockType.Air)
                        Blocks[i, j] = BlockType.GrassUpLeft;
                    if (curve[i + 1] < j)
                        if (Blocks[i, j] == BlockType.GrassUpLeft)
                            Blocks[i, j] = BlockType.GrassUpLeftRight;
                        else
                            Blocks[i, j] = BlockType.GrassUpRight;
                }
                else if (Blocks[i, j] == BlockType.Ground && i > 0 && j > 0 && i + 1 < Width)
                {
                    if (Blocks[i - 1, j] == BlockType.Air && Blocks[i, j - 1] != BlockType.Air)
                        Blocks[i, j] = BlockType.GrassLeft;
                    if (curve[i + 1] < j && Blocks[i, j - 1] != BlockType.Air)
                        if (Blocks[i, j] == BlockType.GrassLeft)
                            Blocks[i, j] = BlockType.GrassLeftRight;
                        else
                            Blocks[i, j] = BlockType.GrassRight;
                }
            }
    }
}
