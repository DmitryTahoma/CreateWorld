using UnityEngine;

public class Character : MonoBehaviour
{
	private void Start()
	{
		UnitManager.Instance.AddUnit(gameObject);
	}
}
