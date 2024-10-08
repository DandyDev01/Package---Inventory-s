using UnityEngine;

namespace Inventorys
{
	public class InventoryItem
	{
		public readonly static int NULLID = -1;

		public Sprite Icon;
		public string Name;
		public string Description;
		public float Weight;
		public int Count;
		public int StackLimit = 10;
		public int ID;
		public bool Stackable;


		public InventoryItem(InventoryItemData data)
		{
			Icon = data.Icon;
			Name = data.Name;
			Description = data.Description;
			ID = data.ID;
			Stackable = data.Stackable;
			StackLimit = data.StackLimit;
		}

		public InventoryItem()
		{
			ID = NULLID;
		}
	}
}