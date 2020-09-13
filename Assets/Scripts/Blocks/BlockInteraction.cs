using System;
using UnityEngine;

public class BlockInteraction : MonoBehaviour
{
    private event Action _updateAround;

    private string _name;

    public void SetName(BlockType type) => _name = type.ToString();
    public string GetName() => _name;

    public void DoUpdate()
    {
        _updateAround?.Invoke();
    }

    public void BindInteraction(BlockInteraction interaction)
    {
        _updateAround += interaction.OnUpdate;
        interaction._updateAround += OnUpdate;
    }

    private void OnUpdate()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
    }
}
