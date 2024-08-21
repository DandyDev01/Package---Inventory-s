using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Inventorys
{
	public abstract class InventoryBase : MonoBehaviour
	{
		protected List<InventoryItemData> _items;
		
		public Action<InventoryItemData> OnAddItem;
		public Action<InventoryItemData> OnRemoveItem;
		public Action OnChange;

		protected virtual void Awake()
		{
			_items = new List<InventoryItemData>();
		}

		public abstract bool AddItem(InventoryItemData item);

		public bool RemoveItem(InventoryItemData item)
		{
			return RemoveItemByID(item.ID);
		}

		public bool RemoveItem(int index)
		{
			if (index >= _items.Count || index < 0)
				return false;

			InventoryItemData removedItem = _items[index]; 
			_items.RemoveAt(index);

			OnRemoveItem?.Invoke(removedItem);
			OnChange?.Invoke();

			return true;
		}

		public bool RemoveItemByID(int id)
		{
			if (_items.Any() == false || _items.Any(x => x.ID == id)
				|| id <= InventoryItemData.NULLID)
			{
				return false;
			}

			InventoryItemData[] items = _items.Where(x => x.ID == id).ToArray();

			InventoryItemData removedItem = items.First();
			_items.Remove(removedItem);

			OnRemoveItem?.Invoke(removedItem);
			OnChange?.Invoke();

			return true;
		}

		public bool MoveItem(InventoryItemData item, int index)
		{
			if (index >= _items.Count)
				return false;

			int indexOfItemToMove = _items.IndexOf(item);
			InventoryItemData itemAtIndex = _items[index];
			InventoryItemData temp = itemAtIndex;
			_items[index] = item;
			_items[indexOfItemToMove] = itemAtIndex;

			OnChange?.Invoke();

			return true;
		}
	}

}