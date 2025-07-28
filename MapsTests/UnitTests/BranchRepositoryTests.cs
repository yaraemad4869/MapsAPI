using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maps.src.Maps.Core.Models;
using Maps.src.Maps.Infrastructure.Data;
using Maps.src.Maps.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Moq;

namespace MapsTests.UnitTests
{
    public class BranchRepositoryTests
    {
        private readonly DbContextOptions<AppDbContext> _dbContextOptions;
        private readonly AppDbContext _dbContext;
        private readonly BranchRepo _repository;

        public BranchRepositoryTests()
        {
            _dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _dbContext = new AppDbContext(_dbContextOptions);
            _repository = new BranchRepo(_dbContext);

            SeedDatabase();
        }

        private void SeedDatabase()
        {
            _dbContext.Branches.AddRange(
                new Branch { Id = 1, Name = "Downtown", Latitude = 40.7128, Longitude = -74.0060 },
                new Branch { Id = 2, Name = "Uptown", Latitude = 34.0522, Longitude = -118.2437 },
                new Branch { Id = 3, Name = "Midtown", Latitude = 40.7549, Longitude = -73.9840 }
            );
            _dbContext.SaveChanges();
        }

        [Fact]
        public async Task GetNearbyBranchesAsync_ReturnsBranches()
        {
            // Arrange
            double testLat = 40.7128;  // Near Downtown
            double testLon = -74.0060;
            double radiusKm = 5;

            // Act
            var result = await _repository.GetNearBranch(testLat, testLon, radiusKm);

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task GetNearbyBranchesAsync_ReturnsEmptyWhenNoBranchesInRadius()
        {
            // Arrange
            double testLat = 51.5074;  // London
            double testLon = -0.1278;
            double radiusKm = 5;

            // Act
            var result = await _repository.GetNearBranch(testLat, testLon, radiusKm);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetNearbyBranchesAsync_ReturnsOrderedByDistance()
        {
            // Arrange
            double testLat = 40.7500;  // Between Downtown and Midtown
            double testLon = -73.9900;
            double radiusKm = 10;

            // Act
            var result = (await _repository.GetNearBranch(testLat, testLon, radiusKm)).ToList();

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Downtown", result[0].Branch.Name);
        }
        public async Task GetBranches()
        {
            // Arrange
            double testLat = 40.7500;  // Between Downtown and Midtown
            double testLon = -73.9900;
            double radiusKm = 10;

            // Act
            var result = await _repository.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Count);
        }
        public async Task GetBranchById()
        {
            // Arrange
            int branchId = 1; // Downtown
            // Act
            var result = await _repository.GetByIdAsync(branchId);
            // Assert
            Assert.NotNull(result);
            Assert.Equal("Downtown", result.Name);
        }
        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
