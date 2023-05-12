namespace DynamicFilter.Lib.Tests.Extensions;

[Trait("Category", "Extensions")]
public class ConvertBetweenEntityAndFilterExtensionTests : BaseTest
{
    [Fact]
    public void ToFilter_Success()
    {
        var convertedFilter = TestUser.ToFilter(TestFilter);

        Assert.Equal(TestUser.Id, convertedFilter.Id);
        Assert.Equal(TestUser.Name, convertedFilter.Name);
        Assert.Equal(TestUser.Location, convertedFilter.Location);
    }
    
    [Fact]
    public void ToEntity_Success()
    {
        var convertedEntity = TestFilter.ToEntity(TestUser);

        Assert.Equal(convertedEntity.Id, TestUser.Id);
        Assert.Equal(convertedEntity.Name, TestUser.Name);
        Assert.Equal(convertedEntity.Location, TestUser.Location);
    }
}