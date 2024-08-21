using Inventorys;
using System;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField]  private InventoryBase _inventory;
	[SerializeField]  private DragAndDropView _inventoryView;

	public InventoryBase Inventory => _inventory;

	private void Awake()
	{
		_inventoryView.Enable += Connect;
		_inventoryView.Disable += Disconnect;
	}

	private void Connect()
	{
		_inventory.OnAddItem += _inventoryView.AddItem;
		_inventory.OnRemoveItem += _inventoryView.RemoveItem;
		_inventory.OnChange += Refresh;
		_inventoryView.OnMoveItem += _inventory.MoveItem;
		Refresh();
	}

	private void Disconnect() 
	{
		_inventory.OnAddItem -= _inventoryView.AddItem;
		_inventory.OnRemoveItem -= _inventoryView.RemoveItem;
		_inventory.OnChange -= Refresh;

		_inventoryView.OnMoveItem -= _inventory.MoveItem;
	}

	private void Refresh()
	{
		_inventoryView.Refresh(_inventory.Items);
	}

	private void OnDestroy()
	{
		Disconnect();
	}
}
