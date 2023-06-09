﻿using FightWasteConsole.DataAccess;
using FightWasteConsole.Models;
using FightWasteConsole.Repositories;
using System.Diagnostics.CodeAnalysis;

namespace FightWasteConsoleTests.Repositories;

[ExcludeFromCodeCoverage]
[TestFixture]
public class ModelRepositoryTests
{
    private IDataAccess<MealModel> _dataAccess;
    private ModelRepository<MealModel> _modelRepository;

    [SetUp]
    public void SetUp()
    {
        _dataAccess = A.Fake<IDataAccess<MealModel>>();
        A.CallTo(() => _dataAccess.GetData()).Returns(GetFakeMealData());

        _modelRepository = new(_dataAccess);
    }

    [Test]
    public void GetAllReturnsCollectionOfExpectedModels()
    {
        // Arrange
        var expected = GetFakeMealData();

        // Act
        var actual = _modelRepository.GetAll();

        // Assert
        actual.Should().BeEquivalentTo(expected);
    }

    private IEnumerable<MealModel> GetFakeMealData() => new List<MealModel>
    {
        new()
        {
            Id = 1,
            Name = "Test Meal",
            Ingredients = new()
            {
                new() { Name = "Mushrooms", Quantity = 50, Unit = Unit.G },
                new() { Name = "Watermelon", Quantity = 1, Unit = Unit.Of }
            }
        },
        new()
        {
            Id = 2,
            Name = "Average Omelette",
            Ingredients = new()
            {
                new() { Name = "Egg", Quantity = 3, Unit = Unit.Of },
                new() { Name = "Ham", Quantity = 30, Unit = Unit.G },
                new() { Name = "Cheese", Quantity = 30, Unit = Unit.G }
            }
        }
    };
}
