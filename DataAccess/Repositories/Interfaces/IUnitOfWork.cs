namespace DataAccess.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity , new();

        int SaveChanges();

        public ISessionRepository SessionRepository { get;}
        public IMembershipRepository MembershipRepository { get; }
        public IBookingRepository BookingRepository { get; }
        public IDbContextTransaction BeginTransaction();
    }
}
