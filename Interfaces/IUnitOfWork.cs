namespace ApotekaBackend.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        ILekRepository LekRepository { get; }   
        IKlijentRepository KlijentRepository { get; }   

        IReceptRepository ReceptRepository { get; } 


        Task<bool> Complete();
        bool HasChanges();  
    }
}
