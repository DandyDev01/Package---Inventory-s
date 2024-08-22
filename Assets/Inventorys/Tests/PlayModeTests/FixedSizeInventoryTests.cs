using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Inventorys.Tests
{
	public class FixedSizeInventoryTests
	{
		private FixedSizeInventory _inventory;

		[UnitySetUp]
		public IEnumerator Setup()
		{
			yield return new WaitForSeconds(1);
			var inventoryObject = new GameObject();
			_inventory = inventoryObject.AddComponent<FixedSizeInventory>();
		}

		[UnityTest]
		public IEnumerator AddNullItemTest()
		{
			Assert.IsFalse(_inventory.AddItem(new InventoryItem()));	

			yield return null;
		}

		[UnityTest]
		public IEnumerator AddNewItemTest()
		{
			InventoryItem item = new InventoryItem()
			{
				Name = "item 1",
				ID = 1234
			};

			Assert.IsTrue(_inventory.AddItem(item));

			yield return null;
		}

		[UnityTest]
		public IEnumerator AddStackableNewItemTest()
		{
			InventoryItem item = new InventoryItem()
			{
				Name = "item 1",
				ID = 1234,
				Stackable = true,
				StackLimit = 2
			};

			Assert.IsTrue(_inventory.AddItem(item));
			Assert.IsTrue(_inventory.AddItem(item) && _inventory.Count == 1);
			Assert.IsTrue(_inventory.AddItem(item) && _inventory.Count == 2);

			yield return null;
		}

		[UnityTest]
		public IEnumerator EnforceInventoryStackLimit()
		{
			_inventory.EnforeStacklimit(true);

			InventoryItem item = new InventoryItem()
			{
				Name = "item 1",
				ID = 1234,
				Stackable = true,
				StackLimit = 2
			};

			Assert.IsTrue(_inventory.AddItem(item));
			Assert.IsTrue(_inventory.AddItem(item) && _inventory.Count == 2);
			Assert.IsTrue(_inventory.AddItem(item) && _inventory.Count == 3);

			yield return null;
		}
	}

}