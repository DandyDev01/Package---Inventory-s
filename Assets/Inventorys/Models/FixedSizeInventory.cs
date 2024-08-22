using System.Linq;
using UnityEngine;

namespace Inventorys
{
	public class FixedSizeInventory : InventoryBase
	{
		[SerializeField] private int _maxItemsCount = 15;
		private int _itemCount = 0;

		public int Count => _itemCount;

		public void Awake()
		{
			for (int i = 0; i < _maxItemsCount; i++)
			{
				AddItem(new InventoryItem());
			}
		}

		protected override bool CanAdd(InventoryItem item)
		{
			if (_itemCount >= _maxItemsCount || (_items.Count + 1 > _maxItemsCount && item.ID == InventoryItem.NULLID))
				return false;

			if (item.ID == InventoryItem.NULLID)
			{
				_items.Add(item);
				return true;
			}

			return true;
		}

		public override bool AddItem(InventoryItem item)
		{
			if (_itemCount >= _maxItemsCount || (_items.Count + 1 > _maxItemsCount && item.ID == InventoryItem.NULLID))
				return false;

			if (item.ID == InventoryItem.NULLID)
			{
				_items.Add(item);
				return true;
			}

			if (_items.Contains(x => x.ID == item.ID && x.Stackable))
			{
				InventoryItem[] items = _items.Where(x => x.ID == item.ID).ToArray();

				foreach (var i in items)
				{
					int stackLimit = _enforeStackLimit ? _stackLimit : i.StackLimit;
					if (i.Count < stackLimit)
					{
						i.Count += 1;
						OnChange?.Invoke();
						return true;
					}
				}
			}

			int nearestEmptyIndex = GetNearestEmptyIndex();

			if (nearestEmptyIndex == -1)
				return false;

			item.Count = 1;
			_items[nearestEmptyIndex] = item;
			_itemCount += 1;

			OnAddItem?.Invoke(item);
			OnChange?.Invoke();

			return true;
		}
	}
}