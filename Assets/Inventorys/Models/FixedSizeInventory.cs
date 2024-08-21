using System.Linq;
using UnityEngine;

namespace Inventorys
{
	public class FixedSizeInventory : InventoryBase
	{
		[SerializeField] private int _maxItemsCount = 15;

		public override bool AddItem(InventoryItem item)
		{
			if (_items.Count + 1 > _maxItemsCount)
				return false;

			
			if (_items.Contains(x => x.ID == item.ID))
			{
				InventoryItem i = _items.Where(x => x.ID == item.ID).First();
				int stackLimit = _enforeStackLimit ? _stackLimit : i.StackLimit;

				if (i.Stackable && _enforeStackLimit == false && i.Count < i.StackLimit)
				{ 
					i.Count += 1;
				}
				else if (i.Stackable && _enforeStackLimit && i.Count + 1 < _stackLimit)
				{
					i.Count += 1;
				}
				else
				{
					item.Count = 1;
					_items.Add(item);

					OnAddItem?.Invoke(item);
				}

				OnChange?.Invoke();

				return true;
			}

			item.Count = 1;
			_items.Add(item);

			OnAddItem?.Invoke(item);
			OnChange?.Invoke();

			return true;
		}   
	}
}