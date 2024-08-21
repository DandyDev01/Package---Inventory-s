using Inventorys;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField]  private InventoryBase _inventory;
	[SerializeField]  private DragAndDropView _inventoryView;

	public InventoryBase inventory => _inventory;

	private void Start()
	{
		_inventory.OnAddItem += _inventoryView.AddItem;
		_inventory.OnRemoveItem += _inventoryView.RemoveItem;
	}

	private void OnDestroy()
	{
		_inventory.OnAddItem -= _inventoryView.AddItem;
		_inventory.OnRemoveItem -= _inventoryView.RemoveItem;
	}
}
