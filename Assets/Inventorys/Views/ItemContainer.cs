using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static UnityEngine.UI.Button;
using System;

public class ItemContainer : MonoBehaviour
{
    private Button _button;
	private Image _icon;
	private TextMeshProUGUI _name;
	private TextMeshProUGUI _itemCount;
	private InventoryItem _itemData;
	
	public InventoryItem ItemData => _itemData;
	public ButtonClickedEvent onClick => _button.onClick;
	public int Index { get; set; }

	public int ItemID
	{
		get
		{
			if (_itemData is null)
				return -1;
			else
				return _itemData.ID;	
		}
	}

	private void Awake()
	{
		_button = GetComponent<Button>();
		_icon = GetComponentInChildren<Image>();
		_name = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
		_itemCount = transform.GetChild(2).GetComponent<TextMeshProUGUI>();
	}

	public void SetDataContext(InventoryItem itemData)
	{
		if (itemData is null)
		{
			_itemData = null;
			_icon.sprite = null;
			_name.text = string.Empty;
			_itemCount.text = string.Empty;
			return;
		}

		_itemData = itemData;
		_icon.sprite = itemData.Icon;
		_name.text = itemData.Name;
		_itemCount.text = itemData.Count.ToString();
	}

	public void HideItem()
	{
		Transform[] children = transform.GetChildren();

		foreach (Transform child in children)
		{
			child.gameObject.SetActive(false);
		}
	}
}
