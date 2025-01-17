using System.Text.RegularExpressions;

namespace Throw.UnitTests.ValidatableExtensions;

[TestClass]
public class StringsTests
{
    [TestMethod]
    public void ThrowIfWhiteSpace_WhenWhiteSpace_ShouldThrow()
    {
        // Arrange
        string value = " ";

        // Act
        Action action = () => value.Throw().IfWhiteSpace();

        // Assert
        action.Should()
            .ThrowExactly<ArgumentException>()
            .WithMessage($"String should not be white space only. (Parameter '{nameof(value)}')");
    }

    [TestMethod]
    public void ThrowIfWhiteSpace_WhenNotWhiteSpace_ShouldNotThrow()
    {
        // Arrange
        string value = "not white space";

        // Act
        Action action = () => value.Throw().IfWhiteSpace();

        // Assert
        action.Should().NotThrow();
    }

    [TestMethod]
    public void ThrowIfEmpty_WhenEmpty_ShouldThrow()
    {
        // Arrange
        string value = "";

        // Act
        Action action = () => value.Throw().IfEmpty();

        // Assert
        action.Should()
            .ThrowExactly<ArgumentException>()
            .WithMessage($"String should not be empty. (Parameter '{nameof(value)}')");
    }

    [TestMethod]
    public void ThrowIfEmpty_WhenNotEmpty_ShouldNotThrow()
    {
        // Arrange
        string value = "not empty";

        // Act
        Action action = () => value.Throw().IfEmpty();

        // Assert
        action.Should().NotThrow();
    }

    [TestMethod]
    public void ThrowIfLongerThan_WhenLongerThan_ShouldThrow()
    {
        // Arrange
        string value = "value";

        // Act
        Action action = () => value.Throw().IfLongerThan(2);

        // Assert
        action.Should()
            .ThrowExactly<ArgumentException>()
            .WithMessage($"String should not be longer than 2 characters. (Parameter '{nameof(value)}')");
    }

    [TestMethod]
    public void ThrowIfLongerThan_WhenNotLongerThan_ShouldNotThrow()
    {
        // Arrange
        string value = "value";

        // Act
        Action action = () => value.Throw().IfLongerThan(100);

        // Assert
        action.Should().NotThrow();
    }

    [TestMethod]
    public void ThrowIfShorterThan_WhenShorterThan_ShouldThrow()
    {
        // Arrange
        string value = "value";

        // Act
        Action action = () => value.Throw().IfShorterThan(100);

        // Assert
        action.Should()
            .ThrowExactly<ArgumentException>()
            .WithMessage($"String should not be shorter than 100 characters. (Parameter '{nameof(value)}')");
    }

    [TestMethod]
    public void ThrowIfShorterThan_WhenNotShorterThan_ShouldNotThrow()
    {
        // Arrange
        string value = "value";

        // Act
        Action action = () => value.Throw().IfShorterThan(2);

        // Assert
        action.Should().NotThrow();
    }

    [TestMethod]
    public void ThrowIfEquals_WhenEquals_ShouldThrow()
    {
        // Arrange
        string value = "value";

        // Act
        Action action = () => value.Throw().IfEquals("value");

        // Assert
        action.Should()
            .ThrowExactly<ArgumentException>()
            .WithMessage($"String should not be equal to 'value' (comparison type: '{StringComparison.Ordinal}'). (Parameter '{nameof(value)}')");
    }

    [TestMethod]
    public void ThrowIfEquals_WhenNotEquals_ShouldNotThrow()
    {
        // Arrange
        string value = "value";

        // Act
        Action action = () => value.Throw().IfEquals("VALUE");

        // Assert
        action.Should().NotThrow();
    }

