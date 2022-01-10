using BookStoreApi.Models;
using BookStoreApi.Repostory;
using BookStoreApi.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookStoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AutherController : ControllerBase
    {
        public IUnitOfWork _UnitOfWork { get; }
        ResultDto result = new ResultDto();
        public AutherController(IUnitOfWork unitOfWork)
        {
            _UnitOfWork = unitOfWork;
        }
        // GET: api/<AutherController>
        [HttpGet]
        public ResultDto Get()
        {
            result.Message = "All Authers";
            result.Data= _UnitOfWork.GeAutherRepo().Get();
            if (result.Data == null)
            {
                result.Message = "Not Found any Authers ";
                result.ISuccessed = false;

            }
            return result;
        }

        // GET api/<AutherController>/5
        [HttpGet("{id}")]
        public async Task<ResultDto> Get(int id)
        {
            result.Message = "Auther Where ID is: "+id;
            result.Data = await _UnitOfWork.GeAutherRepo().GetByID(id);
            if (result.Data == null)
            {
                result.Message = "Not Found Auther Use Id " + id;
                result.ISuccessed = false;

            }
            return result;
        }

        // POST api/<AutherController>
        [HttpPost]
        public async Task<ResultDto> Post(Auther auther)
        {
            if(auther != null)
            {
               await _UnitOfWork.GeAutherRepo().Add(auther);
                await _UnitOfWork.Save();
                result.Message = "Added new Auther";
                result.Data = auther;
            }
            else
            {
                result.Message = "Not Added";
                result.ISuccessed = false;

            }

            return result;
        }

        // PUT api/<AutherController>/5
        [HttpPut("{id}")]
        public async Task< ResultDto> Put(Auther auther)
        {
            if (auther != null)
            {
                _UnitOfWork.GeAutherRepo().Update(auther);
                await _UnitOfWork.Save();
                result.Message = "Update Auther Done";
                result.Data = auther;
            }
            else
            {
                result.Message = "Not Updeted";
                result.ISuccessed = false;

            }
            return result;

        }

        // DELETE api/<AutherController>/5
        [HttpDelete("{id}")]
        public async Task<ResultDto> Delete(int id)
        {

            if (id != 0)
            {
                IEnumerable<Book> AutherBooks = _UnitOfWork.GetBookRepo().Get().Where(b => b.AutherID == id);
                if (AutherBooks != null)
                {
                    _UnitOfWork.GetBookRepo().RemoveRange(AutherBooks);
                    await _UnitOfWork.Save();

                }

               await _UnitOfWork.GeAutherRepo().Remove(id);
                await _UnitOfWork.Save();
                result.Message = "Removed Auther Done";
            }
            else
            {
                result.Message = "Not Removed";
                result.ISuccessed = false;

            }
            return result;
        }
    }
}
