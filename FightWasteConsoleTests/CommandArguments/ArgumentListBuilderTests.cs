using FightWasteConsole.CommandArguments;
using System.Diagnostics.CodeAnalysis;

namespace FightWasteConsoleTests.CommandArguments;

[ExcludeFromCodeCoverage]
[TestFixture]
public class ArgumentListBuilderTests
{
    private ArgumentListBuilder _builder;

    [SetUp]
    public void SetUp()
    {
        _builder = new ArgumentListBuilder();
    }

    // TODO It gives list of arguments when given correct format
    // TODO It throws error when given incorrect format i.e., -n is valid but --n is not
    // TODO It reads arguments not enclosed in '' as multiple argument values
    // TODO It reads arguments enclosed in '' as a single argument value

    [Test]
    public void BuildReturnsListOfOneArgumentWhenGivenASingleArgument()
    {
        // Arrange
        var expected = new List<Argument>
        {
            new()
            {
                ArgumentName = "n",
                ArgumentValues = new() { "1" }
            }
        };

        // Act
        var actual = _builder.Build("meals -n 1");

        // Assert
        actual.Should().BeEquivalentTo(expected);
    }

    [Test]
    public void BuildReturnsListOfMultipleArgumentsWhenGivenStringWithMultiple()
    {
        // Arrange
        var expected = new List<Argument>
        {
            new()
            {
                ArgumentName = "n",
                ArgumentValues = new() { "5" }
            },
            new()
            {
                ArgumentName = "bob",
                ArgumentValues = new() { "hope" }
            }
        };

        // Act
        var actual = _builder.Build("meals -n 5 -bob hope");

        // Assert
        actual.Should().BeEquivalentTo(expected);
    }

    [Test]
    public void BuildReadsArgumentValuesNotEnclosedInQuotesAsMultipleValues()
    {
        // Arrange
        var expected = new List<Argument>
        {
            new()
            {
                ArgumentName = "names",
                ArgumentValues = new() { "carrot", "pea", "okra" }
            }
        };

        // Act
        var actual = _builder.Build("findingredients -names carrot pea okra");

        // Assert
        actual.Should().BeEquivalentTo(expected);
    }

    [Test]
    public void BuildReadsArgumentValuesEnclosedInQuotesAsSingleValue()
    {
        // Arrange
        var expected = new List<Argument>
        {
            new()
            {
                ArgumentName = "name",
                ArgumentValues = new() { "Sunday Roast" }
            }
        };

        // Act
        var actual = _builder.Build("findmeal -name 'Sunday Roast'");

        // Assert
        actual.Should().BeEquivalentTo(expected);
    }

    [Test]
    public void BuildThrowsErrorWhenGivenArgumentsInWrongFormat()
    {
        // Assert
        Assert.Throws<InvalidOperationException>(() => _builder.Build("findmeal --name 'Sunday Roast'"));
    }
}
