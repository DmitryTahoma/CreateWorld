using System.Collections.Generic;
using UnityEngine;

public class WalkingTask : ITask
{
	public WalkingTask(Vector2Int targetPosition)
	{
		Type = TaskType.Walking;
		TargetPosition = targetPosition;
	}

	public Unit Unit { get; set; }
	public TaskType Type { get; }
	public TaskState State { get; set; }
	public Vector2Int TargetPosition { get; set; }
	public List<Vector2Int> Way { get; set; }
}
