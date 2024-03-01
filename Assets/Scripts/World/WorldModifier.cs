using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class WorldModifier : MouseClickableBase
{
	[SerializeField] private Inventory inventory;
	[SerializeField] private Tilemap frontTilemap;
	[SerializeField] private Tilemap backTilemap;

	public override void OnClick(InputAction.CallbackContext context)
	{
		if (!context.canceled || !isActiveAndEnabled) return;

		BlockType typeToPlace = inventory.GetSelectedBlockType();

		TileBase tileToPlace = null;
		if (typeToPlace != BlockType.Air)
		{
			tileToPlace = WorldInformation.Instance.Tiles[(int)typeToPlace];
		}

		Vector3Int tilemapPos = frontTilemap.WorldToCell(GameInput.Instance.GetMouseWorldPosition());
		if (backTilemap.HasTile(tilemapPos))
		{
			if (frontTilemap.HasTile(tilemapPos) == (tileToPlace == null))
			{
				Handled = true;
			}
			frontTilemap.SetTile(tilemapPos, tileToPlace);
		}
		else
		{
			if (backTilemap.HasTile(tilemapPos) == (tileToPlace == null))
			{
				Handled = true;
			}
			backTilemap.SetTile(tilemapPos, tileToPlace);
		}
	}
}
