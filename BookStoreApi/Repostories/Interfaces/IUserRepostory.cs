using BookStoreApi.DTOS;
using System.Threading.Tasks;

namespace BookStoreApi.Repostories.Interfaces
{
    public interface IUserRepostory
    {
        Task<string> SignUp(SignUpDto signUpDto);
        Task<string> SignIn(LoginDto model);

    }
}
