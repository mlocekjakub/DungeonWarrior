using System.Collections;
using System.Collections.Generic;
using DungeonCrawl.Actors.Characters;
using DungeonCrawl.Core;
using NUnit.Framework;
using NUnit.Framework.Internal;
using UnityEngine;
using UnityEngine.TestTools;

public class ItemTests
{
    private GameObject _gameObject;

    [SetUp]
    public void InitGameObject()
    {
        _gameObject = new GameObject();
    }

    [Test]
    public void SetPickable_BoolIsTrue_BoolIsSet()
    {
        // Arrange
        var item = _gameObject.AddComponent<Bow>();
        bool expectedBool = true;
        
        // Act
        item.SetPickable(true);
        var actualBool = item.GetIfIsPickable();

        Assert.AreEqual(expectedBool, actualBool);
    }

    [Test]
    public void SetPickable_BoolIsFalse_BoolIsSet()
    {
        var item = _gameObject.AddComponent<Bow>();
        bool expectedBool = false;
        
        // Act
        item.SetPickable(false);
        var actualBool = item.GetIfIsPickable();
        
        Assert.AreEqual(expectedBool, actualBool);
    }

    [Test]
    public void OnCollision_AnotherActorIsPlayerAndItemIsPickable_ItemIsInPlayerInventory()
    {
        var item = _gameObject.AddComponent<Sword>();
        var player = _gameObject.AddComponent<Player>();
        player.SetInventory();
        bool isInInventory = false;
        
        // Act
        item.OnCollision(player);
        var playerInventory = player.GetInventory();
        var getIfIsInInventory = item.GetIfIsInInventory();

        foreach(var inventorySlot in playerInventory.GetInventorySlots())
        {
            if (inventorySlot.GetItem() == item)
            {
                isInInventory = true;
            }
        }
        
        Assert.IsTrue(isInInventory);
        Assert.IsTrue(getIfIsInInventory);
    }

    [Test]
    public void OnCollision_AnotherActorIsNotPlayerAndItemIsPickable_ItemIsNotInPlayerInventory()
    {
        var item = _gameObject.AddComponent<Sword>();
        var notPlayer = _gameObject.AddComponent<Bow>();
        bool isInInventory;
        
        // Act
        item.OnCollision(notPlayer);
        isInInventory = item.GetIfIsInInventory();
        
        Assert.IsFalse(isInInventory);
    }

    [Test]
    public void OnCollision_AnotherActorIsPlayerAndItemIsNotPickable_ItemIsNotInPlayerInventory()
    {
        var item = _gameObject.AddComponent<Sword>();
        var player = _gameObject.AddComponent<Player>();
        bool isInInventory;
        item.SetPickable(false);
        
        item.OnCollision(player);
        isInInventory = item.GetIfIsInInventory();
        
        Assert.IsFalse(isInInventory);
    }

    [Test]
    public void OnCollision_AnotherActorIsNotPlayerAndItemIsNotPickable_ItemIsNotInPlayerInventory()
    {
        var item = _gameObject.AddComponent<Sword>();
        item.SetPickable(false);
        var notPlayer = _gameObject.AddComponent<Bow>();
        bool isInInventory;
        
        item.OnCollision(notPlayer);
        isInInventory = item.GetIfIsInInventory();
        
        Assert.IsFalse(isInInventory);
    }
}
