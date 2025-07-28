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

namespace MapsTests.IntegratonTests
{
    public class BranchRepositoryInteratinTests : IDisposable
    {
        private readonly AppDbContext _context;
        private readonly BranchRepo _repository;
        public BranchRepositoryInteratinTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new AppDbContext(options);
            _repository = new BranchRepo(_context);

            _context.Branches.AddRange(
                new Branch { Id = 1, Name = "Downtown", Latitude = 40.7128, Longitude = -74.0060 },
                new Branch { Id = 2, Name = "Uptown", Latitude = 34.0522, Longitude = -118.2437 },
                new Branch { Id = 3, Name = "Midtown", Latitude = 40.7549, Longitude = -73.9840 }
            );
            _context.SaveChanges();
        }

        [Fact]
        public async Task GetByIdAsync_ExistingBranch_ReturnsBranch()
        {
            // Act
            var result = await _repository.GetByIdAsync(11);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async Task AddAsync_NewBranch_AddsToDatabase()
        {
            // Arrange
            var newBranch = new Branch { Name = "Hightown", Latitude = 40.0522, Longitude = -115.2437 };
            // Act
            var result = await _repository.AddAsync(newBranch);
            var fromDb = await _context.Branches.FindAsync(result.Id);

            // Assert
            Assert.NotNull(fromDb);
            Assert.Equal("Hightown", fromDb.Name);
            Assert.Equal(result.Id, fromDb.Id);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
