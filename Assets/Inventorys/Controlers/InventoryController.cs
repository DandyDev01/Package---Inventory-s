using Inventorys;
using System;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField]  private InventoryBase _inventory;
	[SerializeField]  private DragAndDropView _inventoryView;

	public InventoryBase Inventory => _inventory;

	private void Start()
	{
		_inventory.OnAddItem += _inventoryView.AddItem;
		_inventory.OnRemoveItem += _inventoryView.RemoveItem;
		_inventory.OnChange += Refresh;

		_inventoryView.OnMoveItem += _inventory.MoveItem;
	}

	private void Refresh()
	{
		_inventoryView.Refresh(_inventory.Items);
	}

	private void OnDestroy()
	{
		_inventory.OnAddItem -= _inventoryView.AddItem;
		_inventory.OnRemoveItem -= _inventoryView.RemoveItem;
		_inventory.OnChange -= Refresh;

		_inventoryView.OnMoveItem -= _inventory.MoveItem;
	}
}
