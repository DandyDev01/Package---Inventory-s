using UnityEngine;

namespace Inventorys
{
	public class FixedSizeInventory : InventoryBase
	{
		[SerializeField] private int _maxItemsCount = 15;

		public override bool AddItem(InventoryItemData item)
		{
			if (_items.Contains(item) || _items.Contains(x => x.ID == item.ID)) 
				return false;

			if (_items.Count + 1 > _maxItemsCount)
				return false;

			_items.Add(item);

			OnAddItem?.Invoke(item);
			OnChange?.Invoke();

			return true;
		}
	}
}