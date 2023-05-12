namespace DynamicFilter.Lib.Tests;

[Trait("Category", "Metadata")]
public class DynamicFilterTests : BaseTest
{
    [Fact]
    public void ValueProperties_AND_IsNull_ShouldContainValuesAndIsNullMustBeFalse()
    {
        Assert.Equal(3, TestFilter.ValueProperties.Count());
        Assert.False(TestFilter.IsNull);
    }
    
    [Fact]
    public void ValueProperties_AND_IsNull_ShouldContainNoValuesAndIsNullMustBeTrue()
    {
        TestFilter = new UserFilter();

        Assert.Empty(TestFilter.ValueProperties);
        Assert.True(TestFilter.IsNull);
    }
}