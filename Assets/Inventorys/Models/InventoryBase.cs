using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Inventorys
{
	public abstract class InventoryBase : MonoBehaviour
	{
		[SerializeField] protected int _stackLimit = 10;
		[SerializeField] protected bool _enforeStackLimit = false;

		protected List<InventoryItem> _items = new List<InventoryItem>();

		public IReadOnlyCollection<InventoryItem> Items => _items;

		public Action<InventoryItem> OnAddItem;
		public Action<InventoryItem> OnRemoveItem;
		public Action OnChange;

		public abstract bool AddItem(InventoryItem item);

		public bool RemoveItem(InventoryItemData item)
		{
			return RemoveItemByID(item.ID);
		}

		public bool RemoveItem(int index)
		{
			if (index >= _items.Count || index < 0)
				return false;

			InventoryItem removedItem = _items[index]; 
			_items.RemoveAt(index);

			OnRemoveItem?.Invoke(removedItem);
			OnChange?.Invoke();

			return true;
		}

		public bool RemoveItemByID(int id)
		{
			if (_items.Any() == false || _items.Any(x => x.ID == id)
				|| id <= InventoryItem.NULLID)
			{
				return false;
			}

			InventoryItem[] items = _items.Where(x => x.ID == id).ToArray();

			InventoryItem removedItem = items.First();
			_items.Remove(removedItem);

			OnRemoveItem?.Invoke(removedItem);
			OnChange?.Invoke();

			return true;
		}

		public void MoveItem(InventoryItem item, int index)
		{
			if (index >= _items.Count)
				return;

			int indexOfItemToMove = _items.IndexOf(item);
			InventoryItem itemAtIndex = _items[index];
			InventoryItem temp = itemAtIndex;
			_items[index] = item;
			_items[indexOfItemToMove] = itemAtIndex;

			OnChange?.Invoke();
		}
	}
}