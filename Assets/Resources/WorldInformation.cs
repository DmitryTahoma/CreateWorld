using UnityEngine;

public static class WorldInformation
{
    private const int _grassHeight = 220;

    public static byte[,] Blocks { private set; get; }
    public static int Width { set; get; } = 200;
    public static int Depth { private set; get; } = 350;

    static WorldInformation()
    {
        Blocks = new byte[Width, Depth];

        int[] curve = CurveGenerator.GetCurve(_grassHeight, Width, 6);
        for (int i = 0; i < Width; ++i)
            for (int j = 0; j < Depth; ++j)
            {
                if (curve[i] == j)
                    Blocks[i, j] = 0;
                else if (curve[i] > j)
                    Blocks[i, j] = 1;
                else
                    Blocks[i, j] = 255;
            }

        for (int i = 0; i < Width; ++i)
            for (int j = _grassHeight - 13; j < _grassHeight + 13; ++j)
            {
                if (Blocks[i, j] == 0 && i > 0 && i + 1 < Width)
                {
                    if (Blocks[i - 1, j] == 255)
                        Blocks[i, j] = 2;
                    if (curve[i + 1] < j)
                        if (Blocks[i, j] == 2)
                            Blocks[i, j] = 7;
                        else
                            Blocks[i, j] = 3;
                }
                else if (Blocks[i, j] == 1 && i > 0 && j > 0 && i + 1 < Width)
                {
                    if (Blocks[i - 1, j] == 255 && Blocks[i, j - 1] != 255)
                        Blocks[i, j] = 4;
                    if (curve[i + 1] < j && Blocks[i, j - 1] != 255)
                        if (Blocks[i, j] == 4)
                            Blocks[i, j] = 6;
                        else
                            Blocks[i, j] = 5;
                }
            }
    }

    public static GameObject GetObjectById(byte id)
    {
        BlockType type;
        switch (id)
        {
            default: type = BlockType.GrassUp; break;
            case 1: type = BlockType.Ground; break;
            case 2: type = BlockType.GrassUpLeft; break;
            case 3: type = BlockType.GrassUpRight; break;
            case 4: type = BlockType.GrassLeft; break;
            case 5: type = BlockType.GrassRight; break;
            case 6: type = BlockType.GrassLeftRight; break;
            case 7: type = BlockType.GrassUpLeftRight; break;
        };
        return BlockBuilder.Built(type);
    }

    public static GameObject GetObjectById(int x, int y)
        => GetObjectById(Blocks[x, y]);
}
