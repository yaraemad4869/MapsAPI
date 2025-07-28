using System.Drawing;
using Maps.src.Maps.Core.Interfaces;
using Maps.src.Maps.Infrastructure.Data;
using static Azure.Core.HttpHeader;

namespace Maps.src.Maps.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _db;

        public UnitOfWork(AppDbContext db)
        {
            _db = db;
            
            Branches = new BranchRepo(_db);
            Users = new UserRepository(_db);
        }
        public IBranchRepo Branches { get; private set; }
        public IUserRepository Users { get; private set; }

        public void Dispose()
        {
            _db.Dispose();
        }
        public async Task CompleteAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
