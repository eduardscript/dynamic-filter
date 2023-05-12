
namespace DynamicFilter.Lib.Tests.Extensions;

[Trait("Category", "Extensions")]
public class BuildQueryStringExtensionTests : BaseTest
{
    [Theory]
    [InlineData("?id=99&name=TestUser&location=Porto")]
    public void BuildQueryWithMultipleParameters_Success(string expectedQuery)
    {
        var builtQuery = TestFilter.BuildQueryString();

        Assert.Equal(expectedQuery, builtQuery);
    }
    
    [Theory]
    [InlineData("?name=TestUser")]
    public void BuildQueryWithSingleParameter_Success(string expectedQuery)
    {
        TestFilter.Id = null;
        TestFilter.Location = null;

        var builtQuery = TestFilter.BuildQueryString();

        Assert.Equal(expectedQuery, builtQuery);
    }

    [Fact]
    public void EmptyFilter_Success()
    {
        TestFilter = new UserFilter();
        
        var builtQuery = TestFilter.BuildQueryString();

        Assert.Null(builtQuery);
    } 
}