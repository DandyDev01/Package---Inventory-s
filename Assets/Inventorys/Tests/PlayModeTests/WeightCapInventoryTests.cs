using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Inventorys.Tests
{
	public class WeightCapInventoryTests
	{
		private WeightCapInventory _inventory;

		[UnitySetUp]
		public IEnumerator Setup()
		{
			yield return new WaitForSeconds(1);
			var inventoryObject = new GameObject();
			_inventory = inventoryObject.AddComponent<WeightCapInventory>();
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
				ID = 1234,
				Weight = 1
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
				Weight = 3.3f,
				Stackable = true,
				StackLimit = 2
			};

			InventoryItem item1 = new InventoryItem()
			{
				Name = "item 2",
				ID = 1234,
				Weight = 3.3f,
				Stackable = true,
				StackLimit = 2
			};

			InventoryItem item2 = new InventoryItem()
			{
				Name = "item 3",
				ID = 1234,
				Weight = 3.3f,
				Stackable = true,
				StackLimit = 2
			};

			Assert.IsTrue(_inventory.AddItem(item));
			Assert.IsTrue(_inventory.AddItem(item1) && _inventory.Count == 1);
			Assert.IsTrue(_inventory.AddItem(item2) && _inventory.Count == 2);
			Assert.AreEqual(_inventory.CurrentWeight, item2.Weight * 3);

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
				Weight = 3.3f,
				Stackable = true,
				StackLimit = 2
			};

			InventoryItem item1 = new InventoryItem()
			{
				Name = "item 2",
				ID = 1234,
				Weight = 3.3f,
				Stackable = true,
				StackLimit = 2
			};

			InventoryItem item2 = new InventoryItem()
			{
				Name = "item 3",
				ID = 1234,
				Weight = 3.3f,
				Stackable = true,
				StackLimit = 2
			};
			Assert.IsTrue(_inventory.AddItem(item));
			Assert.IsTrue(_inventory.AddItem(item1) && _inventory.Count == 2);
			Assert.IsTrue(_inventory.AddItem(item2) && _inventory.Count == 3);

			yield return null;
		}
	}

}