namespace Core.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
    }
}
