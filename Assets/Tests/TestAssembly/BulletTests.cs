using System.Collections;
using System.Collections.Generic;
using DungeonCrawl;
using DungeonCrawl.Actors.Characters;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class BulletTests
{
    private GameObject _gameObject;

    [SetUp]
    public void InitGameObject()
    {
        _gameObject = new GameObject();
    }
    
    [Test]
    public void SetDirection_DirectionUp_DirectionIsSet()
    {
        // Arrange
        _gameObject.AddComponent<SpriteRenderer>();
        var bullet = _gameObject.AddComponent<Bullet>();

        Direction expectedDirection = Direction.Up;

        // Act
        bullet.SetDefaultSprite("Arrow");
        bullet.SetDirection(Direction.Up);
        var actualDirection = bullet.direction;

        // Assert
        Assert.AreEqual(expectedDirection, actualDirection);
    }

    [Test]
    public void SetDirection_DirectionDown_DirectionIsSet()
    {
        // Arrange
        _gameObject.AddComponent<SpriteRenderer>();
        var bullet = _gameObject.AddComponent<Bullet>();
        Direction expectedDirection = Direction.Down;

        // Act
        bullet.SetDefaultSprite("Bone");
        bullet.SetDirection(Direction.Down);
        var actualDirection = bullet.direction;

        // Assert
        Assert.AreEqual(expectedDirection, actualDirection);
    }

    [Test]
    public void SetDirection_DirectionLeft_DirectionIsSet()
    {
        // Arrange
        _gameObject.AddComponent<SpriteRenderer>();
        var bullet = _gameObject.AddComponent<Bullet>();
        Direction expectedDirection = Direction.Left;

        // Act
        bullet.SetDefaultSprite("BlobProjectile");
        bullet.SetDirection(Direction.Left);
        var actualDirection = bullet.direction;

        //Assert
        Assert.AreEqual(expectedDirection, actualDirection);
    }

    [Test]
    public void SetDirection_DirectionRight_DirectionIsSet()
    {
        // Arrange
        _gameObject.AddComponent<SpriteRenderer>();
        var bullet = _gameObject.AddComponent<Bullet>();
        Direction expectedDirection = Direction.Right;

        // Act
        bullet.SetDefaultSprite("Fire");
        bullet.SetDirection(Direction.Right);
        var actualDirection = bullet.direction;

        //Assert
        Assert.AreEqual(expectedDirection, actualDirection);
    }

    [Test]
    public void SetDirection_InvalidDirection_DirectionStaysUp()
    {
        // Arrange
        _gameObject.AddComponent<SpriteRenderer>();
        var bullet = _gameObject.AddComponent<Bullet>();
        Direction expectedDirection = Direction.Up;
        
        // Act
        bullet.SetDefaultSprite("Fire");
        bullet.SetDirection(Direction.None);
        var actualDirection = bullet.direction;
        
        //Assert
        Assert.AreEqual(expectedDirection, actualDirection);
    }
}
