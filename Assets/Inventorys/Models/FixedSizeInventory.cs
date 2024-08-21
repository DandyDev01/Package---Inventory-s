using System.Linq;
using UnityEngine;

namespace Inventorys
{
	public class FixedSizeInventory : InventoryBase
	{
		[SerializeField] private int _maxItemsCount = 15;
		private int _itemCount = 0;
		private int _nearestEmpty = 0;

		public void Awake()
		{
			for (int i = 0; i < _maxItemsCount; i++)
			{
				AddItem(new InventoryItem());
			}
		}

		public override bool AddItem(InventoryItem item)
		{
			if (_itemCount >= _maxItemsCount)
				return false;

			if (item.ID == InventoryItem.NULLID)
			{
				_items.Add(item);
				return false;
			}

			for (int i = 0; i < _maxItemsCount; i++)
			{
				if (_items[i].ID == InventoryItem.NULLID)
				{
					_nearestEmpty = i;
					break;
				}
			}

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
					_items[_nearestEmpty] = item;
					_itemCount += 1;

					OnAddItem?.Invoke(item);
				}

				OnChange?.Invoke();

				return true;
			}

			item.Count = 1;
			_items[_nearestEmpty] = item;
			_itemCount += 1;

			OnAddItem?.Invoke(item);
			OnChange?.Invoke();

			return true;
		}   
	}
}