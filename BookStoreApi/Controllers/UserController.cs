using BookStoreApi.DTOS;
using BookStoreApi.Repostory;
using BookStoreApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookStoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public UserController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        ResultDto result = new ResultDto();
        [HttpPost("SignUp")]
        public async Task<ResultDto> SignUp(SignUpDto signUp)
        {
           string Token= await unitOfWork.GetUserRepo().SignUp(signUp);
            if(string.IsNullOrEmpty(Token))
                result.ISuccessed=false;

            result.Data = Token;

            return result;
        }

        [HttpPost("Login")]
        public async Task<ResultDto> SignIn(LoginDto model)
        {
            string Token = await unitOfWork.GetUserRepo().SignIn(model);
            result.Data = Token;
            if (Token=="Email or Password is incorrect!")
            {
                result.ISuccessed=false;
            }


            return result;
        }


    }
}
