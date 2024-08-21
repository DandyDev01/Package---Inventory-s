using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DragAndDropView : MonoBehaviour
{
    [SerializeField] private ItemContainer _itemContainerTemplate;

    private List<ItemContainer> _items;
    private RectTransform _container;

	private void Awake()
	{
        _items = new List<ItemContainer>();
        _container = transform.GetChild(0).GetComponent<RectTransform>();
	}

	public void AddItem(InventoryItemData item)
    {
        ItemContainer newItem = Instantiate(_itemContainerTemplate, _container);
        newItem.SetDataContext(item);

        newItem.onClick.AddListener(delegate
        {
            Debug.Log("Clicked on " + item.Name);
        });

        _items.Add(newItem);
    }

    public void RemoveItem(InventoryItemData item)
    {
        ItemContainer removedItem = _items.Where(x => x.ItemID == item.ID).First();

        removedItem.SetDataContext(null);

        Destroy(removedItem);
    }
}
