using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DragAndDropView : MonoBehaviour
{
    [SerializeField] private ItemContainer _itemContainerTemplate;
    [SerializeField] private Transform _selectedItemTransfrom;

    private Image _selectedItemRenderer;
    private List<ItemContainer> _items;
    private RectTransform _container;
    private ItemContainer _selectedContainer;
    private InventoryItemData _selectedItemData;

	private void Awake()
	{
        _selectedItemTransfrom.gameObject.SetActive(false);
        _selectedItemRenderer = _selectedItemTransfrom.GetComponent<Image>();
        _items = new List<ItemContainer>();
        _container = transform.GetChild(0).GetComponent<RectTransform>();
    }

	private void Update()
	{
        if (_selectedItemTransfrom.gameObject.activeSelf)
        {
            _selectedItemTransfrom.position = Input.mousePosition;
        }
	}

	public void AddItem(InventoryItemData item)
    {
        ItemContainer newItem = Instantiate(_itemContainerTemplate, _container);
        newItem.SetDataContext(item);

        newItem.onClick.AddListener(delegate
        {
            _selectedContainer = newItem;
			_selectedContainer.HideItem();
            _selectedItemData = newItem.ItemData;


            _selectedItemTransfrom.gameObject.SetActive(true);
            _selectedItemRenderer.sprite = _selectedItemData.Icon;
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
