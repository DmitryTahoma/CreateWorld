using System.Collections.Generic;
using UnityEngine;

public partial class AStar
{
	private const int maxSqrMagnitude = 1000;

	public static List<Vector2Int> FindWay(Vector2Int start, Vector2Int end, AStarCondition condition = null)
	{
		List<AStarCell> openList = new List<AStarCell>();
		List<AStarCell> closedList = new List<AStarCell>();
		List<Vector2Int> way = new List<Vector2Int>();

		AStarCell cell = new AStarCell();
		cell.Position = start;
		openList.Add(cell);

		while (openList.Count > 0)
		{
			for (int sorted_size = 1; sorted_size < openList.Count; sorted_size++)
			{
				if (openList[sorted_size].GetF(end) < openList[sorted_size - 1].GetF(end))
				{
					AStarCell tmp = openList[sorted_size];

					int idx = sorted_size;
					for (; idx != 0 && tmp.GetF(end) < openList[idx - 1].GetF(end); idx--)
					{
						openList[idx] = openList[idx - 1];
					}
					openList[idx] = tmp;
				}
			}

			AStarCell min = openList[0];
			openList.RemoveAt(0);
			closedList.Add(min);

			if ((start - min.Position).sqrMagnitude > maxSqrMagnitude)
			{
				return way;
			}

			if (min.GetH(end) == 0)
			{
				AStarCell it = min;
				way.Add(it.Position);

				do
				{
					it = it.Parent;
					way.Add(it.Position);

				} while (it.Parent != null);

				break;
			}
			else
			{
				AStarCell left = new AStarCell();
				left.Position = min.Position;
				left.Position.x--;
				left.G += min.G;
				left.Parent = min;
				if (!HasElement(openList, left)
					&& !HasElement(closedList, left)
					&& (condition == null || condition(left.Position)))
				{
					openList.Add(left);
				}

				AStarCell right = new AStarCell();
				right.Position = min.Position;
				right.Position.x++;
				right.G += min.G;
				right.Parent = min;
				if (!HasElement(openList, right)
					&& !HasElement(closedList, right)
					&& (condition == null || condition(right.Position)))
				{
					openList.Add(right);
				}

				AStarCell top = new AStarCell();
				top.Position = min.Position;
				top.Position.y--;
				top.G += min.G;
				top.Parent = min;
				if (!HasElement(openList, top)
					&& !HasElement(closedList, top)
					&& (condition == null || condition(top.Position)))
				{
					openList.Add(top);
				}

				AStarCell bottom = new AStarCell();
				bottom.Position = min.Position;
				bottom.Position.y++;
				bottom.G += min.G;
				bottom.Parent = min;
				if (!HasElement(openList, bottom)
					&& !HasElement(closedList, bottom)
					&& (condition == null || condition(bottom.Position)))
				{
					openList.Add(bottom);
				}
			}
		}

		return way;
	}

	private static bool HasElement(List<AStarCell> list, AStarCell elem)
	{
		foreach (var e in list)
		{
			if (e.Position == elem.Position)
			{
				return true;
			}
		}
		return false;
	}
}