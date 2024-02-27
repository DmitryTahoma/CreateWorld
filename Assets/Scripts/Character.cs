using UnityEngine;

public class Character : MonoBehaviour
{
    void Start()
    {
        UnitManager.Instance.AddUnit(gameObject);
    }
}
