using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class UnitManager : MouseClickableBase
{
	public static UnitManager Instance { get; private set; }

	private List<Unit> units;
	private Vector2Int tempStartAStar;
	private Vector2Int tempEndAStar;

	[SerializeField] private bool renderWays = true;
	[SerializeField] private GameObject grid;
	[SerializeField] private TileBase pointTile;
	[SerializeField] private TileBase arrowTile;
	[SerializeField] private Tilemap frontTilemap;

	private void Awake()
	{
		Instance = this;
		units = new List<Unit>();
	}

	public void AddUnit(GameObject gameObject)
	{
		Unit unit = new Unit
		{
			GameObject = gameObject
		};
		gameObject.name = $"[{unit.Id}] Unit";

		GameObject tilemapObject = new GameObject("WAY_" + gameObject.name);
		tilemapObject.SetActive(renderWays);
		tilemapObject.transform.parent = grid.transform;
		unit.Tilemap = tilemapObject.AddComponent<Tilemap>();
		tilemapObject.AddComponent<TilemapRenderer>();

		units.Add(unit);
	}

	public override void OnClick(InputAction.CallbackContext context)
	{
		if (!context.canceled) return;

		Unit unit = units[0];
		Tilemap tilemap = unit.Tilemap;
		tilemap.ClearAllTiles();

		Vector3Int startVec3 = frontTilemap.WorldToCell(unit.GameObject.transform.position);
		tempStartAStar = new Vector2Int(startVec3.x, startVec3.y);

		Vector3Int endVec3 = frontTilemap.WorldToCell(GameInput.Instance.GetMouseWorldPosition());
		tempEndAStar = new Vector2Int(endVec3.x, endVec3.y);

		List<Vector2Int> way = AStar.FindWay(tempStartAStar, tempEndAStar, CheckPosition);

		if (way.Count > 0)
		{
			for (int i = way.Count - 1; i >= 0; --i)
			{
				Vector2Int vec = way[i];
				Vector3Int position = new Vector3Int(vec.x, vec.y);
				if (i == 0)
				{
					tilemap.SetTile(position, pointTile);
				}
				else
				{
					Vector2Int vecNext = way[i - 1];
					tilemap.SetTile(position, arrowTile);
					float angle = 0f;
					switch (vec - vecNext)
					{
						case { x: 1, y: 0 }: angle = 180f; break;
						case { x: 0, y: 1 }: angle = 270f; break;
						case { x: 0, y: -1 }: angle = 90f; break;
					}
					Matrix4x4 matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0, 0, angle), Vector3.one);
					tilemap.SetTransformMatrix(position, matrix);
				}
			}
		}

		tempStartAStar.Set(0, 0);
		tempEndAStar.Set(0, 0);

		Handled = true;
	}

	private bool CheckPosition(Vector2Int position)
	{
		if (tempStartAStar == position) return true;

		Vector3Int vec3 = new Vector3Int(position.x, position.y);
		if (frontTilemap.HasTile(vec3)) return false;

		return frontTilemap.HasTile(vec3 + new Vector3Int(0, -1))
			|| frontTilemap.HasTile(vec3 + new Vector3Int(-1, -1))
			|| frontTilemap.HasTile(vec3 + new Vector3Int(1, -1));
	}
}
