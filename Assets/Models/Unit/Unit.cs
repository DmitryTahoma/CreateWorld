using UnityEngine;
using UnityEngine.Tilemaps;

public class Unit
{
	private static int idIncrement = 0;

	public Unit()
	{
		Id = idIncrement++;
	}

	public int Id { get; }
	public GameObject GameObject { get; set; }
	public Tilemap Tilemap { get; set; }
}
