using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
	private GameObject[] buttons;
	private int selectedButton = -1;

	[SerializeField] private Sprite buttonDefault;
	[SerializeField] private Sprite buttonSelected;

	private void Awake()
	{
		buttons = new GameObject[10];
		
		for (int i = 0; i < transform.childCount; i++)
		{
			int index = i;
			buttons[i] = transform.GetChild(i).gameObject;
			buttons[i].GetComponent<Button>().onClick.AddListener(() => { OnButtonClick(index); });
		}
	}

	private void OnButtonClick(int index)
	{
		if (selectedButton == index || selectedButton != -1)
		{
			buttons[selectedButton].GetComponent<Image>().sprite = buttonDefault;
		}

		if (selectedButton != index)
		{
			selectedButton = index;
			buttons[selectedButton].GetComponent<Image>().sprite = buttonSelected;
		}
		else
		{
			selectedButton = -1;
		}
	}

	public BlockType GetSelectedBlockType()
	{
		if (selectedButton == -1) return BlockType.Air;

		return buttons[selectedButton].GetComponent<InventoryCell>().BlockType;
	}
}
