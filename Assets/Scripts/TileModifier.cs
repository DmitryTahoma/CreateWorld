using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class TileModifier : MonoBehaviour
{
	private Camera mainCamera;

	[SerializeField] private Tilemap tilemap;
	[SerializeField] private TileBase tileToPlace;

	private void Awake()
	{
		mainCamera = Camera.main;
	}

	public void OnClick(InputAction.CallbackContext context)
	{
		if (!context.started) return;

		TileBase tile = null;
		if (context.control.displayName == "Right Button")
		{
			tile = tileToPlace;
		}
		Vector3 worldPos = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
		Vector3Int tilemapPos = tilemap.WorldToCell(worldPos);
		tilemap.SetTile(tilemapPos, tile);
	}
}