    [DataTestMethod]
    [DataRow("value", "VALUE", StringComparison.OrdinalIgnoreCase)]
    [DataRow("\u0061\u030a", "\u00e5", StringComparison.InvariantCulture)]
    [DataRow("AA", "A\u0000\u0000\u0000a", StringComparison.InvariantCultureIgnoreCase)]
    public void ThrowIfEquals_WhenEqualsUsingCustomComparisonType_ShouldThrow(string value, string otherValue, StringComparison comparisonType)
    {
        // Act
        Action action = () => value.Throw().IfEquals(otherValue, comparisonType);

        // Assert
        action.Should()
            .ThrowExactly<ArgumentException>()
            .WithMessage($"String should not be equal to '{otherValue}' (comparison type: '{comparisonType}'). (Parameter '{nameof(value)}')");
    }

    [DataTestMethod]
    [DataRow("value", "different value", StringComparison.OrdinalIgnoreCase)]
    [DataRow("\u0061\u030a", "different value", StringComparison.InvariantCulture)]
    [DataRow("AA", "different value", StringComparison.InvariantCultureIgnoreCase)]
    public void ThrowIfEquals_WhenNotEqualsUsingCustomComparisonType_ShouldNotThrow(string value, string otherValue, StringComparison comparisonType)
    {
        // Act
        Action action = () => value.Throw().IfEquals(otherValue, comparisonType);

        // Assert
        action.Should().NotThrow();
    }

    [TestMethod]
    public void ThrowIfEqualsIgnoreCase_WhenEqualsSameCase_ShouldThrow()
    {
        // Arrange
        string value = "value";

        // Act
        Action action = () => value.Throw().IfEqualsIgnoreCase("value");

        // Assert
        action.Should()
            .ThrowExactly<ArgumentException>()
            .WithMessage($"String should not be equal to 'value' (comparison type: '{StringComparison.OrdinalIgnoreCase}'). (Parameter '{nameof(value)}')");
    }

    [TestMethod]
    public void ThrowIfEqualsIgnoreCase_WhenEqualsDifferentCase_ShouldThrow()
    {
        // Arrange
        string value = "value";

        // Act
        Action action = () => value.Throw().IfEqualsIgnoreCase("VALUE");

        // Assert
        action.Should()
            .ThrowExactly<ArgumentException>()
            .WithMessage($"String should not be equal to 'VALUE' (comparison type: '{StringComparison.OrdinalIgnoreCase}'). (Parameter '{nameof(value)}')");
    }

    [TestMethod]
    public void ThrowIfEqualsIgnoreCase_WhenNotEquals_ShouldNotThrow()
    {
        // Arrange
        string value = "value";

        // Act
        Action action = () => value.Throw().IfEqualsIgnoreCase("different value");

        // Assert
        action.Should().NotThrow();
    }

    [TestMethod]
    public void ThrowIfNotEquals_WhenNotEquals_ShouldThrow()
    {
        // Arrange
        string value = "value";

        // Act
        Action action = () => value.Throw().IfNotEquals("different value");

        // Assert
        action.Should()
            .ThrowExactly<ArgumentException>()
            .WithMessage($"String should be equal to 'different value' (comparison type: '{StringComparison.Ordinal}'). (Parameter '{nameof(value)}')");
    }

    [DataTestMethod]
    [DataRow("value", "VALUE", StringComparison.OrdinalIgnoreCase)]
    [DataRow("\u0061\u030a", "\u00e5", StringComparison.InvariantCulture)]
    [DataRow("AA", "A\u0000\u0000\u0000a", StringComparison.InvariantCultureIgnoreCase)]
    public void ThrowIfNotEquals_WhenEqualsUsingCustomComparisonType_ShouldNotThrow(string value, string otherValue, StringComparison comparisonType)
    {
        // Act
        Action action = () => value.Throw().IfNotEquals(otherValue, comparisonType);

        // Assert
        action.Should().NotThrow();
    }

    [DataTestMethod]
    [DataRow("value", "different value", StringComparison.OrdinalIgnoreCase)]
    [DataRow("\u0061\u030a", "different value", StringComparison.InvariantCulture)]
    [DataRow("AA", "different value", StringComparison.InvariantCultureIgnoreCase)]
    public void ThrowIfNotEquals_WhenNotEqualsUsingCustomComparisonType_ShouldThrow(string value, string otherValue, StringComparison comparisonType)
    {
        // Act
        Action action = () => value.Throw().IfNotEquals(otherValue, comparisonType);

        // Assert
        action.Should()
            .ThrowExactly<ArgumentException>()
            .WithMessage($"String should be equal to '{otherValue}' (comparison type: '{comparisonType}'). (Parameter '{nameof(value)}')");
    }

