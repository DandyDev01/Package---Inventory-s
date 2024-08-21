using UnityEngine;

public class AddItem : MonoBehaviour
{
	[SerializeField] private InventoryController _controller;
	[SerializeField] private InventoryItemData[] _items;

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			Debug.Log("Press");
  			_controller.Inventory.AddItem(new InventoryItem(_items.RandomElement()));
		}


	}
}
