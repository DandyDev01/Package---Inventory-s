using UnityEngine;

public class AddItem : MonoBehaviour
{
	[SerializeField] private InventoryController _controller;
	[SerializeField] private InventoryItemData[] _items;

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
			_controller.inventory.AddItem(_items.RandomElement());


	}
}
