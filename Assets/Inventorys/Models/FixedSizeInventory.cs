using System.Linq;
using UnityEngine;

namespace Inventorys
{
	public class FixedSizeInventory : InventoryBase
	{
		[SerializeField] private int _maxItemsCount = 15;

		public override bool AddItem(InventoryItemData item)
		{
			if (_items.Count + 1 > _maxItemsCount)
				return false;


			if (_items.Contains(x => x.ID == item.ID))
			{
				InventoryItemData i = _items.Where(x => x.ID == item.ID).First();
				if (i.Stackable)
					i.Count += 1;
			}
			else
			{
				_items.Add(item);
			}

			OnAddItem?.Invoke(item);
			OnChange?.Invoke();

			return true;
		}
	}
}