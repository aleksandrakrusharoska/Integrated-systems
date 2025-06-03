using LibraryDomain.Identity;

namespace LibraryRepository.Interface
{
    public interface IUserRepository
    {
        LibraryUser GetUserById(string id);
    }
}