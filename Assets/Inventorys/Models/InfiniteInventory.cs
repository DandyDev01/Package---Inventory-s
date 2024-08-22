using System.Linq;

namespace Inventorys
{
	public class InfiniteInventory : InventoryBase
	{
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

		protected override bool CanAdd(InventoryItem item)
		{
			return true;
		}
	}
}