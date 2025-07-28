using System.Data.Entity;
using Maps.src.Maps.Application.DTOs;
using Maps.src.Maps.Core.Interfaces;
using Maps.src.Maps.Core.Models;
using Maps.src.Maps.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace Maps.src.Maps.Infrastructure.Repositories
{
    public class BranchRepo : BasicRepo<Branch>, IBranchRepo
    {
        private readonly AppDbContext context;
        public BranchRepo(AppDbContext context) : base(context)
        {
            this.context = context;
        }
        public double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
        {
            const double R = 6371; // Earth radius in km
            var dLat = ToRadians(lat2 - lat1);
            var dLon = ToRadians(lon2 - lon1);
            lat1 = ToRadians(lat1);
            lat2 = ToRadians(lat2);

            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Sin(dLon / 2) * Math.Sin(dLon / 2) * Math.Cos(lat1) * Math.Cos(lat2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return R * c;
        }

        public double ToRadians(double angle) => Math.PI * angle / 180.0;
        public async Task<IEnumerable<BranchDistanceDto>> GetNearBranch(double latitude, double longitude, double radiusKm)
        {
            List<Branch>? branches = await context.Branches.ToListAsync();

            return branches
                .Select(b => new BranchDistanceDto
                {
                    Branch = b,
                    Distance = CalculateDistance(latitude, longitude, b.Latitude, b.Longitude)
                })
                .Where(x => x.Distance <= radiusKm)
                .OrderBy(x => x.Distance)
                .ToList();
        }
    }
}
