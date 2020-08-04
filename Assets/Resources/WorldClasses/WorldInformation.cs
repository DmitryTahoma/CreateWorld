using UnityEngine;

public static class WorldInformation
{
    public static byte[,] Blocks { private set; get; }
    public static int Width { set; get; } = 200;
    public static int Depth { private set; get; } = 150;

    static WorldInformation()
    {
        Blocks = new byte[Width, Depth];

        int[] curve = CurveGenerator.GetCurve(8, Width, 6);
        for(int i = 0; i < Width; ++i)
            for(int j = 0; j < Depth; ++j)
            {
                if (curve[i] == j)
                    Blocks[i, j] = 0;
                else if (curve[i] > j)
                    Blocks[i, j] = 1;
                else
                    Blocks[i, j] = 255;
            }
    }

    public static GameObject GetObjectById(byte id)
    {
        string name;
        switch(id)
        {
            default: name = "grass"; break;
            case 1: name = "ground"; break;
        };
        return Resources.Load("Blocks/" + name) as GameObject;
    }

    public static GameObject GetObjectById(int x, int y)
        => GetObjectById(Blocks[x, y]);
}
