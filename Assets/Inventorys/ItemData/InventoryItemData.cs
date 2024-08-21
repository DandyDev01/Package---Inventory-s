using UnityEngine;

[CreateAssetMenu(menuName = "Inventory's/ItemData")]
public class InventoryItemData : ScriptableObject
{
	public readonly static int NULLID = -1;

	public Sprite Icon;
	public string Name;
	public string Description;
	public int Count;
	public int ID;
	public bool Stackable;
}
