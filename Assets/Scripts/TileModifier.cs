using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class TileModifier : MonoBehaviour
{
	[SerializeField] private Tilemap frontTilemap;
	[SerializeField] private Tilemap backTilemap;
	[SerializeField] private TileBase tileToPlace;

	public void OnLeftClick(InputAction.CallbackContext context)
	{
		if (!context.started) return;

		Vector3Int tilemapPos = frontTilemap.WorldToCell(GameInput.Instance.GetMouseWorldPosition());
		if (frontTilemap.HasTile(tilemapPos))
		{
			frontTilemap.SetTile(tilemapPos, null);
		}
		else
		{
			backTilemap.SetTile(tilemapPos, null);
		}
	}

	public void OnRightClick(InputAction.CallbackContext context)
	{
		if (!context.started) return;

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
