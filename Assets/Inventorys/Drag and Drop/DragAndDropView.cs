using System;
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
    private InventoryItem _selectedItemData;
    private bool _itemSelected;
    private Vector3 _offset;

    public Action<InventoryItem, int> OnMoveItem;

	private void Awake()
	{
        _offset = new Vector3(-1, -1);
        _selectedItemTransfrom.gameObject.layer = 2;
        _selectedItemTransfrom.gameObject.SetActive(false);
        _selectedItemRenderer = _selectedItemTransfrom.GetComponent<Image>();
        _items = new List<ItemContainer>();
        _container = transform.GetChild(0).GetComponent<RectTransform>();
    }

	private void Update()
	{
        if (_selectedItemTransfrom.gameObject.activeSelf && _itemSelected == true)
        {
            _selectedItemTransfrom.position = Input.mousePosition + _offset * 100;
        }
	}

    public void Refresh(IReadOnlyCollection<InventoryItem> data)
    {
        Clear();

        foreach (InventoryItem item in data) 
        {
            AddItem(item);
        }
    }

    public void Clear()
    {
        ItemContainer[] items = _items.ToArray();
        _items.Clear();

        for (int i = 0; i < items.Length; i++)
        {
            Destroy(items[i].gameObject);   
        }
    }

	public void AddItem(InventoryItem item)
    {
        ItemContainer newItem = Instantiate(_itemContainerTemplate, _container);
        newItem.SetDataContext(item);
        newItem.Index = _items.Count;

        newItem.onClick.AddListener(delegate
        {
            if (_itemSelected)
            {
                OnMoveItem?.Invoke(_selectedItemData, newItem.Index);
                _selectedItemTransfrom.gameObject.SetActive(false);
                _selectedContainer = null;
                _selectedItemData = null;
                _itemSelected = false;
                return;
            }

            _selectedContainer = newItem;
			_selectedContainer.HideItem();
            _selectedItemData = newItem.ItemData;

            _itemSelected = true;

            _selectedItemTransfrom.gameObject.SetActive(true);
            _selectedItemRenderer.sprite = _selectedItemData.Icon;
            
        });

        _items.Add(newItem);
    }

	private void SwitchItems(ItemContainer newItem)
	{
		
	}

	public void RemoveItem(InventoryItem item)
    {
        ItemContainer removedItem = _items.Where(x => x.ItemID == item.ID).First();

        removedItem.SetDataContext(null);

        Destroy(removedItem);
    }
}
