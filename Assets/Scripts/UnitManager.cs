using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance { get; private set; }

	private Dictionary<GameObject, Tilemap> units;

    [SerializeField] private bool renderWays = true;
    [SerializeField] private GameObject grid;
	[SerializeField] private TileBase pointTile;
	[SerializeField] private TileBase arrowTile;
	[SerializeField] private Tilemap frontTilemap;

	private void Awake()
	{
		Instance = this;
		units = new Dictionary<GameObject, Tilemap>();
	}

	public void AddUnit(GameObject unit)
	{
		Tilemap tilemap = null;

		if (renderWays)
		{
			GameObject tilemapObject = new GameObject("WAY_" + unit.name);
			tilemapObject.transform.parent = grid.transform;
			tilemap = tilemapObject.AddComponent<Tilemap>();
			tilemapObject.AddComponent<TilemapRenderer>();

			List<Vector2Int> way = AStar.FindWay(Vector2Int.zero, new Vector2Int(10, 3)/*,
				(vector2Int) => { return frontTilemap.HasTile(new Vector3Int(vector2Int.x, vector2Int.y)); }*/);

			foreach(Vector2Int vec in way)
			{
				tilemap.SetTile(new Vector3Int(vec.x, vec.y), pointTile);
			}
		}

		units.Add(unit, tilemap);
	}
}
