using System;
using DungeonCrawl;
using DungeonCrawl.Actors;
using DungeonCrawl.Actors.Characters;
using DungeonCrawl.Actors.Static;
using DungeonCrawl.Core;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class UtilitiesTests
{
    [Test]
    public void ToVector_DirectionUp_ReturnsExpected()
    {
        // Arrange
        var expected = (0, 1);
        
        // Act
        var actual = Utilities.ToVector(Direction.Up);

        // Assert
        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void ToVector_DirectionDown_ReturnsExpected()
    {
        // Arrange
        var expected = (0, -1);

        // Act
        var actual = Utilities.ToVector(Direction.Down);

        // Assert
        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void ToVector_DirectionLeft_ReturnsExpected()
    {
        // Arrange
        var expected = (-1, 0);
        
        // Act
        var actual = Utilities.ToVector(Direction.Left);

        // Assert
        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void ToVector_DirectionRight_ReturnsExpected()
    {
        // Arrange
        var expected = (1, 0);
        
        // Act
        var actual = Utilities.ToVector(Direction.Right);

        // Assert
        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void ToVector_InvalidDirection_ThrowsException()
    {
        // Arrange
        TestDelegate result = () => Utilities.ToVector(Direction.None);

        // Act
        // Assert
        Assert.Throws<ArgumentOutOfRangeException>(result);
    }
}
