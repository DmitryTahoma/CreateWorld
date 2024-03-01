using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class InventoryCell : MonoBehaviour
{
    private Image image;

	[SerializeField] private BlockType blockType;

	public BlockType BlockType => blockType;

	private void Awake()
	{
        image = transform.GetChild(0).GetComponent<Image>();
	}

	private void Start()
	{
		UpdateImage();
	}

	public void UpdateImage()
    {
		if (blockType == BlockType.Air)
		{
			image.sprite = null;
			image.gameObject.SetActive(false);
		}
		else
		{
			image.sprite = ((Tile)WorldInformation.Instance.Tiles[(int)blockType]).sprite;
			image.gameObject.SetActive(true);
		}
	}
}