    [TestMethod]
    public void ThrowIfNotEquals_WhenEquals_ShouldNotThrow()
    {
        // Arrange
        string value = "value";

        // Act
        Action action = () => value.Throw().IfNotEquals("value");

        // Assert
        action.Should().NotThrow();
    }

    [TestMethod]
    public void ThrowIfNotEqualsIgnoreCase_WhenNotEquals_ShouldThrow()
    {
        // Arrange
        string value = "value";

        // Act
        Action action = () => value.Throw().IfNotEqualsIgnoreCase("different value");

        // Assert
        action.Should()
            .ThrowExactly<ArgumentException>()
            .WithMessage($"String should be equal to 'different value' (comparison type: '{StringComparison.OrdinalIgnoreCase}'). (Parameter '{nameof(value)}')");
    }

    [TestMethod]
    public void ThrowIfNotEqualsIgnoreCase_WhenEquals_ShouldNotThrow()
    {
        // Arrange
        string value = "value";

        // Act
        Action action = () => value.Throw().IfNotEqualsIgnoreCase("value");

        // Assert
        action.Should().NotThrow();
    }

    [TestMethod]
    public void ThrowIfNotEqualsIgnoreCase_WhenEqualsDifferentCase_ShouldNotThrow()
    {
        // Arrange
        string value = "value";

        // Act
        Action action = () => value.Throw().IfNotEqualsIgnoreCase("VALUE");

        // Assert
        action.Should().NotThrow();
    }

    [TestMethod]
    public void ThrowIfLengthEquals_WhenLengthEquals_ShouldThrow()
    {
        // Arrange
        string value = "value";

        // Act
        Action action = () => value.Throw().IfLengthEquals(5);

        // Assert
        action.Should()
            .ThrowExactly<ArgumentException>()
            .WithMessage($"String length should not be equal to 5. (Parameter '{nameof(value)}')");
    }

    [TestMethod]
    public void ThrowIfLengthEquals_WhenLengthNotEquals_ShouldNotThrow()
    {
        // Arrange
        string value = "value";

        // Act
        Action action = () => value.Throw().IfLengthEquals(100);

        // Assert
        action.Should().NotThrow();
    }

    [TestMethod]
    public void ThrowIfLengthNotEquals_WhenLengthNotEquals_ShouldThrow()
    {
        // Arrange
        string value = "value";

        // Act
        Action action = () => value.Throw().IfLengthNotEquals(100);

        // Assert
        action.Should()
            .ThrowExactly<ArgumentException>()
            .WithMessage($"String length should be equal to 100. (Parameter '{nameof(value)}')");
    }

    [TestMethod]
    public void ThrowIfLengthNotEquals_WhenLengthEquals_ShouldNotThrow()
    {
        // Arrange
        string value = "value";

        // Act
        Action action = () => value.Throw().IfLengthNotEquals(value.Length);

        // Assert
        action.Should().NotThrow();
    }

    [TestMethod]
    public void ThrowIfEndsWith_WhenNotEndsWith_ShouldNotThrow()
    {
        // Arrange
        string name = "John";

        // Act
        Action action = () => name.Throw().IfEndsWith("Jo");

        // Assert
        action.Should().NotThrow();
    }

    [TestMethod]
    public void ThrowIfEndsWith_WhenEndsWith_ShouldThrow()
    {
        // Arrange
        string name = "John";

        // Act
        Action action = () => name.Throw().IfEndsWith("hn");

        // Assert
        action.Should()
            .ThrowExactly<ArgumentException>()
            .WithMessage($"String should not end with 'hn'. (Parameter '{nameof(name)}')");
    }

    [TestMethod]
    public void ThrowIfNotEndsWith_WhenNotEndsWith_ShouldThrow()
    {
        // Arrange
        string name = "John";

        // Act
        Action action = () => name.Throw().IfNotEndsWith("Jo");

        // Assert
        action.Should()
            .ThrowExactly<ArgumentException>()
            .WithMessage($"String should end with 'Jo'. (Parameter '{nameof(name)}')");
    }

