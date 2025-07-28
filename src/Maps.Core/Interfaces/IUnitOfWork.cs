
namespace Maps.src.Maps.Core.Interfaces
{
    public interface IUnitOfWork
    {
        IBranchRepo Branches { get; }
        IUserRepository Users { get; }
        void Dispose();
        Task CompleteAsync();
    }
}
