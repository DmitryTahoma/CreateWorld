using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class TileModifier : MonoBehaviour
{
	private Camera mainCamera;

	[SerializeField] private Tilemap frontTilemap;
	[SerializeField] private Tilemap backTilemap;
	[SerializeField] private TileBase tileToPlace;

	private void Awake()
	{
		mainCamera = Camera.main;
	}

	public void OnClick(InputAction.CallbackContext context)
	{
		if (!context.started) return;

		bool isPlacing = false;
		if (context.control.displayName == "Right Button")
		{
			isPlacing = true;
		}
		Vector3 worldPos = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
		Vector3Int tilemapPos = frontTilemap.WorldToCell(worldPos);

		if (isPlacing)
		{
			if (backTilemap.HasTile(tilemapPos))
			{
				frontTilemap.SetTile(tilemapPos, tileToPlace);
			}
			else
			{
				backTilemap.SetTile(tilemapPos, tileToPlace);
			}
		}
		else
		{
			if (frontTilemap.HasTile(tilemapPos))
			{
				frontTilemap.SetTile(tilemapPos, null);
			}
			else
			{
				backTilemap.SetTile(tilemapPos, null);
			}
		}
	}
}