    [TestMethod]
    public void ThrowIfNotEndsWith_WhenEndsWith_ShouldNotThrow()
    {
        // Arrange
        string name = "John";

        // Act
        Action action = () => name.Throw().IfNotEndsWith("hn");

        // Assert
        action.Should().NotThrow();
    }

    [TestMethod]
    public void ThrowIfStartsWith_WhenNotStartsWith_ShouldNotThrow()
    {
        // Arrange
        string name = "John";

        // Act
        Action action = () => name.Throw().IfStartsWith("hn");

        // Assert
        action.Should().NotThrow();
    }

    [TestMethod]
    public void ThrowIfStartsWith_WhenStartsWith_ShouldThrow()
    {
        // Arrange
        string name = "John";

        // Act
        Action action = () => name.Throw().IfStartsWith("Jo");

        // Assert
        action.Should()
            .ThrowExactly<ArgumentException>()
            .WithMessage($"String should not start with 'Jo'. (Parameter '{nameof(name)}')");
    }

    [TestMethod]
    public void ThrowIfNotStartsWith_WhenNotStartsWith_ShouldThrow()
    {
        // Arrange
        string name = "John";

        // Act
        Action action = () => name.Throw().IfNotStartsWith("hn");

        // Assert
        action.Should()
            .ThrowExactly<ArgumentException>()
            .WithMessage($"String should start with 'hn'. (Parameter '{nameof(name)}')");
    }

    [TestMethod]
    public void ThrowIfNotStartsWith_WhenStartsWith_ShouldNotThrow()
    {
        // Arrange
        string name = "John";

        // Act
        Action action = () => name.Throw().IfNotStartsWith("Jo");

        // Assert
        action.Should().NotThrow();
    }

    [TestMethod]
    public void ThrowIfContains_WhenContains_ShouldThrow()
    {
        // Arrange
        string value = "the quick brown fox jumps over the lazy dog.";
        string otherValue = "quick";

        // Act
        Action action = () => value.Throw().IfContains(otherValue);

        // Assert
        action.Should()
            .ThrowExactly<ArgumentException>()
            .WithMessage($"String should not contain '{otherValue}' (comparison type: '{StringComparison.Ordinal}'). (Parameter '{nameof(value)}')");
    }

    [TestMethod]
    public void ThrowIfContains_WhenNotContains_ShouldNotThrow()
    {
        // Arrange
        string value = "the quick brown fox jumps over the lazy dog.";
        string otherValue = "horse";

        // Act
        Action action = () => value.Throw().IfContains(otherValue);

        // Assert
        action.Should().NotThrow();
    }

    [TestMethod]
    public void ThrowIfNotContains_WhenNotContains_ShouldThrow()
    {
        // Arrange
        string value = "the quick brown fox jumps over the lazy dog.";
        string otherValue = "horse";

        // Act
        Action action = () => value.Throw().IfNotContains(otherValue);

        // Assert
        action.Should()
            .ThrowExactly<ArgumentException>()
            .WithMessage($"String should contain '{otherValue}' (comparison type: '{StringComparison.Ordinal}'). (Parameter '{nameof(value)}')");
    }

    [TestMethod]
    public void ThrowIfNotContains_WhenContains_ShouldNotThrow()
    {
        // Arrange
        string value = "the quick brown fox jumps over the lazy dog.";
        string otherValue = "jumps";

        // Act
        Action action = () => value.Throw().IfNotContains(otherValue);

        // Assert
        action.Should().NotThrow();
    }

