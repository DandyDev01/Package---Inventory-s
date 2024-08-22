using System.Linq;
using UnityEngine;

namespace Inventorys
{
	public class WeightCapInventory : InventoryBase
	{
		[SerializeField] private float _maxWeight = 10;

		public float CurrentWeight => CalculateWeight();

		private float CalculateWeight()
		{
			float weight = 0;

			foreach (var item in _items)
			{
				weight += item.Weight;
			}

			return weight;
		}

		protected override bool CanAdd(InventoryItem item)
		{
			bool isOverWight = CurrentWeight + item.Weight > _maxWeight;
			bool isNullItem = item.ID == InventoryItem.NULLID;
			bool contains = _items.Contains(item);

			if (isOverWight || isNullItem || contains)
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
						i.Weight += item.Weight;
						OnChange?.Invoke();
						return true;
					}
				}
			}

			item.Count = 1;
			_items.Add(item);

			OnAddItem?.Invoke(item);
			OnChange?.Invoke();

			return true;
		}
	}
}