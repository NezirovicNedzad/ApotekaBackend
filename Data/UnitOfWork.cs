using ApotekaBackend.Interfaces;

namespace ApotekaBackend.Data
{
    public class UnitOfWork(DataContext _context,IUserRepository _userRepository,ILekRepository _lekRepository,IKlijentRepository _klijentRepository,IReceptRepository _receptRepository) : IUnitOfWork
    {
        public IUserRepository UserRepository => _userRepository;

        public ILekRepository LekRepository => _lekRepository;

        public IKlijentRepository KlijentRepository => _klijentRepository;

        public IReceptRepository ReceptRepository => _receptRepository;

        public async Task<bool> Complete()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public bool HasChanges()
        {
            return _context.ChangeTracker.HasChanges(); 
        }
    }
}