    [DataTestMethod]
    [DataRow("value", "AL", StringComparison.OrdinalIgnoreCase)]
    [DataRow("\u0068\u0065\u006c\u006c\u006f", "\u0065\u006c", StringComparison.InvariantCulture)]
    [DataRow("\u0041\u0041", "\u0061", StringComparison.InvariantCultureIgnoreCase)]
    public void ThrowIfContains_WhenContainsUsingCustomComparisonType_ShouldThrow(string value, string otherValue, StringComparison comparisonType)
    {
        // Act
        Action action = () => value.Throw().IfContains(otherValue, comparisonType);

        // Assert
        action.Should()
            .ThrowExactly<ArgumentException>()
            .WithMessage($"String should not contain '{otherValue}' (comparison type: '{comparisonType}'). (Parameter '{nameof(value)}')");
    }

    [DataTestMethod]
    [DataRow("value", "different value", StringComparison.OrdinalIgnoreCase)]
    [DataRow("\u0061\u030a", "different value", StringComparison.InvariantCulture)]
    [DataRow("AA", "different value", StringComparison.InvariantCultureIgnoreCase)]
    public void ThrowIfContains_WhenNotContainsUsingCustomComparisonType_ShouldNotThrow(string value, string otherValue, StringComparison comparisonType)
    {
        // Act
        Action action = () => value.Throw().IfContains(otherValue, comparisonType);

        // Assert
        action.Should().NotThrow();
    }

    [DataTestMethod]
    [DataRow("value", "different value", StringComparison.OrdinalIgnoreCase)]
    [DataRow("\u0061\u030a", "different value", StringComparison.InvariantCulture)]
    [DataRow("AA", "different value", StringComparison.InvariantCultureIgnoreCase)]
    public void ThrowIfNotContains_WhenNotContainsUsingCustomComparisonType_ShouldThrow(string value, string otherValue, StringComparison comparisonType)
    {
        // Act
        Action action = () => value.Throw().IfNotContains(otherValue, comparisonType);

        // Assert
        action.Should()
            .ThrowExactly<ArgumentException>()
            .WithMessage($"String should contain '{otherValue}' (comparison type: '{comparisonType}'). (Parameter '{nameof(value)}')");
    }

    [DataTestMethod]
    [DataRow("value", "AL", StringComparison.OrdinalIgnoreCase)]
    [DataRow("\u0068\u0065\u006c\u006c\u006f", "\u0065\u006c", StringComparison.InvariantCulture)]
    [DataRow("\u0041\u0041", "\u0061", StringComparison.InvariantCultureIgnoreCase)]
    public void ThrowIfNotContains_WhenContainsUsingCustomComparisonType_ShouldNotThrow(string value, string otherValue, StringComparison comparisonType)
    {
        // Act
        Action action = () => value.Throw().IfNotContains(otherValue, comparisonType);

        // Assert
        action.Should().NotThrow();
    }

    [DataTestMethod]
    [DataRow("Amichai", @"^[a-zA-Z]+$", RegexOptions.None)]
    [DataRow("123456789", @"^[0-9]+$", RegexOptions.None)]
    [DataRow("My NAME", @"\bname\b", RegexOptions.IgnoreCase)]
    public void ThrowIfMatches_WhenMatchesRegexPattern_ShouldThrow(string value, string regexPattern, RegexOptions regexOptions)
    {
        // Act
        Action action = () => value.Throw().IfMatches(regexPattern, regexOptions);
        
        // Assert
        action.Should()
            .ThrowExactly<ArgumentException>()
            .WithMessage($"String should not match RegEx pattern '{regexPattern}' (Parameter '{nameof(value)}')");
    }

    [DataTestMethod]
    [DataRow("123456789", @"^[a-zA-Z]+$", RegexOptions.None)]
    [DataRow("Amichai", @"^[0-9]+$", RegexOptions.None)]
    [DataRow("My AGE", @"\bname\b", RegexOptions.IgnoreCase)]
    public void ThrowIfMatches_WhenMatchesRegexPattern_ShouldNotThrow(string value, string regexPattern, RegexOptions regexOptions)
    {
        // Act
        Action action = () => value.Throw().IfMatches(regexPattern, regexOptions);
        
        // Assert
        action.Should().NotThrow();
    }

