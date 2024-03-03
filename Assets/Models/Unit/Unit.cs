using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Unit
{
	private static int idIncrement = 0;

	private GameObject gameObject = null;

	public Unit()
	{
		Id = idIncrement++;
	}

	public int Id { get; }
	public GameObject GameObject
	{
		get => gameObject;
		set
		{
			gameObject = value;
			if (value == null)
			{
				Script = null;
			}
			else
			{
				Script = gameObject.GetComponent<Character>();
			}
		}
	}

	public Character Script { get; private set; }
	public Tilemap Tilemap { get; set; }
	public ITask CurrentTask { get; set; }

	public void Update()
	{
		WalkingTask walking = CurrentTask as WalkingTask;

		if (walking != null)
		{
			Vector3 unitPosition = GameObject.transform.position;

			Vector3Int startVec3 = Tilemap.WorldToCell(unitPosition);
			Vector2Int tempStartAStar = new Vector2Int(startVec3.x, startVec3.y);
			Vector2Int tempEndAStar = new Vector2Int(walking.TargetPosition.x, walking.TargetPosition.y);

			walking.Way = AStar.FindWay(tempStartAStar, tempEndAStar);

			if (walking.Way.Count > 1)
			{
				Vector2Int cellTarget2D = walking.Way[^2];
				Vector3Int cellTarget3D = new Vector3Int(cellTarget2D.x, cellTarget2D.y);
				Vector3 worldTarget = Tilemap.CellToWorld(cellTarget3D);

				const float speed = 1f;

				Vector3 direction = worldTarget - unitPosition;

				float distanceToTarget = direction.magnitude;

				if (distanceToTarget <= speed * Time.deltaTime)
				{
					unitPosition = worldTarget;
				}
				else
				{
					direction.Normalize();
					Vector3 velocity = direction * speed * Time.deltaTime;
					unitPosition += velocity;
				}

				GameObject.transform.position = unitPosition;
			}
		}
	}
}
