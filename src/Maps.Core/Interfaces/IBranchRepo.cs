using Maps.src.Maps.Application.DTOs;
using Maps.src.Maps.Core.Models;

namespace Maps.src.Maps.Core.Interfaces
{
    public interface IBranchRepo : IBasicRepo<Branch>
    {
        double CalculateDistance(double lat1, double lon1, double lat2, double lon2);
        double ToRadians(double angle);
        Task<IEnumerable<BranchDistanceDto>> GetNearBranch(double latitude, double longitude, double radiusKm);
    }
}