    [DataTestMethod]
    [DataRow("Amichai", @"^[a-zA-Z]+$", RegexOptions.None)]
    [DataRow("123456789", @"^[0-9]+$", RegexOptions.None)]
    [DataRow("My NAME", @"\bname\b", RegexOptions.IgnoreCase)]
    public void ThrowIfMatches_WhenMatchesRegex_ShouldThrow(string value, string regexPattern, RegexOptions regexOptions)
    {
        // Arrange
        var regex = new Regex(regexPattern, regexOptions);

        // Act
        Action action = () => value.Throw().IfMatches(regex);

        // Assert
        action.Should()
            .ThrowExactly<ArgumentException>()
            .WithMessage($"String should not match RegEx pattern '{regexPattern}' (Parameter '{nameof(value)}')");
    }

    [DataTestMethod]
    [DataRow("123456789", @"^[a-zA-Z]+$", RegexOptions.None)]
    [DataRow("Amichai", @"^[0-9]+$", RegexOptions.None)]
    [DataRow("My AGE", @"\bname\b", RegexOptions.IgnoreCase)]
    public void ThrowIfMatches_WhenMatchesRegex_ShouldNotThrow(string value, string regexPattern, RegexOptions regexOptions)
    {
        // Arrange
        var regex = new Regex(regexPattern, regexOptions);

        // Act
        Action action = () => value.Throw().IfMatches(regex);

        // Assert
        action.Should().NotThrow();
    }

    [DataTestMethod]
    [DataRow("123456789", @"^[a-zA-Z]+$", RegexOptions.None)]
    [DataRow("Amichai", @"^[0-9]+$", RegexOptions.None)]
    [DataRow("My AGE", @"\bname\b", RegexOptions.IgnoreCase)]
    public void ThrowIfNotMatches_WhenMatchesRegexPattern_ShouldThrow(string value, string regexPattern, RegexOptions regexOptions)
    {
        // Act
        Action action = () => value.Throw().IfNotMatches(regexPattern, regexOptions);

        // Assert
        action.Should()
            .ThrowExactly<ArgumentException>()
            .WithMessage($"String should match RegEx pattern '{regexPattern}' (Parameter '{nameof(value)}')");
    }

    [DataTestMethod]
    [DataRow("Amichai", @"^[a-zA-Z]+$", RegexOptions.None)]
    [DataRow("123456789", @"^[0-9]+$", RegexOptions.None)]
    [DataRow("My NAME", @"\bname\b", RegexOptions.IgnoreCase)]
    public void ThrowIfNotMatches_WhenMatchesRegexPattern_ShouldNotThrow(string value, string regexPattern, RegexOptions regexOptions)
    {
        // Act
        Action action = () => value.Throw().IfNotMatches(regexPattern, regexOptions);

        // Assert
        action.Should().NotThrow();
    }

    [DataTestMethod]
    [DataRow("123456789", @"^[a-zA-Z]+$", RegexOptions.None)]
    [DataRow("Amichai", @"^[0-9]+$", RegexOptions.None)]
    [DataRow("My AGE", @"\bname\b", RegexOptions.IgnoreCase)]
    public void ThrowIfNotMatches_WhenMatchesRegex_ShouldThrow(string value, string regexPattern, RegexOptions regexOptions)
    {
        // Arrange
        var regex = new Regex(regexPattern, regexOptions);

        // Act
        Action action = () => value.Throw().IfNotMatches(regex);

        // Assert
        action.Should()
            .ThrowExactly<ArgumentException>()
            .WithMessage($"String should match RegEx pattern '{regexPattern}' (Parameter '{nameof(value)}')");
    }

    [DataTestMethod]
    [DataRow("Amichai", @"^[a-zA-Z]+$", RegexOptions.None)]
    [DataRow("123456789", @"^[0-9]+$", RegexOptions.None)]
    [DataRow("My NAME", @"\bname\b", RegexOptions.IgnoreCase)]
    public void ThrowIfNotMatches_WhenMatchesRegex_ShouldNowThrow(string value, string regexPattern, RegexOptions regexOptions)
    {
        // Arrange
        var regex = new Regex(regexPattern, regexOptions);

        // Act
        Action action = () => value.Throw().IfNotMatches(regex);

        // Assert
        action.Should().NotThrow();
    }
}