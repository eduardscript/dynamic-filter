using DynamicFilter.Lib.Shared.Entities;

namespace DynamicFilter.Lib.Shared;

public abstract class BaseTest
{
    protected User TestUser = new()
    {
        Name = "Admin",
        Location = "Porto"
    };
    
    protected UserFilter TestFilter = new()
    {
        Id = 99,
        Name = "TestUser",
        Location = "Porto"
    };
}