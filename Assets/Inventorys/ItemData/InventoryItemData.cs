using UnityEngine;

namespace Inventorys
{
	[CreateAssetMenu(menuName = "Inventory's/ItemData")]
	public class InventoryItemData : ScriptableObject
	{
		public Sprite Icon;
		public string Name;
		public string Description;
		public int ID;
		public int StackLimit = 10;
		public bool Stackable;
	}

}