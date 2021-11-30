using System.Collections;
using System.Collections.Generic;
using DungeonCrawl.Actors.Characters;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class ArmorTests
{
    private GameObject _gameObject;

    [SetUp]
    public void InitGameObject()
    {
        _gameObject = new GameObject();
    }
    
    [Test]
    public void GetValueFromArmorSpriteBank_GetNoArmorSprite_ReturnsExpected()
    {
        // Arrange
        var armor = _gameObject.AddComponent<Armor>();
        int expectedSpriteId = 1;
        string spriteName = "NoArmor";

        // Act
        int actualSpriteId = armor.GetValueFromArmorSpriteBank(spriteName);

        // Assert
        Assert.AreEqual(expectedSpriteId, actualSpriteId);
    }
    
    [Test]
    public void GetValueFromArmorSpriteBank_GetLeatherSprite_ReturnsExpected()
    {
        // Arrange
        var armor = _gameObject.AddComponent<Armor>();
        int expectedSpriteId = 85;
        string spriteName = "Leather";

        // Act
        int actualSpriteId = armor.GetValueFromArmorSpriteBank(spriteName);

        // Assert
        Assert.AreEqual(expectedSpriteId, actualSpriteId);
    }
    
    [Test]
    public void GetValueFromArmorSpriteBank_GetPlateSprite_ReturnsExpected()
    {
        // Arrange
        var armor = _gameObject.AddComponent<Armor>();
        int expectedSpriteId = 80;
        string spriteName = "Plate";

        // Act
        int actualSpriteId = armor.GetValueFromArmorSpriteBank(spriteName);

        // Assert
        Assert.AreEqual(expectedSpriteId, actualSpriteId);
    }
    
    [Test]
    public void GetValueFromArmorSpriteBank_InvalidSpriteName_ThrowsException()
    {
        // Arrange
        var armor = _gameObject.AddComponent<Armor>();

        // Act
        TestDelegate exeption = () => armor.GetValueFromArmorSpriteBank("NoName");

        // Assert
        Assert.Catch(exeption);
    }
    
    [Test]
    public void SetDefence_PositiveInt_DefenceIsSet()
    {
        // Arrange
        var armor = _gameObject.AddComponent<Armor>();
        int expectedDefenceValue = 60;

        // Act
        armor.SetDefence(60);

        // Assert
        Assert.AreEqual(expectedDefenceValue, armor.GetDefence());
    }
    
    [Test]
    public void SetDefence_NegativeInt_DefenceIsNotSet()
    {
        // Arrange
        var armor = _gameObject.AddComponent<Armor>();

        // Act
        armor.SetDefence(-60);
        var defence = armor.GetDefence();

        // Assert
        Assert.AreEqual(0, defence);
    }
}