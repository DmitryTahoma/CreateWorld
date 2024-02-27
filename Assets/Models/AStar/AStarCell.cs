using System;
using UnityEngine;

public partial class AStar
{
	private class AStarCell
	{
		public Vector2Int Position;

		public AStarCell()
		{
			G = 10;
			Parent = null;
			Position = Vector2Int.zero;
		}

		public int G { get; set; }
		public AStarCell Parent { get; set; }

		// energiya, zatrachivaemaya na peredvizhenie iz startovoj kletki A v tekushchuyu rassmatrivaemuyu kletku,
		// sleduya najdennomu puti k etoj kletke
		public int GetH(Vector2Int target)
		{
			return 10 * (Math.Abs(target.x - Position.x) + Math.Abs(target.y - Position.y));
		}

		// primernoe kolichestvo energii, zatrachivaemoe na peredvizhenie ot tekushchej kletki do celevoj kletki B.
		// Iznachal'no eta velichina ravna predpolozhitel'nomu znacheniyu, takomu, chto esli by my shli napryamuyu,
		// ignoriruya prepyatstviya (no isklyuchiv diagonal'nye peremeshcheniya). V processe poiska ona korrektiruetsya
		// v zavisimosti ot vstrechayushchihsya na puti pregrad
		public int GetF(Vector2Int target)
		{
			return G + GetH(target);
		}
	}
}