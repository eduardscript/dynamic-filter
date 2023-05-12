using AutoFixture;
using DynamicFilter.Lib.EFCore.Tests.Entities;
using DynamicFilter.Lib.Shared;
using DynamicFilter.Lib.Shared.Extensions;
using Microsoft.EntityFrameworkCore;

namespace DynamicFilter.Lib.EFCore.Tests;

public class GetAllTests : BaseTest, IClassFixture<TestDbContext>
{
    private readonly Fixture fixture = new();
    private readonly TestDbContext _context;
    private readonly DbSet<User> _dbSet;
    
    public GetAllTests(TestDbContext context)
    {
        _context = context;
        _dbSet = _context.Users;
        _context.RemoveRange(_dbSet);
    }

    [Fact]
    public async Task GetAll_WithDynamicFiltering_ReturnsFilteredEntities()
    {
        // Arrange
        _dbSet.Add(TestUser);
        await _context.SaveChangesAsync();

        TestFilter = TestUser.ConvertEntityValuesToFilterValuesExtension(TestFilter);
        
        // Act
        var insertedUser = (await _dbSet.GetAll(TestFilter)).Single();
        
        // Assert
        AssertEntity(insertedUser, TestUser);
    }
    
    [Fact]
    public async Task GetAll_WithEmptyFilter_ReturnsAllEntries()
    {
        // Arrange
        var usersToBeInserted = fixture.CreateMany<User>()
            .ToList();
        
        _dbSet.AddRange(usersToBeInserted);
        await _context.SaveChangesAsync();

        TestFilter = new UserFilter();
        
        // Act
        var insertedUsers = await _dbSet.GetAll(TestFilter);
       
        // Assert
        Assert.All(insertedUsers, insertedEntity =>
        {
            var rawEntity = usersToBeInserted.Single(x => x.Id == insertedEntity.Id);
        
            AssertEntity(rawEntity, insertedEntity);
        });
    }

    private void AssertEntity(User from, User to)
    {
        Assert.Equal(from.Id, to.Id);
        Assert.Equal(from.Name, to.Name);
        Assert.Equal(from.Location, to.Location);
    }
}