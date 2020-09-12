using System;
using UnityEngine;

public class BlockInteraction : MonoBehaviour
{
    public string Name;
    public event Action UpdateBlock;

    public void DoUpdateBlock()
    {
        UpdateBlock?.Invoke();
    }

    public void BindInteraction(BlockInteraction interaction)
    {
        UpdateBlock += interaction.OnUpdate;
        interaction.UpdateBlock += OnUpdate;
    }

    private void OnUpdate()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
    }
}
