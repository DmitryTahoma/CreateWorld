using System.Collections.Generic;
using UnityEngine;

public static class BlockBuilder
{
    private static Dictionary<BlockType, GameObject> _prefabs = null;

    static BlockBuilder()
    {
        _prefabs = new Dictionary<BlockType, GameObject>();

        for (int i = 0; i < 14; ++i)
        {
            BlockType type = (BlockType)i;
            GameObject block = new GameObject(type.ToString());
            block.transform.localScale = new Vector3(0.79f, 0.79f, 0.79f);

            SpriteRenderer renderer = block.AddComponent<SpriteRenderer>();
            Sprite sprite = Resources.Load<Sprite>(@"Textures/Blocks/" + type.ToString());
            renderer.sprite = sprite;

            block.AddComponent<BoxCollider2D>().isTrigger = true;
            block.AddComponent<BlockInteraction>().SetName(type);
            block.AddComponent<ClickReceiver>();

            _prefabs.Add(type, block);
        }
    }

    public static GameObject Built(BlockType type)
    {
        _prefabs.TryGetValue(type, out GameObject block);
        return block;
    }
}
