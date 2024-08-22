using System.Linq;
using UnityEngine;

namespace Inventorys
{
	public class WeightCapInventory : InventoryBase
	{
		[SerializeField] private float _maxWeight = 10;
		private float _currentWeight;

		protected override bool CanAdd(InventoryItem item)
		{
			bool isOverWight = _currentWeight + item.Weight > _maxWeight;
			bool isNullItem = item.ID == InventoryItem.NULLID;

			if (isOverWight || isNullItem)
				return false;

			return true;
		}

		public override bool AddItem(InventoryItem item)
		{
			if (CanAdd(item) == false)
				return false;

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
			_items.Add(item);

			OnAddItem?.Invoke(item);
			OnChange?.Invoke();

			return true;
		}
	}
}