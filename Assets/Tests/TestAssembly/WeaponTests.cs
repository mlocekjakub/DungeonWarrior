using System.Collections;
using System.Collections.Generic;
using DungeonCrawl.Actors.Characters;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class WeaponTests
{
    private GameObject _gameObject;

    [SetUp]
    public void InitGameObject()
    {
        _gameObject = new GameObject();
    }
    
    [Test]
    [Category("Weapon")]
    public void SetDamage_PositiveInt_DamageIsSet()
    {
        // Arrange
        var weapon = _gameObject.AddComponent<Fist>();
        var expectedDamage = 100;
        
        // Act
        weapon.SetDamage(100);
        var actualDamage = weapon.GetDamage();
        
        // Assert
        Assert.AreEqual(expectedDamage, actualDamage);
    }

    [Test]
    [Category("Weapon")]
    public void SetDamage_NegativeInt_DamageIsNotSet()
    {
        // Arrange
        var weapon = _gameObject.AddComponent<Bow>();
        
        // Act
        weapon.SetDamage(-100);
        var damage = weapon.GetDamage();
        
        // Assert
        Assert.AreEqual(0, damage);
    }
}
