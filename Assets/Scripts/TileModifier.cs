using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class TileModifier : MouseClickableBase
{
	[SerializeField] private Tilemap frontTilemap;
	[SerializeField] private Tilemap backTilemap;
	[SerializeField] private TileBase tileToPlace;

	public override void OnClick(InputAction.CallbackContext context)
	{
		if (!context.canceled || !isActiveAndEnabled) return;

		Vector3Int tilemapPos = frontTilemap.WorldToCell(GameInput.Instance.GetMouseWorldPosition());
		if (frontTilemap.HasTile(tilemapPos))
		{
			frontTilemap.SetTile(tilemapPos, null);
		}
		else
		{
			backTilemap.SetTile(tilemapPos, null);
		}

		Handled = true;
	}

	public void OnRightClick(InputAction.CallbackContext context)
	{
		if (!context.started || !isActiveAndEnabled) return;

		Vector3Int tilemapPos = frontTilemap.WorldToCell(GameInput.Instance.GetMouseWorldPosition());
		if (backTilemap.HasTile(tilemapPos))
		{
			frontTilemap.SetTile(tilemapPos, tileToPlace);
		}
		else
		{
			backTilemap.SetTile(tilemapPos, tileToPlace);
		}
	}
}
